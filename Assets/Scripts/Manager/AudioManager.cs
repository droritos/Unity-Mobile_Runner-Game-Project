using GlobalClasses;
using Scriptable_Scripts;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Clips Library")]
        [SerializeField] AudioDataBase audioDataBase;

        private void Awake()
        {
            // Singleton Setup
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayMusic(audioDataBase.GameBackgroundMusic);
        }

        // --- MUSIC ---
        public void PlayMusic(AudioClip clip)
        {
            if (clip == null) return;
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        // --- SFX (Simple) ---
        public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            // PlayOneShot allows multiple sounds to overlap (e.g. rapid coin pickups)
            sfxSource.PlayOneShot(clip, volume);
        }

        // --- SFX (With Random Pitch) ---
        // Perfect for Coins!
        public void PlayRandomPitchSFX(AudioClip clip)
        {
            if (clip == null) return;

            // Randomize pitch slightly (0.9 to 1.1)
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip);
        
            // Reset pitch back to normal immediately after triggering
            // (Note: PlayOneShot uses the source's CURRENT settings at the moment of playing)
            // However, to be safe for the next sound, we can reset it in a coroutine or just accept the source stays pitched.
            // A cleaner way for single source is to reset it after:
            // But since PlayOneShot "fires and forgets", changing pitch immediately might affect it depending on Unity version.
            // TRICK: For PlayOneShot, the pitch must be set BEFORE calling it.
            // To avoid messing up other sounds, we usually reset it, but that might clip the audio.
            // The PRO way is to spawn a temp object, but for mobile optimization, we stick to this:
        }
        public void PlaySFXByType(RandomAudioType randomAudioType)
        {
            AudioClip clip = audioDataBase.GetRandomAudioClip(randomAudioType);
            if (clip == null) return;

            // Randomize pitch slightly (0.9 to 1.1)
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip);
        
            // Reset pitch back to normal immediately after triggering
            // (Note: PlayOneShot uses the source's CURRENT settings at the moment of playing)
            // However, to be safe for the next sound, we can reset it in a coroutine or just accept the source stays pitched.
            // A cleaner way for single source is to reset it after:
            // But since PlayOneShot "fires and forgets", changing pitch immediately might affect it depending on Unity version.
            // TRICK: For PlayOneShot, the pitch must be set BEFORE calling it.
            // To avoid messing up other sounds, we usually reset it, but that might clip the audio.
            // The PRO way is to spawn a temp object, but for mobile optimization, we stick to this:
        }
    
        // Better Random Pitch for Mobile (Safe):
        public void PlayCollectSound()
        {
            if (audioDataBase == null) return;
        
            // Change pitch, play, then we accept the source stays at that pitch until next sound
            sfxSource.pitch = Random.Range(0.95f, 1.05f); 
            sfxSource.PlayOneShot(audioDataBase.GetRandomAudioClip(RandomAudioType.Collected));
        }
    
        // Reset pitch for UI clicks so they sound consistent
        public void PlayClickSound()
        {
            sfxSource.pitch = 1f;
            sfxSource.PlayOneShot(audioDataBase.GetRandomAudioClip(RandomAudioType.Click));
        }
        
        
    }
}