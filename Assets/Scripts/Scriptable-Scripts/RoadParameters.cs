using UnityEngine;

[CreateAssetMenu(fileName = "RoadParameters" , menuName = "ScriptableObject/Road")] // You can now Create new Road file under "ScriptableObject"
                                                                                    // "CreateAssetMenu" gives you that option
public class RoadParameters : ScriptableObject // Note : Inhireted from ScriptableObject and not MonoBehaivor !!
{
    public float Speed = 1.0f;
    public float objectDistance = -40;
    public float despawnDistance = -110f;


    private void OnValidate()
    {
        if (Speed < 8.5f)
        {
            Speed = 8.5f;
        }
    }
}
