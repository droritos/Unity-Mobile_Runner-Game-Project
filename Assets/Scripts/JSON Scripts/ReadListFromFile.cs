using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ReadListFromFile : MonoBehaviour
{
    public GameObject playerPhoto;
    public RawImage playerPhotoDisplay; // Reference to display the loaded photo

    public List<Player> LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playersData.json");
        string jsonData = ReadFromFile(filePath);

        if (!string.IsNullOrEmpty(jsonData))
        {
            PlayerListWrapper playerListWrapper = JsonUtility.FromJson<PlayerListWrapper>(jsonData);
            List<Player> players = playerListWrapper.players;

            foreach (Player player in players)
            {
                //GameObject newText = Instantiate(playerPhotoDisplay, RunnerFace.transform);
                //newText.GetComponent<TextMeshProUGUI>().text = player.playerName + " " + player.playerAge.ToString();

                // Load and display the photo
                if (File.Exists(player.photoPath))
                {
                    byte[] fileData = File.ReadAllBytes(player.photoPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    playerPhotoDisplay.texture = texture;
                }

                Debug.Log("Player Name: " + player.playerName + ", Player Age: " + player.playerAge);
            }
            return players;
        }
        return null;
    }

    string ReadFromFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Debug.Log("File read successfully from: " + path);
                return content;
            }
            else
            {
                Debug.LogWarning("File not found: " + path);
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to read from file: " + e.Message);
            return null;
        }
    }
}
[System.Serializable]
public class Player
{
    public string playerName;
    public int playerAge;
    public string photoPath;
}

[System.Serializable]
public class PlayerListWrapper
{
    public List<Player> players;
}
