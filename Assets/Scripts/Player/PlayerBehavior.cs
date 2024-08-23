using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public int coins = 0;
    public bool IsAlive = true;
    public int ExperiencePoints = 0;
    

    [Header("Local Fields")]
    [SerializeField] ObjectPoolManager coinPool;
    [SerializeField] float moveSpeed = 5;
    private int _currentLevel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinPool.ReleaseObject(other.gameObject);
            coins++;
        }
    }

    #region << Dying Methods >> 
    private void Die()
    {
        IsAlive = false;
        // Die Animtaion 
        // Move to next scene 
    }

    // This method will be called by an animation event on the player's Grabbed animation
    #endregion
}
