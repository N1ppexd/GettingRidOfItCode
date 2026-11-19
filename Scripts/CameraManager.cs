using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        private List<WallManager> walls = new List<WallManager>();


        private void Start()
        {
            player = Player.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if(Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("wall"))
                {
                    if(hit.transform.TryGetComponent<WallManager>(out WallManager wall))
                    {
                        //Set the wall transparent and add it to the list of walls.
                        walls.Add(wall);
                        wall.ChangeTransparency(true);
                    }
                }
                else
                {
                    //Set all walls back to normal
                    foreach(var wall in walls)
                        wall.ChangeTransparency(false);

                    walls.Clear();
                }
            }
        }
    }
}
