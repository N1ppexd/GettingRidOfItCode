using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GettingRidOfIt
{
    public class AudioSettings : MonoBehaviour
    {

        [SerializeField]
        private AudioMixer playerAudio, backgroundAudio;

        [SerializeField] Slider playerAudioSlider, backgroundAudioSlider;

        private void Start()
        {
            SetPlayerVolume(PlayerPrefs.GetFloat("PlayerVolume", 0));
            SetBackgroundVolume(PlayerPrefs.GetFloat("BackgroundVolume", 0));

            playerAudioSlider.value = PlayerPrefs.GetFloat("PlayerVolume", 0);
            backgroundAudioSlider.value = PlayerPrefs.GetFloat("BackgroundVolume", 0);
        }

        public void SetPlayerVolume(float volume)
        {
            playerAudio.SetFloat("Volume", volume);

            PlayerPrefs.SetFloat("PlayerVolume", volume);
        }

        public void SetBackgroundVolume(float volume)
        {
            backgroundAudio.SetFloat("Volume", volume);

            PlayerPrefs.SetFloat("BackgroundVolume", volume);
        }

    }
}
