using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Setting
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        private const string MasterSound = "MasterSound";
        private float _originalMasterSound = 0f;
        
        private void OnEnable()
        {
            AudioEventManager.OnAudioChanged += ToggleMute;
        }

        private void OnDisable()
        {
            AudioEventManager.OnAudioChanged -= ToggleMute;
        }


        private void ToggleMute(bool isMuted)
        {
            if (isMuted)
            {
                // Save current volume before muting
                audioMixerGroup.audioMixer.GetFloat(MasterSound, out _originalMasterSound);

                // Mute
                audioMixerGroup.audioMixer.SetFloat(MasterSound, -80f);
            }
            else
            {
                // Restore previous volume
                audioMixerGroup.audioMixer.SetFloat(MasterSound, _originalMasterSound);
            }
        }

    }
}