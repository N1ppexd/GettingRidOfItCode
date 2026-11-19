using Sirenix.OdinInspector;
using UnityEngine;

namespace GettingRidOfIt
{
    [RequireComponent(typeof(Player))]
    public class PlayerInteract : InteractableObject
    {
        [Required]
        [SerializeField] private Player player;


        [Space(20)]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color highlightColor;

        [Required]
        [Space(10)]
        [SerializeField] private SpriteRenderer spriteRenderer;

        public override void Highlight(bool highlight)
        {
            hasHighlighted = highlight;
            Debug.Log($"Player highlight = {highlight}");
            //throw new System.NotImplementedException();
        }

        public override void Interact(bool interact)
        {

            //if the player has an item, this way the interact also happens when the player character is clicked,
            //and there is no need to click the item that is in the players hand
            Debug.Log("Player interact!");

            if (player.Inventory.HasItems())
            {
                if(player.Inventory.GetItems()[0].TryGetComponent(out PromptInteract prompt))
                {
                    prompt.Interact(true);
                }
                else
                {
                    Debug.Log("prompt interact does not exist in the item object");
                }
                
            }

            Highlight(false);
        }


        private void Update()
        {
            if (hasHighlighted)
            {
                //set the player highlight color
                if(spriteRenderer.color != highlightColor)
                    spriteRenderer.color = highlightColor;
            }
            //otherwise we set the default color
            else if (!hasHighlighted)
            {
                if (spriteRenderer.color != defaultColor)
                    spriteRenderer.color = defaultColor;
            }
        }
    }
}
