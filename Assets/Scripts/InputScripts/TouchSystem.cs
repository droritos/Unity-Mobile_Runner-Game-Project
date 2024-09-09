using UnityEngine;

public class TouchSystem : MonoBehaviour
{
    
    Vector2 startPos;
    float minSwipeDist = 30; // Minimum Ssize of swipe
    float startTime;
    float touchDuration;
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    startTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    Vector2 endPos = touch.position;
                    Vector2 swipeDirection = endPos - startPos;
                    touchDuration = Time.time - startTime;
                    //Debug.Log("Touch lasted for: " + touchDuration + " seconds");

                    if (swipeDirection.magnitude < minSwipeDist)
                    {
                        //Debug.Log("Touch Shel halavi!");
                        // VERY SMALL TOOL
                        return;
                    }

                    // Detect direction dude (\L/)00(\R/)
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // horizontal
                        if (swipeDirection.x > 0)
                        {
                            _player.MoveLeft();
                            //Debug.Log("Right Swipe");
                        }
                        else
                        {
                            _player.MoveRight();
                            //Debug.Log("Left Swipe");
                        }
                    }
                    break;
            }
        }
    }
   
}


