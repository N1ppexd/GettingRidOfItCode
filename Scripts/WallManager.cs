using UnityEngine;

namespace GettingRidOfIt
{
    public class WallManager : MonoBehaviour
    {
        [Header("Material for the wall")]
        [SerializeField] private Material transparentMaterial, defaultMaterial;

        [Space(5)]
        [Header("mesh renderer")]
        [SerializeField] private MeshRenderer meshRenderer;

        public void ChangeTransparency(bool on)
        {
            meshRenderer.material = on ? transparentMaterial : defaultMaterial;
        }
    }
}
