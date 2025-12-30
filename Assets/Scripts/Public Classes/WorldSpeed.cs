using UnityEngine;

public class WorldSpeed : MonoBehaviour
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

    private void Awake()
    {
        // RESET STATIC VARIABLES
        SpeedMultiplier = minSpeed;
        _targetMultiplier = minSpeed;
    }

    void Update()
    {
        // 1. Calculate Target Speed based on Score
        // Formula: BaseSpeed + (TotalScore * IncreaseFactor)
        if (ScoreManager.Instance != null)
        {
            float currentScore = ScoreManager.Instance.TotalScore;
            _targetMultiplier = minSpeed + (currentScore * speedIncreasePerPoint);
        }

        // 2. Cap the speed (Safety limit)
        _targetMultiplier = Mathf.Clamp(_targetMultiplier, minSpeed, maxSpeed);

        // 3. Smoothly move actual speed towards target
        SpeedMultiplier = Mathf.Lerp(SpeedMultiplier, _targetMultiplier, Time.deltaTime * smoothingSpeed);
    }
}