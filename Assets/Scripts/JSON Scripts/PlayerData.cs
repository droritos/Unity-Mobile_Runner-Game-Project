using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_InputField playerAgeInput;
    public WriteListToFile writeListToFile; // Reference to the WriteListToFile script
    public ReadListFromFile readListFromFile; // Reference to the ReadListFromFile script
    public WebcamCapture webCamCapture; // Reference to the WebCamCapture script

    public void TakePhoto()
    {
        webCamCapture.CaptureAndSaveImage();
    }

    public void SaveProfile()
    {
        string playerName = playerNameInput.text;
        int playerAge = int.Parse(playerAgeInput.text);
        writeListToFile.SavePlayerData(playerName, playerAge, "capturedImage.png");
    }

    public void LoadProfile()
    {
        readListFromFile.LoadPlayerData();
    }
}
