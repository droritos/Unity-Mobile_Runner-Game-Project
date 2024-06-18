using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintchTouchDetected2 : MonoBehaviour
{
    private float _initialDistance;
    private bool _isPintching = false;
    private float _pintchThreshold = 0.001f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);  

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                //Record the _initialDistance distance between the two touches
                _initialDistance = Vector2.Distance(touch0.position, touch1.position);
                _isPintching = true;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if (_isPintching)
                {
                    // Calculate the current distance between the two touches
                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                    // Determine the pintch direction
                    if (Mathf.Abs(currentDistance - _initialDistance) > _pintchThreshold)
                    {
                        if (currentDistance > _initialDistance)
                        {
                            Debug.Log("Pintch Out . Zoom In");
                        }
                        else
                        {
                            Debug.Log("Pintch In . Zoom Out");
                        }
                        // Update the initial distance for the next comparison
                        _initialDistance = currentDistance;
                    }
                }
            }
            else if (touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled || touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
            {
                // Reset pinching state when any touch ends or is cancled
                _isPintching = false;
            }
        }
    }
}
