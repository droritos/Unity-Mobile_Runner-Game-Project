using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTouchDetected2 : MonoBehaviour
{
    private bool _isSwiping = false;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float _startTime;
    private float _endTime;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    _startTime = Time.time;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;
                    _endTime = Time.time;
                    DetectSwipe();
                    break;

            }
        }
    }

    private void DetectSwipe()
    {
        _isSwiping = true; Debug.Log("Swiping");
        float swipeDistanceX = endPosition.x - startPosition.x;
        float swipeDistanceY = endPosition.y - startPosition.y;
        if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
        {
            if (swipeDistanceX > 0)
            {
                Debug.Log("Swipe Right");
            }
            else
            {
                Debug.Log("Swipe Left");

            }
        }
        else
        {
            if (swipeDistanceY > 0)
            {
                Debug.Log("Swipe Up");
            }
            else
            {
                Debug.Log("Swipe Down");
            }
        }
    }
}
