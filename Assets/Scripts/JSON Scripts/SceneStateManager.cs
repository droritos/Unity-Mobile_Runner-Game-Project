

using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class SceneState
{
    public List<GameObjectState> gameObjects = new List<GameObjectState>();
}

[System.Serializable]
public class GameObjectState
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public List<ComponentState> components = new List<ComponentState>();
}

[System.Serializable]
public class ComponentState
{
    public string type;
    public string json;
}

public class SceneStateManager : MonoBehaviour
{
    public void SaveSceneState(string filePath)
    {
        SceneState sceneState = new SceneState();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            GameObjectState objState = new GameObjectState
            {
                name = obj.name,
                position = obj.transform.position,
                rotation = obj.transform.rotation
            };

            // Serialize all components
            foreach (var component in obj.GetComponents<Component>())
            {
                ComponentState compState = new ComponentState
                {
                    type = component.GetType().ToString(),
                    json = JsonUtility.ToJson(component)
                };
                objState.components.Add(compState);
            }

            sceneState.gameObjects.Add(objState);
        }

        string json = JsonUtility.ToJson(sceneState, true);
        filePath = Path.Combine(Application.persistentDataPath, filePath);
        File.WriteAllText(filePath, json);
        Debug.Log("Scene state saved to " + filePath);
    }
}