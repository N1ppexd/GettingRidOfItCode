using UnityEngine;
using Sirenix.OdinInspector;

namespace GettingRidOfIt
{
    public class Bleeding : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject bloodDecal;

        [Space(10)]
        [MinValue(0)]
        [LabelText("Distance, until bleeding again permamently")]
        [HideLabel]
        [SerializeField] private float distTilBleed;

        private Vector3 lastBleedPos;

        // Start is called before the first frame update
        void Start()
        {
            lastBleedPos = transform.position;
            //StartCoroutine(BleedChecker());
        }

        /*
        IEnumerator BleedChecker()
        {
            yield return new WaitForSeconds(0.04f);

            
        }*/

        private void Update()
        {
            if (Vector3.Distance(lastBleedPos, transform.position) >= distTilBleed)
            {
                Bleed();
            }
        }


        private void Bleed()
        {
            Instantiate(bloodDecal, transform.position, bloodDecal.transform.rotation);
            lastBleedPos = transform.position;
        }
    }
}
