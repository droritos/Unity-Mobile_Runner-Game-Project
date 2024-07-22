using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] RoadParameters roadParameters;
    [SerializeField] ReadListFromFile readListFromFile;

    public float SetFloorSpeed()
    {
        roadParameters.Speed = floorSpeed;
        return floorSpeed;
    }

    private void Start()
    {
        readListFromFile.LoadPlayerData();
    }
}
