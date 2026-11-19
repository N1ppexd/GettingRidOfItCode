
using UnityEngine;

namespace GettingRidOfIt
{
    public class FoodInteract : ToolInteract
    {

        [SerializeField]
        private Sprite emptyPlate, fullPlate;


        public Sprite EmptyPlate { get => emptyPlate; }
        public Sprite FullPlate { get => fullPlate; }

        public SpriteRenderer SpriteRenderer { get => spriteRenderer; }

        public override FoodInteract GetFood() => this;

        private void OnEnable()
        {
            GameEvents.OnStopCooking += StopCooking;
        }

        private void OnDisable()
        {
            GameEvents.OnStopCooking -= StopCooking;
        }


        private void StopCooking()
        {
            spriteRenderer.sprite = fullPlate;
        }

        public void Eat()
        {
            spriteRenderer.sprite = emptyPlate;
        }
    }
}
