using UnityEngine;

[CreateAssetMenu(fileName = "MovingObjectsSO", menuName = "ScriptableObject/ObjectsConfig")] // You can now Create new Road file under "ScriptableObject"
                                                                                    // "CreateAssetMenu" gives you that option
public class MovingObjectsConfig : ScriptableObject // Note : Inhireted from ScriptableObject and not MonoBehaivor !!
{
    [Header("Distances")]
    public float objectDistance = -40;
    public float despawnDistance = -110f;
    
    [Header("Speed")]
    public float CarRoadSpeed = 1.0f;
    public float SideWalkSpeed = 1.0f;
    public float CobwebSpeed = 8;
    public float EnemyProjectileSpeed = 8;

    private void OnValidate()
    {
        if (CarRoadSpeed < 8.5f)
        {
            CarRoadSpeed = 8.5f;
        }
    }
}
