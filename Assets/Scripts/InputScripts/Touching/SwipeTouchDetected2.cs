using UnityEngine;

public class SwipeTouchDetected2 : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;
                    DetectSwipe();
                    break;

            }
        }
    }

    private void DetectSwipe()
    {
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
