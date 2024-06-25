using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchHandler2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int touchCount = Input.touchCount;

        if(touchCount < 1) return;

        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            //Display informtaion about each touch
            Debug.Log($"Touch {i} - Position {touch.position} Phase: {touch.phase}");

            // You can add additional touch handling logic herer
            // For example, handle different touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log($"Touch {i}. Started at Position {touch.position}");
                    break;
                case TouchPhase.Moved:
                    Debug.Log($"Touch {i}. Moved at Position {touch.position}");
                    break;
                case TouchPhase.Stationary:
                    Debug.Log($"Touch {i} is Stationary at Position {touch.position}");
                    break;
                case TouchPhase.Ended:
                    Debug.Log($"Touch {i}. Ended at Position {touch.position}");
                    break;
                case TouchPhase.Canceled:
                    Debug.Log($"Touch {i}. Cancled at Position {touch.position}");
                    break;
            }
        }

    }


}
