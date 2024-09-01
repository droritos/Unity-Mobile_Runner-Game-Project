using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SceneStateLoader : MonoBehaviour
{
    public void LoadSceneState(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found at " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        SceneState sceneState = JsonUtility.FromJson<SceneState>(json);

        // Clear existing objects if needed
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            Destroy(obj);
        }

        // Recreate objects
        foreach (var objState in sceneState.gameObjects)
        {
            GameObject newObj = new GameObject(objState.name);
            newObj.transform.position = objState.position;
            newObj.transform.rotation = objState.rotation;

            // Attach components
            foreach (var compState in objState.components)
            {
                System.Type type = System.Type.GetType(compState.type);
                if (type != null)
                {
                    Component component = newObj.AddComponent(type);
                }
            }
        }
        Debug.Log("Scene state loaded from " + filePath);
    }
}