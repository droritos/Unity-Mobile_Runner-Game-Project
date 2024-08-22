using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int coins = 0;
    public bool IsAlive = true;
    [SerializeField] ObjectPoolManager coinPool;
    [SerializeField] SpidyMorals chaser;
    [SerializeField] float moveSpeed = 5;
    private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinPool.ReleaseObject(other.gameObject);
            coins++;
        }
        //else if (other.CompareTag("Web"))
        //{
        //    Destroy(other.gameObject);
        //    //Die();
        //}
    }

    #region << Dying Methods >> 
    private void Die()
    {
        IsAlive = false;
        StartCoroutine(MoveTowardsChaser());
        chaser.Grab();
    }
    private IEnumerator MoveTowardsChaser()
    {
        chaser.IsGrabbing = true;
        // Continue moving towards the chaser while the distance is greater than a small threshold
        while (Vector3.Distance(transform.position, chaser.transform.position) > 0.1f)
        {
            // Move towards the chaser
            transform.position = Vector3.MoveTowards(transform.position, chaser.transform.position, moveSpeed * Time.deltaTime);
            yield return null;  // Wait for the next frame
        }
        AlignPlayerWithChaser();
        chaser.Slam();
    }
    private void AlignPlayerWithChaser()
    {
        // Set the player's position relative to the chaser
        transform.position = chaser.transform.position + new Vector3(0, 0, 1);  // Adjust the offset as needed
        transform.rotation = chaser.transform.rotation;  // Optionally align rotation
    }

    // This method will be called by an animation event on the player's Grabbed animation
    public void OnGrabbedAnimationEvent()
    {
        // Keep the player aligned with the chaser
        AlignPlayerWithChaser();
    }
    #endregion
}
