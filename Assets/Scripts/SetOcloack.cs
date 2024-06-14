using UnityEngine;

public class SetOcloack : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Change rotation on the Z axis
            RotateObject();
        }
    }

    private void RotateObject()
    {
        // Define the rotation amount
        float rotationSpeed = 100f; // Adjust the speed as needed

        // Calculate the rotation for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Apply the rotation to the object
        transform.Rotate(rotationAmount, 0, 0);
    }
}
