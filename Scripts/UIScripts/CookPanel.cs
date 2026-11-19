using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public class CookPanel : MonoBehaviour
    {

        [Required]
        [SerializeField] private Animator animator;

        [Required]
        [SerializeField] private string cookAnim, idleAnim;


        private void OnEnable()
        {
            GameEvents.OnStartCooking += StartCooking;
        }

        private void OnDisable()
        {
            GameEvents.OnStartCooking -= StartCooking;
        }


        private void StartCooking()
        {
            Debug.Log("cooking the bodypart");
            animator.Play(cookAnim);
        }

        public void StopCooking()
        {
            animator.Play(idleAnim);
            GameEvents.StopCooking();
        }
    }
}
