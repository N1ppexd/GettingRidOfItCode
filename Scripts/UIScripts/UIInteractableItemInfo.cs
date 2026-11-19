using TMPro;
using UnityEngine;

namespace GettingRidOfIt
{
    public class UIInteractableItemInfo : MonoBehaviour
    {
        public static UIInteractableItemInfo Instance { get; private set; }


        [SerializeField] private TextMeshProUGUI itemNameObj;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }


        public void ShowInfo(InteractItem interactItem)
        {
            itemNameObj.gameObject.SetActive(true);
            itemNameObj.gameObject.transform.position = Camera.main.WorldToScreenPoint(interactItem.transform.position) + Vector3.right * 4;

            itemNameObj.text = interactItem.isItem ? interactItem.ItemSo.itemName : interactItem.gameObject.name;
        }

        public void HideInfo()
        {
            itemNameObj.gameObject.SetActive(false);
        }

    }
}
