using UnityEngine;
using UnityEngine.UI;

public class CameraCaptureFromImage : MonoBehaviour
{
    public RawImage displayImage;
    private WebCamTexture _webCamTexture;

        private void Start()
    {
        _webCamTexture = new WebCamTexture();

        displayImage.texture = _webCamTexture;

        _webCamTexture.Play();
    }

    //private void OnDestroy()
    //{
    //    if (_webCamTexture != null && _webCamTexture.isPlaying)
    //    {
    //        //_webCamTexture.Stop();
    //    }
    //}
}
