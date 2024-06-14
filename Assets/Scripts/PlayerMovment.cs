using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckRight();
        CheckLeft();
    }

    public void CheckLeft()
    {
        if (Input.GetMouseButton(0) && transform.position.x >= -1.5f)
        {
            // Move the player to the left
            transform.Translate(new Vector3(-0.3f, 0, 0));
            Debug.Log("LeftMoush key was pressed");
        }
    }

    private void CheckRight()
    {
        if (Input.GetMouseButton(1) && transform.position.x <= 1.5f)
        {
            // Move the player to the right
            transform.Translate(new Vector3(6f, 0, 0));
            Debug.Log("RightMouse key was pressed");
        }
    }
}
