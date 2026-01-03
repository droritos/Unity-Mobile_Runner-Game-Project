using UnityEngine;

namespace Scriptable_Scripts
{
    [CreateAssetMenu(fileName = "Random Audio", menuName = "ScriptableObject/Random Audio")] // You can now Create new Road file under "ScriptableObject"
    public class RandomAudio : ScriptableObject
    {   
        [SerializeField] AudioClip[] m_audioClip;

        public AudioClip GetRandomAudioClip()
        {
            return m_audioClip[Random.Range(0, m_audioClip.Length)];
        }
    }
}
