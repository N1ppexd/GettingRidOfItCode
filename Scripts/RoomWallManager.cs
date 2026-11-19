using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GettingRidOfIt
{
    public class RoomWallManager : MonoBehaviour
    {

        [Required]
        [SerializeField] private MeshRenderer[] wallMeshes;


        [Required]
        [SerializeField] private Material[] transparentWalls;

        [SerializeField] private float fadeTime;

        [SerializeField] private Color minimumOpacity;
        [SerializeField] private Color maximumOpacity;

        private Material[] startingMaterials;

        private readonly List<Task> startingTasks = new();
        private readonly List<Task> endingTasks = new();

        private void Start()
        {

            for(int i = 0; i < transparentWalls.Length; i++)
            {
                transparentWalls[i] = new Material(transparentWalls[i])
                {
                    color = maximumOpacity
                };
            }

            //Get all of the starting materials
            startingMaterials = new Material[wallMeshes.Length];
            for (int i = 0; i < wallMeshes.Length; i++)
            {
                startingMaterials[i] = wallMeshes[i].material;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Transition(false);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Transition(true);
            }
        }


        void Transition(bool setBack)
        {
            StopAllCoroutines();

            if (setBack)
            {
               Color start = transparentWalls[0].color;
                transparentWalls[1].color = start;

                foreach (var mesh in wallMeshes)
                    mesh.material = transparentWalls[1];

                StartCoroutine(FadeOutAsync(start, maximumOpacity, fadeTime, transparentWalls[1], setBack, null));

                //startingTasks.Add();
                //RoomWallTasks.endingTasks.Add(RoomWallTasks.FadeOutAsync(minimumOpacity, maximumOpacity, fadeTime, transparentWalls[1], setBack, wallMeshes, startingMaterials));
            }
            else
            {
                Material temp = new(transparentWalls[1]);

                Color start = temp.color;

                foreach (var mesh in wallMeshes)
                    mesh.material = temp;

                StartCoroutine(FadeOutAsync(start, minimumOpacity, fadeTime, transparentWalls[0], setBack, temp));

                //startingTasks.Add(FadeOutAsync(start, minimumOpacity, fadeTime, transparentWalls[0], setBack, temp));
                //RoomWallTasks.endingTasks.Add(RoomWallTasks.FadeOutAsync(maximumOpacity, minimumOpacity, fadeTime, transparentWalls[0], setBack, wallMeshes, startingMaterials));
            }
        }

        /*
        async Task SetTasks(List<Task> tasks)
        {
            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);

                foreach (var task in tasks)
                    task.Dispose();

                tasks.Clear();
            }
        }*/

        //make a version of FadeOut using async stuff
        private IEnumerator FadeOutAsync(Color start, Color end, float duration, Material material, bool setBack, Material temp)
        {

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;

                if (!setBack)
                    temp.color = Color.Lerp(start, end, normalizedTime);
                else
                    material.color = Color.Lerp(start, end, normalizedTime);

                yield return null;
            }

            material.color = end;
            if (setBack)
            {
                for (int i = 0; i < wallMeshes.Length; i++)
                    wallMeshes[i].material = startingMaterials[i];
            }
            else
            {
                for (int i = 0; i < wallMeshes.Length; i++)
                    wallMeshes[i].material = material;
            }
        }
    }
}
