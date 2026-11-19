using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public abstract class InteractItem : MonoBehaviour
    {
        public bool isItem = true;

        [SerializeField] protected ItemSO itemSo;
        [Required]
        public AudioSource audioSource;

        //ihan paskaa mutta ihan sama
        public bool interacted;

        public ItemSO ItemSo { get => itemSo; }

        public bool IsInHolder { get; set;}

        public virtual void PutInItem() { }
        public virtual void TakeOutItem() { }


        public abstract void DefaultInteract(bool onOrOff);
        public virtual InteractItem GetItem() => null;
        public virtual Body GetBody() => null;
        public virtual BodyPart GetBodyPart() => null;
        public virtual Holder GetHolder() => null;
        public virtual TrashBagInteract GetTrashBag() => null;
        public virtual OutDoorInteract GetOutDoor() => null;

        public virtual FoodInteract GetFood() => null;
    }
}
