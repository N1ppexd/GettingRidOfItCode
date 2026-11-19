using UnityEngine;

namespace GettingRidOfIt
{
    public class Faucet : InteractItem
    {


        [SerializeField] private ParticleSystem water;

        public override void DefaultInteract(bool onOrOff)
        {
            if(onOrOff)
            {
                water.Play();
                if(audioSource != null)
                    audioSource.Play();
            }
            else
            {
                water.Stop();
                if (audioSource != null)
                    audioSource.Stop();
            }

            interacted = onOrOff;
        }


        public override InteractItem GetItem() => this;
    }
}
