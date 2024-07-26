using UnityEngine;

public class GyroInput : MonoBehaviour
{
    [SerializeField] float minShakeInterval = 0.5f;
    [SerializeField] float shakeDetectionThreshold = 1.0f;
    private PlayerMovement _player;

    private float _lastShakeTime;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _lastShakeTime = Time.time;
        shakeDetectionThreshold *= shakeDetectionThreshold;
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration * Time.deltaTime;

        float accelerationSqrMagnitude = acceleration.sqrMagnitude;

        if (accelerationSqrMagnitude >= shakeDetectionThreshold && Time.time >= _lastShakeTime + minShakeInterval)
        {
            _lastShakeTime = Time.time;

            if (Mathf.Abs(acceleration.x) > Mathf.Abs(acceleration.y))
            {
                if (acceleration.x > 0)
                {
                    LeftShaken();
                }
                else
                {
                    RightShaken();
                }
            }
            else
            {
                if (acceleration.y > 0)
                {
                    UpShaken();
                }
                else
                {
                    DownShaken();
                }
            }
        }
    }

    private void RightShaken()
    {
        Debug.Log("Device shaken to the right");
        _player.MoveLeft();
    }

    private void LeftShaken()
    {
        Debug.Log("Device shaken to the left");
        _player.MoveRight();
    }

    private void UpShaken()
    {
        Debug.Log("Device shaken up");
        _player.CheckJump();
    }

    private void DownShaken()
    {
        Debug.Log("Device shaken down");
        _player.CheckSlide();
    }
}
