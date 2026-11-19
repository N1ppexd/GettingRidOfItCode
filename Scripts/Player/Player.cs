using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GettingRidOfIt
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [Required]
        [SerializeField] private PlayerMovement playerMovement;
        [Required]
        [SerializeField] private Transform handTransform;

        [Required] [SerializeField] private CinemachineImpulseSource impulseSource;

        [Space(10)]
        [MinValue(1)]
        [SerializeField] private int inventorySlots = 1;

        private InventorySystem inventory;


        public InventorySystem Inventory { get => inventory; }
        public Transform HandTransform { get => handTransform; }
        public CinemachineImpulseSource ImpulseSource { get => impulseSource; }

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else Destroy(this);

            inventory = new InventorySystem(inventorySlots);

            GameEvents.OnPlayerTeleport += TeleportPlayer;
            GameEvents.OnPlayerDrag += PlayerDrag;
        }


        private void OnDisable()
        {
            GameEvents.OnPlayerTeleport -= TeleportPlayer;
            GameEvents.OnPlayerDrag -= PlayerDrag;
        }


        //Set the dragging eithe on or off
        private void PlayerDrag(bool drag)
        {
            if(drag)
                playerMovement.Speed = playerMovement.DraggingSpeed;
            if(!drag) 
                playerMovement.Speed = playerMovement.DefaultSpeed;
        }


        /// <summary>
        /// Teleport player when the event happens
        /// </summary>
        /// <param name="pos"></param>
        private void TeleportPlayer(Vector3 pos)
        {
            //start the coroutine that teleports the player
            StartCoroutine(Teleport(pos));
        }



        IEnumerator Teleport(Vector3 pos)
        {
            yield return new WaitForSeconds(0.5f);

            transform.position = pos;

            yield return new WaitForSeconds(0.5f);

        }
    }
}
