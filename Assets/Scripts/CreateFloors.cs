using UnityEngine;

public class CreateFloors : MonoBehaviour
{
    [SerializeField] float timerToCreate = 1f;
    [SerializeField] GameObject floorPrefab;

    private float elapsedTime = 0f;

    void Update()
    {
        // Update the elapsed time with the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // Check if the elapsed time has reached or exceeded the timer interval
        if (elapsedTime >= timerToCreate)
        {
            // Call the method to create the floor
            CreateFloor();

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }

    private void CreateFloor()
    {
        // Instantiate the floor prefab at the current position with no rotation and as a child of this GameObject
        Instantiate(floorPrefab, transform.position, Quaternion.identity, transform);
    }
}
