using N1ppeUtils;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace GettingRidOfIt
{
    public class BringTrashPanel : MonoBehaviour
    {
        [Required]
        [SerializeField] private Animator animator;

        [Required]
        [SerializeField] private string goOutAnim, idleAnim;

        private void OnEnable()
        {
            GameEvents.OnBringTrash += StartBringingTrash;
        }
        private void OnDisable()
        {
            GameEvents.OnBringTrash -= StartBringingTrash;
        }

        private void StartBringingTrash()
        {
            Debug.Log("cooking the bodypart");
            animator.Play(goOutAnim);


            foreach(var item in Player.Instance.Inventory.GetItems())
            {
                if (item.GetTrashBag())
                {
                    if (!item.GetTrashBag().HolderInventory.HasItems())
                    {
                        Debug.Log("MITÄ VITTUA?");
                        return;
                    }

                    foreach(var trashitem in item.GetTrashBag().HolderInventory.GetItems())
                    {
                        if(trashitem.GetBodyPart())
                        {
                            Body.Instance.BodyRemaining -= 100f / 11f;

                            Destroy(trashitem.gameObject);

                            Player.Instance.Inventory.RemoveItem(item);

                            Destroy(item.gameObject);
                            return;
                        }
                    }
                }
            }
        }


        public void StopBringingTrash()
        {
            Debug.Log("Trashs hould be dealt with");
        }
    }
}
