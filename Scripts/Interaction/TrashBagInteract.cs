

using UnityEngine;

namespace GettingRidOfIt
{
    public class TrashBagInteract : Holder
    {
        public override TrashBagInteract GetTrashBag() => this;
        public override InteractItem GetItem() => this;


        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool pickedUp;

        public bool PickedUp { get => pickedUp; set => pickedUp = value; }


        public void Pickup()
        {
            Player.Instance.Inventory.AddItem(GetItem());

            if (HolderInventory.HasItems())
            {
                GameEvents.PlayerDrag(true);
            }

            spriteRenderer.gameObject.layer = 9;

            PickedUp = true;
            interacted = true;
        }

        private void OnDestroy()
        {
            GameEvents.PlayerDrag(false);
        }

        public void PutDown()
        {
            GameEvents.PlayerDrag(false);

            Player.Instance.Inventory.RemoveItem(GetItem());

            spriteRenderer.gameObject.layer = 10;
            PickedUp = false;
            interacted = false;
        }

        private new void Update()
        {
            base.Update();
            if (pickedUp)
            {
                transform.position = Player.Instance.HandTransform.position;
            }

        }
    }
}
