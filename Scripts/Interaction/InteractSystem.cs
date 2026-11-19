using N1ppeUtils;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GettingRidOfIt
{
    public class InteractSystem : MonoBehaviour
    {
        InputManager inputManager;

        private Vector2 mousePos;

        private Camera cam;

        [SerializeField] private LayerMask whatIsInteractable, whatIsObstacle, whatIsUI;

        [TitleGroup("Distances")]
        [SerializeField] private float maxDistanceFromPlayer, maxDistanceFromCamera;


        //if the mouse is over UI
        private bool isOverUI;

        //list of interactable objects. One GameObject can have multiple interactableObject scripts
        //(for example for ui prompts and other shit), which is why I did this
        [SerializeField] private List<InteractableObject> currentInteractableObjects = new();
        private InteractableObject lastInteractableObject;

        private void OnEnable()
        {
            try
            {
                //set cam to main camera.
                cam = Camera.main;
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
            }


            //input listeners

            inputManager = new InputManager();
            inputManager.Enable();
            inputManager.UI.Point.performed += SetMousePos;
            inputManager.Player.Fire.performed += Interact;
            inputManager.Player.Fire.canceled += Interact;
        }

        private void OnDisable()
        {
            //stop input listeners

            inputManager.UI.Point.performed -= SetMousePos;
            inputManager.Player.Fire.performed -= Interact;
            inputManager.Player.Fire.canceled -= Interact;
            inputManager.Disable();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
            Gizmos.DrawWireSphere(transform.position, maxDistanceFromPlayer);
        }

        /// <summary>
        /// Set the mouse position based on some input shit
        /// </summary>
        /// <param name="context"></param>
        void SetMousePos(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();

            //Debug.Log("mousePos moving " + mousePos);
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = cam.ScreenPointToRay(mousePos);

            Debug.Log($"is over UI {isOverUI}");


            //if the object the player interacted with last is too far away, we stop interacting
            //This puts the interact canvas away

            Debug.Log($"lastInteractableObject: {lastInteractableObject}");

            if (lastInteractableObject != null && Vector3.Distance(transform.position, lastInteractableObject.transform.position) > maxDistanceFromPlayer)
            {
                Debug.Log("stop interacting");
                lastInteractableObject.Interact(false);
                lastInteractableObject = null;
            }

            isOverUI = NiilosExtensions.IsOverUi();

            if (isOverUI) return;

            //if the mouse is howering over something that can be interacted with
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceFromCamera, whatIsInteractable))
            {
                

                if (Vector3.Distance(transform.position, hit.point) > maxDistanceFromPlayer)
                {
                    return;
                }
                
                //if there are obstacles between player and the interactable object, it's not added to the list
                /*if (HasObstaclesBetween(transform, hit, whatIsObstacle, whatIsInteractable))
                {
                    return;
                }*/

                //if (isOverUI && !Player.Instance.Inventory.HasItems()) return;

                try
                {
                    foreach(var i in currentInteractableObjects)
                        i.Highlight(false);

                    currentInteractableObjects = hit.transform.GetComponents<InteractableObject>().ToList();

                    foreach(var io in currentInteractableObjects)
                    {

                        //if the object is not yet highlighte, it gets highlighted
                        if (!io.hasHighlighted)
                            io.Highlight(true);
                    }
                }
                catch
                {
                    currentInteractableObjects.Clear();
                    return;
                }
            }
            //if the mouse is not howering over something interactable, but there are still objects highlighted...
            else if (currentInteractableObjects.Count > 0)
            {
                //we check if the player is interacting with them, and if not, we stop highlighting it
                //if the player is interacting, we simply return immediately
                foreach (var io in currentInteractableObjects)
                {
                    if (io.isInteracting) 
                        return;

                    try
                    {
                        io.Highlight(false);
                    }
                    catch
                    {
                        Debug.Log("highighjt unable to do");
                    }
                }

                currentInteractableObjects.Clear();
            }
        }


        /// <summary>
        /// Check if there are any obstacles between <paramref name="a"/> and <paramref name="b"/> with a tag of <paramref name="whatIsObstacle"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="whatIsObstacle"></param>
        /// <returns></returns>
        private bool HasObstaclesBetween(Transform start, RaycastHit targetHit, LayerMask whatIsObstacle, LayerMask whatIsInteractable)
        {

            //where the ray starts
            Vector3 startPos = start.position;

            //vector from a to b
            Vector3 diffVector = targetHit.point - startPos;


            Debug.DrawRay(startPos, diffVector);

            bool hasHit = Physics.Raycast(startPos, diffVector, out RaycastHit hit, diffVector.magnitude, whatIsObstacle);

            //if(!hasHit) return false;

            //if it hits either a or b, we return false, because those don't count, otherwise we return hasHit
            //return hit.transform.root != start.root && hit.transform.root != targetHit.transform.root && hasHit;

            return false;
        }



        /// <summary>
        /// Interact with the selected object
        /// </summary>
        /// <param name="context"></param>
        private void Interact(InputAction.CallbackContext context)
        {
            if (isOverUI) return;

            bool interact = GetInteractMode(context);


            //remove all null objects from the list if there are any
            foreach(var io in currentInteractableObjects)
            {
                if (io == null) 
                { 
                    currentInteractableObjects.Remove(io); 
                    continue; 
                }
            }


            //go through the list of current interactable objects
            //and do interact things with them
            foreach(var io in currentInteractableObjects)
            {
                if (io.mode == InteractMode.prompt)
                    continue;

                if (io.mode == InteractMode.hold)
                {
                    if (context.duration >= InputSystem.settings.defaultHoldTime)
                    {
                        io.Interact(interact);

                        lastInteractableObject = interact ? io : null;
                    }
                    else
                    {
                        //hold interaction is disabled if click interaction is enabled.
                        io.Interact(false);
                        lastInteractableObject = null;
                    }
                }

                if (io.mode == InteractMode.click)
                {
                    //Debug.Log("tapping should soon happen + interact is " + interact + " duration = " + context.duration);

                    if (context.duration < InputSystem.settings.defaultHoldTime)
                    {
                        io.Interact(!io.isInteracting);

                        if (!io.isInteracting) lastInteractableObject = null;
                        else lastInteractableObject = io;

                        Debug.Log("tapping happened");
                    }
                    else 
                    {
                        //if the hold interaction starts while the click interaction is on,
                        // the click interaction gets disabled
                        io.Interact(false);
                        lastInteractableObject = null;
                    }
                }
            }



            //clear all interactable objects so that they are not interacted anymore later
            currentInteractableObjects.Clear();


            //get the interact mode
            static bool GetInteractMode(InputAction.CallbackContext context)
            {
                return context.performed ? true : context.canceled ? false : false;
            }
        }
    }
}
