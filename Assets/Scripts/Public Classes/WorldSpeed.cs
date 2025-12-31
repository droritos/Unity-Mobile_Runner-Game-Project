using System;
using Interfaces;
using UnityEngine;

public class WorldSpeed : MonoBehaviour , IPausable
{
    public static float SpeedMultiplier = 1f;
    private static float _targetMultiplier = 1f;

    [Header("Difficulty Settings")]
    [SerializeField] private float smoothingSpeed = 2f; // How fast it accelerates to the new speed
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 10f;
    
    // Formula: Speed increases by 'speedIncreasePerPoint' for every 1 point of score
    // Example: 0.001 means for every 1000 score, speed increases by +1
    [SerializeField] private float speedIncreasePerPoint = 0.0005f; 
    
    // Overide Settings
    private static float _difficultyTarget = 1f;
    private static float _overrideMultiplier = 1f;
    private static bool _hasOverride = false;

    private void Awake()
    {
        // RESET STATIC VARIABLES
        SpeedMultiplier = minSpeed;
        _targetMultiplier = minSpeed;
        
        PauseManager.Instance?.Register(this);
    }

    private void OnDestroy()
    {
        PauseManager.Instance.Unregister(this);
    }
    void Update()
    {
        // ----- Difficulty-based speed -----
        if (!_hasOverride && ScoreManager.Instance != null)
        {
            float score = ScoreManager.Instance.TotalScore;
            _difficultyTarget = minSpeed + (score * speedIncreasePerPoint);
            _difficultyTarget = Mathf.Clamp(_difficultyTarget, minSpeed, maxSpeed);
        }

        // ----- Choose target -----
        float target = _hasOverride ? _overrideMultiplier : _difficultyTarget;

        // ----- Smooth final world speed -----
        SpeedMultiplier = Mathf.Lerp(
            SpeedMultiplier,
            target,
            Time.deltaTime * smoothingSpeed
        );
    }

   // public float GetNormalizedSpeed() => Mathf.InverseLerp(minSpeed, maxSpeed, WorldSpeed.SpeedMultiplier);
    public void SetPaused(bool paused)
    {
        if (paused)
        {
            SetOverrideSpeed(0.5f);   // slow-mo / pause
        }
        else
        {
            ClearOverride();
        }
    }
    private static void SetOverrideSpeed(float speed)
    {
        _overrideMultiplier = Mathf.Max(0f, speed); // allow 0
        _hasOverride = true;
    }
    private static void ClearOverride()
    {
        _hasOverride = false;
    }

}