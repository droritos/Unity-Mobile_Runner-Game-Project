using System;
using UnityEngine;


public static class AudioEventManager
{
    public static event Action<bool> OnAudioChanged;

    public static void ToggleMute(bool mute)
    {
        OnAudioChanged?.Invoke(mute);
    }

}
