using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] RoadParameters roadParameters;

    public float SetFloorSpeed()
    {
        roadParameters.Speed = floorSpeed;
        return floorSpeed;
    }
}
