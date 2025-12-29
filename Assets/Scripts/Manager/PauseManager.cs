using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PauseManager : MonoSingleton<PauseManager>
{
    private readonly HashSet<IPausable> _pausables = new HashSet<IPausable>();
    public bool IsPaused { get; private set; }

    public void Register(IPausable p) => _pausables.Add(p);
    public void Unregister(IPausable p) => _pausables.Remove(p);

    public void SetPaused(bool paused)
    {
        if (IsPaused == paused) return;

        IsPaused = paused;

        foreach (var p in _pausables)
            p.SetPaused(paused);
    }
}