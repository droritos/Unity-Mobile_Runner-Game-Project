using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the ground parent
        if (other.CompareTag("GroundParent"))
        {
            // Loop through all children of the ground parent and destroy them
            foreach (Transform child in other.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
