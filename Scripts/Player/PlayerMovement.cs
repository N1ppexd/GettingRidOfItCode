using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using UnityEditor;

namespace GettingRidOfIt
{
    public class PlayerMovement : MonoBehaviour
    {
        #region movementFields

        [TabGroup("Movement settings")]
        [Required]
        [Header("Animator for player animations")]
        [HideLabel]
        [SerializeField] private Animator animator;

        [TabGroup("Movement settings")]
        [Required]
        [SerializeField] private string defaultMovementString, dragMovementString;


        [TabGroup("Movement settings")]
        [Required]
        [Space(10)]
        [Header("Rigidbody for moving the player")]
        [HideLabel]
        [SerializeField] private Rigidbody rb;

        [TabGroup("Movement settings")]
        [Space(5)]
        [Tooltip("this is used now at the start at least to set the player facing the right direction")]
        [Title("Player Sprite renderer")]
        [HideLabel]
        [SerializeField] SpriteRenderer playerSprite;


        [TabGroup("Movement settings")]
        [Space(5)]
        [Header("obstacle layer mask")]
        [SerializeField] private LayerMask whatIsObstacle;

        [TabGroup("Movement settings")]
        [Tooltip("the distance to the wall, when the player stops trying to go through the wall")]
        [SerializeField] private float stopDist;
        #endregion

        private Vector3 direction;

        #region speed settings
        [TabGroup("Speed Settings")]
        [Space(10)]
        [Title("The Current movementSpeed")]
        [ReadOnly]
        [HideLabel]
        [SerializeField] private float speed;

        [Space(5)]
        [TabGroup("Speed Settings")]
        [SerializeField] private float maxSpeed = 30;

        [Space(5)]
        [CustomValueDrawer("ValueForDefaultSpeed")]
        [TabGroup("Speed Settings")]
        [SerializeField] private float defaultSpeed;

        [Space(5)]
        [CustomValueDrawer("ValueForDraggingSpeed")]
        [TabGroup("Speed Settings")]
        [SerializeField] private float draggingSpeed;


        #endregion
        [Space(5)]
        [SerializeField]
        private AudioSource walkAudio;


        private bool isMoving;
        private InputManager inputManager;


        #region EditorShit


#if UNITY_EDITOR
        ///used in the inspector customization
        private float ValueForDefaultSpeed(float value, GUIContent label)
        {
            return EditorGUILayout.Slider(label, value, draggingSpeed, this.maxSpeed);
        }

        ///used in the inspector customization
        private float ValueForDraggingSpeed(float value, GUIContent label)
        {
            return EditorGUILayout.Slider(label, value, 0, this.defaultSpeed);
        }

#endif
#endregion


        #region getters and setters
        public float Speed 
        { 
            get => speed; 
            set
            {
                if (value < 0)
                    value = 0;
                speed = value;
            }
        }

        public float DefaultSpeed { get => defaultSpeed; }
        public float DraggingSpeed { get => draggingSpeed; }

        #endregion

        private void OnEnable()
        {
            inputManager = new InputManager();
            inputManager.Enable();

            Application.targetFrameRate = 60;

            //input event listeners
            inputManager.Player.Move.performed += MovePlayer;
            inputManager.Player.Move.canceled += MovePlayer;

            GameEvents.OnPlayerDrag += PlayerDrag;
        }
        private void OnDisable()
        {
            //STOP LISTENING TO THE INPUTS
            inputManager.Player.Move.performed -= MovePlayer;
            inputManager.Player.Move.canceled -= MovePlayer;

            GameEvents.OnPlayerDrag -= PlayerDrag;
            inputManager.Disable();
        }


        void PlayerDrag(bool isActive)
        {
            animator.SetBool("isDragging", isActive);
        }

        
        //move player
        private void MovePlayer(InputAction.CallbackContext context)
        {
            //if moving input is stopped, we stop moving...
            if (context.canceled)
            {
                animator.SetFloat("speed", 0);
                isMoving = false;
                return;
            }


            //...otherwise we continue moving normwally.

            Vector2 axis = context.ReadValue<Vector2>();

            axis.Normalize();
            direction = new Vector3(axis.x, 0, axis.y);

            isMoving = true;

            animator.SetFloat("speed", 1);
            //axis x cannot be 0
            if (axis.x == 0) return;

            float originalY = playerSprite.transform.localScale.y;
            float originalZ = playerSprite.transform.localScale.z;
            float xSize = Mathf.Abs(playerSprite.transform.localScale.x);


            //make the player sprite flip on the x axis based on the movement direction.
            //In the future we need to control this from a blend tree from the animator
            //  THIS IS A TEMPORARY SOLUTION !!! PITÄÄ MUISTAA VAIHTAA
            playerSprite.transform.localScale = new Vector3((axis.x / Mathf.Abs(axis.x)) * xSize, originalY, originalZ);
        }

        //this is used to move the player. it's the movement directuon.
        private Vector3 dirVector;

        private void Update()
        {
            if (isMoving)
            {
                if (!walkAudio.isPlaying)
                    walkAudio.Play();

                dirVector = direction * speed * Time.deltaTime;

                //if one of these raycasts collides, it gets canceled from the movement vector
                dirVector += GetDirection(direction, dirVector, stopDist);
                dirVector += GetDirection(transform.forward, dirVector, stopDist);
                dirVector += GetDirection(-transform.forward, dirVector, stopDist);
                dirVector += GetDirection(transform.right, dirVector, stopDist);
                dirVector += GetDirection(-transform.right, dirVector, stopDist);

                animator.SetFloat("speed", dirVector.normalized.magnitude);

                //adding the final movement vector to the movement position fro rb.Moveposition
                rb.MovePosition(transform.position + dirVector);
            }
            else
            {
                if (walkAudio.isPlaying)
                    walkAudio.Stop();
            }
        }


        /// <summary>
        /// <paramref name="direction"/> is the direction of the rayast,
        /// <paramref name="dirVector"/> is the direction of movement,
        /// <paramref name="maxDistance"/> is the maximum distance to a wall..
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="dirVector"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        Vector3 GetDirection(Vector3 direction, Vector3 dirVector, float maxDistance)
        {
            //check if the player is about to hit a wall
            if (Physics.Raycast(transform.position, direction, out _, maxDistance, whatIsObstacle))
            {
                Debug.DrawRay(transform.position, direction, Color.red);
                //in this vector, we only have values in where the direction vector has values
                Vector3 directionCorrection = new Vector3(direction.x * dirVector.x, direction.y * dirVector.y, direction.z * dirVector.z);
                //we return the magnitude of it, multiplied by the normal vector of the wall..
                return -direction.normalized * directionCorrection.magnitude;
            }

            //else we return the direction
            Debug.DrawRay(transform.position, direction, Color.yellow);
            return Vector3.zero;
        }
    }
}
