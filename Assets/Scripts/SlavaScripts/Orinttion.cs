using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orinttion : MonoBehaviour
{
    
    Vector2 startPos;
    float minSwipeDist = 30;
    float startTime;
    float touchDuration;

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
                    Debug.Log("Touch lasted for: " + touchDuration + " seconds");

                    if (swipeDirection.magnitude < minSwipeDist)
                    {
                        Debug.Log("Touch Shel halavi!");
                        // VERY SMALL TOOL
                        return;
                    }

                   

                    // Detect direction dude (\L/)00(\R/)
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // horizontal
                        if (swipeDirection.x > 0)
                        {
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {
                        //vertical
                        if (swipeDirection.y > 0)
                        {
                            Debug.Log("Up Swipe");
                        }
                        else
                        {
                            Debug.Log("Down Swipe");
                        }
                    }
                    break;
            }
        }
    }
   
}


