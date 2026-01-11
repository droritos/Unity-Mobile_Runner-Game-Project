using UnityEngine;
using DG.Tweening; 
using Interfaces; 

public class EnemyDropBehaviour : MonoBehaviour, IPausable
{
    [Header("Visuals")]
    [SerializeField] ParticleSystem landingVFX; // NEW: Drag your Particle System here

    private Tween _dropTween;
    private bool _isPaused;

    public void StartDrop(float floorY, float fallSpeed)
    {
        _dropTween.Kill();

        // Ensure particles are off before we start
        if(landingVFX) landingVFX.Stop(); // NEW

        _dropTween = transform.DOMoveY(floorY, fallSpeed)
            .SetSpeedBased()
            .SetEase(Ease.InQuad) 
            .OnComplete(OnLanded); // NEW: Call a specific function when done
            
        PauseManager.Instance.Register(this);
    }

    // NEW: The logic that runs when he hits the floor
    private void OnLanded()
    {
        if (landingVFX != null)
        {
            landingVFX.Play();
        }
    }

    public void SetPaused(bool paused)
    {
        _isPaused = paused;
        if (paused) 
        {
            _dropTween.Pause();
            if(landingVFX) landingVFX.Pause(); // NEW: Pause smoke if game pauses
        }
        else 
        {
            _dropTween.Play();
            if(landingVFX) landingVFX.Play();
        }
    }

    private void OnDisable()
    {
        _dropTween.Kill();
        PauseManager.Instance?.Unregister(this);
    }
}