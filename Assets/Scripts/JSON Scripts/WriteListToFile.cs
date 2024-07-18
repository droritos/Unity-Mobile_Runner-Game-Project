using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WriteListToFile : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public string playerName;
        public int playerAge;
        public string photoPath; // Path to the saved photo
    }

    [System.Serializable]
    public class PlayerListWrapper
    {
        public List<Player> players = new List<Player>();
    }

    private PlayerListWrapper playerListWrapper = new PlayerListWrapper();

    public void SavePlayerData(string playerName, int playerAge, string photoPath)
    {
        Player newPlayer = new Player
        {
            playerName = playerName,
            playerAge = playerAge,
            photoPath = Path.Combine(Application.persistentDataPath, photoPath)
        };

        playerListWrapper.players.Add(newPlayer);
        string jsonData = JsonUtility.ToJson(playerListWrapper);

        string filePath = Path.Combine(Application.persistentDataPath, "playersData.json");
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Player data saved to: " + filePath);
    }
}
