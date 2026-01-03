using System;
using GlobalClasses;
using UnityEngine;

namespace Scriptable_Scripts
{
    [CreateAssetMenu(fileName = "AudioDataBase", menuName = "DataBase/AudioDataBase")]
    public class AudioDataBase : ScriptableObject
    {
        [Header("Background Music")]
        public AudioClip GameBackgroundMusic;
        public AudioClip DeadBackgroundMusic;
        
        [Header("Random Clips")]
        [SerializeField] RandomAudio clickSound;
        [SerializeField] RandomAudio collectSound;
        [SerializeField] RandomAudio dashSound;
        [SerializeField] RandomAudio deathSound;
        [SerializeField] RandomAudio hitSound;
        [SerializeField] RandomAudio laserSoundV1;
        [SerializeField] RandomAudio laserSoundV2;
        [SerializeField] RandomAudio laserSoundV3;

        public AudioClip GetRandomAudioClip(RandomAudioType audioType)
        {
            return ConvertAudio(audioType).GetRandomAudioClip();
        }
        
        private RandomAudio ConvertAudio(RandomAudioType audioType)
        {
            switch (audioType)
            {
                case RandomAudioType.PlayerLaser:
                    return laserSoundV1;
                case RandomAudioType.Dash:
                    return dashSound;
                case RandomAudioType.Collected:
                    return collectSound;
                case RandomAudioType.Click:
                    return clickSound;
                case RandomAudioType.Hit:
                    return hitSound;
                case RandomAudioType.EnemyLaser:
                    return laserSoundV2;
                default:
                    return null;
            }
        }
    }
}
