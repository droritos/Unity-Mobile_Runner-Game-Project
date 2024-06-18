using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSystem : MonoBehaviour
{
    
    Vector2 startPos;
    float minSwipeDist = 30; // Minimum Ssize of swipe
    float startTime;
    float touchDuration;
    [SerializeField] GameObject playerPrefab;

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
                            if (playerPrefab.transform.position.x >= -0.5f)
                            {
                                // Move the player to the left
                                playerPrefab.transform.Translate(new Vector3(-1f, 0, 0));

                            }
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            if (playerPrefab.transform.position.x <= 1f)
                            {
                                // Move the player to the right
                                playerPrefab.transform.Translate(new Vector3(1f, 0, 0));

                            }
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {
                        //vertical
                        if (swipeDirection.y > 0)
                        {
                            if (playerPrefab.transform.position.y <= 1f)
                            {
                                // Move the player to the right
                                playerPrefab.transform.Translate(new Vector3(0, 1f, 0));

                            }
                            Debug.Log("Up Swipe");
                        }
                        else
                        {
                            if (playerPrefab.transform.position.y >= 1f)
                            {
                                // Move the player to the right
                                playerPrefab.transform.Translate(new Vector3(0, -1f, 0));

                            }
                            Debug.Log("Down Swipe");
                        }
                    }
                    break;
            }
        }
    }
   
}


