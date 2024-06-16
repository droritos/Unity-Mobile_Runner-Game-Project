using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RoadParameters roadParameters;
    [SerializeField] float floorSpeed;
    public static GameManager Instance; // The "Singleton Pattern" - Easy way to call the ScoreManger script from every where
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public float SetFloorSpeed()
    {
        roadParameters.Speed = floorSpeed;
        return floorSpeed;
    }
}
