using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    [CreateAssetMenu(fileName ="Item", menuName =("Items/item"))]
    public class ItemSO : ScriptableObject
    {


        [PropertySpace(SpaceAfter = 20)]
        public string itemName;

        [PreviewField(100, ObjectFieldAlignment.Left)]
        [PropertySpace(SpaceAfter = 20)]
        public Sprite itemSprite;

        [InfoBox("how many items can the player carry in addition to this one")]
        [MinValue(0)]
        public int maxAmount;

        public ItemType itemType;
        
        [Range(0, 100f)]
        [Title ("Carry player speed %")]
        [InfoBox("Speed of the real player speed. 100 = 100%, 0% = player does not move")]
        public float carrySpeedAmount = 100f;


        [Required]
        [PropertySpace(SpaceBefore = 20, SpaceAfter = 20)]
        [Range(0, 100)]
        public float decompositionPerHour;


        [Space(20)]
        [Title("list of all interact actions for this item")]
        [HideLabel]
        public List<InteractData> interactDatas = new();
    }
}
