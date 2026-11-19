using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public class CollisionInteract : MonoBehaviour
    {
        [Required]
        [Title("interactable object")]
        [SerializeField] private InteractableObject interactableObject;



        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {

                Debug.Log("player collided with " + interactableObject.name);
                interactableObject.Interact(true);
                interactableObject.Interact(false);
            }
        }
    }
}
