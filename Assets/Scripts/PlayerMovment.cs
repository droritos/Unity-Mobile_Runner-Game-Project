using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    // Update is called once per frame
    void Update()
    {
        CheckRight();
        CheckLeft();
    }

    public void CheckLeft()
    {
        if (playerPrefab.transform.position.x >= -0.5f)
        {
            // Move the player to the left
            playerPrefab.transform.Translate(new Vector3(-1f, 0, 0));
        }
        Debug.Log("LeftMoush key was pressed");
    }

    public void CheckRight()
    {
        if (playerPrefab.transform.position.x <= 1f)
        {
            // Move the player to the right
            playerPrefab.transform.Translate(new Vector3(1f, 0, 0));
        }
        Debug.Log("RightMouse key was pressed");
    }
}
