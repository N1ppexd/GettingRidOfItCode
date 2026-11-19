using UnityEngine;

namespace GettingRidOfIt
{
    public class BillBoard : MonoBehaviour
    {
        private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (cam != null)
            {
                Vector3 targetPos = cam.transform.position;
                targetPos.x = transform.position.x;

                Vector3 dirVector = transform.position - targetPos;

                transform.LookAt(transform.position + dirVector);
            }
            else Debug.LogError("mitä vittua? Kamera on null");
        }
    }
}
