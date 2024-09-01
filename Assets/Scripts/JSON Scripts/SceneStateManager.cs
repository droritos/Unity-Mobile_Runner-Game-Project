using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

[System.Serializable]
public class SceneState
{
    public List<GameObjectState> gameObjects = new List<GameObjectState>();
    public string sceneName;
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
    public string jsonData;
}

public class SceneStateManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSceneState(string filePath)
    {
        SceneState sceneState = new SceneState();
        sceneState.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.name == sceneState.sceneName)  // Only save objects in the current scene
            {
                GameObjectState objState = new GameObjectState
                {
                    name = obj.name,
                    position = obj.transform.position,
                    rotation = obj.transform.rotation
                };

                // Serialize custom data from components
                foreach (var component in obj.GetComponents<Component>())
                {
                    string componentData = SerializeComponent(component);
                    if (!string.IsNullOrEmpty(componentData))
                    {
                        ComponentState compState = new ComponentState
                        {
                            type = component.GetType().ToString(),
                            jsonData = componentData
                        };
                        objState.components.Add(compState);
                    }
                }

                sceneState.gameObjects.Add(objState);
            }
        }

        string json = JsonUtility.ToJson(sceneState, true);
        filePath = Path.Combine(Application.persistentDataPath, filePath);
        File.WriteAllText(filePath, json);
        Debug.Log("Scene state saved to " + filePath);
    }

    private string SerializeComponent(Component component)
    {
        // Custom serialization logic depending on the component type
        if (component is Transform)
        {
            Transform transform = component as Transform;
            TransformState state = new TransformState
            {
                position = transform.position,
                rotation = transform.rotation,
                scale = transform.localScale
            };
            return JsonUtility.ToJson(state);
        }

        // Add cases for other components if needed, e.g., Rigidbody, custom scripts, etc.
        return null;
    }

    public void LoadSceneState(string filePath)
    {
        filePath = Path.Combine(Application.persistentDataPath, filePath);

        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file not found at " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        SceneState sceneState = JsonUtility.FromJson<SceneState>(json);

        // Load the saved scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneState.sceneName);

        StartCoroutine(RestoreSceneObjects(sceneState));
    }

    private IEnumerator RestoreSceneObjects(SceneState sceneState)
    {
        // Wait for the scene to load
        yield return null;

        foreach (var objState in sceneState.gameObjects)
        {
            GameObject obj = new GameObject(objState.name);
            obj.transform.position = objState.position;
            obj.transform.rotation = objState.rotation;

            foreach (var compState in objState.components)
            {
                System.Type type = System.Type.GetType(compState.type);
                if (type != null)
                {
                    Component component = obj.AddComponent(type);
                    JsonUtility.FromJsonOverwrite(compState.jsonData, component);
                }
                else
                {
                    Debug.LogWarning($"Component type {compState.type} not found.");
                }
            }
        }

        Debug.Log("Scene state restored from " + sceneState.sceneName);
    }

    [System.Serializable]
    public class TransformState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}
