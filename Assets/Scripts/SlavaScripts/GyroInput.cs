using UnityEngine;
public class GyroInput : MonoBehaviour
{

    [SerializeField] float minShakeInterval = 0.5f;
    [SerializeField] float shakeDetectionThreshold = 1.0f;
    
    private float _lastShakeTime;

    private void Start()
    {
        _lastShakeTime = Time.time;

        shakeDetectionThreshold *= shakeDetectionThreshold;
    }

    void Update()
    {
        Vector3 accelertion = Input.acceleration * Time.deltaTime;

        float accelerationSqrMagnitude = accelertion.sqrMagnitude;

        if (accelerationSqrMagnitude >= shakeDetectionThreshold && Time.time >= _lastShakeTime + minShakeInterval)
        {
            _lastShakeTime = Time.time;

            if (Mathf.Abs(accelertion.x) > Mathf.Abs(accelertion.y))
            {
                HorizontalShaken();
            }
            else
            {
                VerticalShaken();
            }
        }
    }

    private void HorizontalShaken()
    {
        Debug.Log("Device Horizontal Shake");
    }

    private void VerticalShaken()
    {
        Debug.Log("Device Vetical Shake");
    }

}
