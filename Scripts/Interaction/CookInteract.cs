using Unity.VisualScripting;
using UnityEngine;

namespace GettingRidOfIt
{
    public class CookInteract : InteractItem
    {

        //Hyvin paskaa koodia mutta nytten vain pitää laittaa tämä toimimaan ja miettiä huomenna tarkemmin asioita
        public override void DefaultInteract(bool onOrOff)
        {
            Debug.Log("cooking the bodypart");

            GameEvents.StartCooking();

            if (Player.Instance.Inventory.HasItems())
            {
                foreach (var item in Player.Instance.Inventory.items)
                {
                    if (item.ItemSo == null)
                    {
                        continue;
                    }

                    if (item.ItemSo.itemType == ItemType.bodypart)
                    {
                        //väliaikainen ratkaisu

                        //IHAN SAATANAN PASKAA KOODIA.
                        //TÄLLÄ HETKELLÄ PELIÄ EI VOI VOITTAA JOS SINNE LIATTAA KOKONAISEN RUUMIINOSAN TÄMÄN TAKIA.
                        //SE PROSENTTI PITÄÄ OTTAA ITEMISTÄ, JA KOKONAISTA RAAJAA EI VOI LAITTAA KERRALLA, VAAN PITÄÄ LAITTAA PALOISSA.
                        //JALKOJEN JA KÄSIEN YLÄOSILLE PITÄÄ LAITTAA OMAT ITEM SCRIPTABLEOBJECTIT.  
                        
                        Body.Instance.BodyRemaining -= 100f / 11f;


                        Player.Instance.Inventory.RemoveItem(item);

                        Destroy(item.gameObject);
                        return;
                    }
                }
            }

            interacted = onOrOff;
        }
    }
}
