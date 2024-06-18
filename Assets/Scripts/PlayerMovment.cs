using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    public void CheckLeft()
    {
        if (playerPrefab.transform.position.x >= -0.5f)
        {
            // Move the player to the left
            playerPrefab.transform.Translate(new Vector3(-1f, 0, 0));
        }
        Debug.Log("Player Moving Left");
    }

    public void CheckRight()
    {
        if (playerPrefab.transform.position.x <= 1f)
        {
            // Move the player to the right
            playerPrefab.transform.Translate(new Vector3(1f, 0, 0));
        }
        Debug.Log("Player Moving Right");
    }

    public void CheckJump()
    {
        if (playerPrefab.transform.position.y <= 1f)
        {
            // Move the player to the right
            playerPrefab.transform.Translate(new Vector3(0, 1f, 0));
        }
        Debug.Log("Player Jumping");
    }

    public void CheckSlide()
    {
        if (playerPrefab.transform.position.y > -0.03999996f)
        {
            // Move the player to the left
            playerPrefab.transform.Translate(new Vector3(0, -1f, 0));
        }
        Debug.Log("Player Sliding");
    }
}
