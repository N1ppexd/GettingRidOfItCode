using UnityEngine;

namespace GettingRidOfIt
{
    public class ToolInteract : InteractItem
    {
        [Header("Sprite")]
        [SerializeField] protected SpriteRenderer spriteRenderer;


        [Space(10)]
        [Header("highlight color")]
        [SerializeField] private Color highlightColor;

        [Space(5)]
        [Header("interact color")]
        [SerializeField] private Color interactColor;

        [Space(5)]
        [Header("default color")]
        [SerializeField] private Color defaultColor;

        //player
        private Player player;
        public override InteractItem GetItem() => this;

        public override void DefaultInteract(bool onOrOff)
        {
            player = Player.Instance;

            interacted = onOrOff;

            if (onOrOff)
            {
                spriteRenderer.gameObject.layer = 9;
                player.Inventory.AddItem(this);
            }
            else
            {
                spriteRenderer.gameObject.layer = 10;

                if (player.Inventory.items.Contains(this))
                    player.Inventory.items.Remove(this);
            }
            //if interact is true, color is set to interactcolor. if false, it's set to default color
            //spriteRenderer.color = onOrOff ? interactColor : defaultColor;
        }

        private void Update()
        {
            if(interacted)
            {
                if (player == null) player = Player.Instance;

                if (player == null) { Debug.LogError("wtf? player is still null"); return; }
                if(player.HandTransform == null) { Debug.LogError("wtf? Player has no hand.."); return; }


                //tool laitetaan pelaajan käteen...
                transform.position = player.HandTransform.position;
                transform.rotation = player.HandTransform.rotation;
            }
        }
    }
}
