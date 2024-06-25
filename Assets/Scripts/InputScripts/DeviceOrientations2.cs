using UnityEngine;
public class DeviceOrientations2 : MonoBehaviour
{
    void Update()
    {
        DeviceOrientation orientation = Input.deviceOrientation;
        switch (orientation)
        {
            case DeviceOrientation.Unknown:
                Debug.Log("Unkown");
                break;
            case DeviceOrientation.Portrait:
                Debug.Log("Portrait");
                break;
            case DeviceOrientation.PortraitUpsideDown:
                Debug.Log("Portrait Upside Down");
                break;
            case DeviceOrientation.LandscapeLeft:
                Debug.Log("Landscape Left");
                break;
            case DeviceOrientation.LandscapeRight:
                Debug.Log("Landscape Right");
                break;
            case DeviceOrientation.FaceUp:
                Debug.Log("Face Up");
                break;
            case DeviceOrientation.FaceDown:
                Debug.Log("Face Down");
                break;
        }
    }
}