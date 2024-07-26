using UnityEngine;
using UnityEngine.UI;
using static NativeCamera;

public class CameraController : MonoBehaviour
{
    // Reference to a UI Image to display the taken picture
    public RawImage displayImage;

    // Method to be called when the user wants to take a picture
    public void TakePicture()
    {
        // Call NativeCamera.TakePicture with a callback
        NativeCamera.TakePicture(OnPictureTaken, maxSize: 1024, preferredCamera: PreferredCamera.Default);
    }

    // Callback method that will be called when the picture is taken
    private void OnPictureTaken(string path)
    {
        // Check if a valid path is returned
        if (path != null)
        {
            Debug.Log("Picture saved to: " + path);

            // Load the image from the path and display it in the UI
            Texture2D texture = NativeCamera.LoadImageAtPath(path, 1024, false);
            if (texture != null)
            {
                displayImage.texture = texture;
                displayImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Couldn't load texture from " + path);
            }
        }
        else
        {
            Debug.LogError("Picture taking cancelled or failed.");
        }
    }
}