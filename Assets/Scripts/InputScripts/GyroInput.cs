using UnityEngine;

public class GyroInput : MonoBehaviour
{
    [SerializeField] float shakeDetectionThreshold = 1.0f;
    [SerializeField] float minShakeEffort = 5f;

    [Header("Logic")]
    private Gyroscope _gyro;
    private bool _gyroActive;

    private PlayerMovement _player;
    private float _lastShakeTime;

    private void Start()
    {
        EnableGyro();  // Enable the gyroscope
        _player = FindObjectOfType<PlayerMovement>();
        _lastShakeTime = Time.time;
        shakeDetectionThreshold *= shakeDetectionThreshold;
    }

    private void EnableGyro()
    {
        if (_gyroActive) return;

        if (SystemInfo.supportsGyroscope)
        {
            _gyro = Input.gyro;
            _gyro.enabled = true;
            _gyroActive = true;
        }
    }

    void Update()
    {
        // Gyroscope rotation (euler angles)
        if (_gyroActive)
        {
            Vector3 gyroRotation = _gyro.attitude.eulerAngles;
            //HandleGyroMovement(gyroRotation);
            Debug.Log("gyroRotation " + _gyro.attitude.eulerAngles);
        }

        //// Accelerometer-based shake detection
        //Vector3 acceleration = Input.acceleration * Time.deltaTime;
        //float accelerationSqrMagnitude = acceleration.sqrMagnitude;

        //if (accelerationSqrMagnitude >= shakeDetectionThreshold && Time.time >= _lastShakeTime + minShakeInterval)
        //{
        //    _lastShakeTime = Time.time;

        //    if (Mathf.Abs(acceleration.x) > Mathf.Abs(acceleration.y))
        //    {
        //        if (acceleration.x > 0)
        //        {
        //            LeftShaken();
        //        }
        //        else
        //        {
        //            RightShaken();
        //        }
        //    }
        //}
    }

    private void HandleGyroMovement(Vector3 rotation)
    {
        // Assuming you're mapping rotation.x to horizontal movement
        if (rotation.x > minShakeEffort) // Rotate to the right
        {
            _player.MoveRight();
        }
        else if (rotation.x < -minShakeEffort) // Rotate to the left
        {
            _player.MoveLeft();
        }
        // You can further refine the thresholds and controls based on your gameplay
    }

    private void RightShaken()
    {
        Debug.Log("Device shaken to the right");
        _player.MoveRight();
    }

    private void LeftShaken()
    {
        Debug.Log("Device shaken to the left");
        _player.MoveLeft();
    }
}
