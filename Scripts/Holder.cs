using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    public class Holder : DoorInteract
    {
        //this is true, if the player has to open it in order to put something in it, not necessarily always the case, for example the trash bag doesn't have to be opened
        [SerializeField] private bool hasToOpen = true;

        //Slots for holder inventory
        [MinValue(0)]
        [SerializeField] private int holdAmount;

        [Required]
        [SerializeField] private Transform holdPosition;

        [Space(10)]
        [Required]
        [SerializeField]
        private HolderSO holderSO;

        InventorySystem holderInventory;

        public InventorySystem HolderInventory { get => holderInventory; }

        public bool HasToOpen { get => hasToOpen; }

        private void Awake()
        {
            holderInventory = new InventorySystem(holdAmount);
        }

        public override void DefaultInteract(bool onOrOff)
        {
            Debug.Log("interact!");
            base.DefaultInteract(onOrOff);
        }

        /// <summary>
        /// Place an item into the holder
        /// </summary>
        public void PlaceItem(InteractItem item)
        {

            Player.Instance.Inventory.RemoveItem(item);

            holderInventory.AddItem(item);

            

            item.PutInItem();
            //when the isInHolder is true, the player cannot interact with the holder directly without taking it out first
            try
            {
                item.IsInHolder = true;
                item.GetBodyPart().CurrentHolder = holderSO;
            }
            catch
            {
                Debug.LogError("couldn't add an holder scriptableObj");
            }
            
            item.transform.position = holdPosition.position;

            //place the item in the holder
        }


        //get the item from the holder
        public InteractItem TakeItem()
        {
            if (holderInventory.HasItems())
            {
                var item = holderInventory.GetItems()[0];
                item.TakeOutItem();
                holderInventory.RemoveItem(item);
                item.IsInHolder = false;


                if(item.GetBodyPart() != null)
                    item.GetBodyPart().CurrentHolder = null;


                return item;
            }
            return null;
        }



        protected void Update()
        {
            if(holderInventory.HasItems())
            {
                //keep all items in the holder inventory in the holder so that they don't move or fall
                foreach(var item in holderInventory.GetItems())
                {
                    item.transform.position = holdPosition.position;
                }
            }
        }


        public override Holder GetHolder() => this;
    }
}
