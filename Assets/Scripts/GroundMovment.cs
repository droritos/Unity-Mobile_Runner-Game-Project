using UnityEngine;

public class MoveGround : MonoBehaviour
{
    private float _speed;
    Transform spawnFloorPosition;
    private void Awake()
    {
        spawnFloorPosition = GameObject.Find("SpawnFloors").transform;
    }
    void Update()
    {
        MoveFloor();
        UpdateMoveSpeed();
    }

    private void MoveFloor()
    {
        // Move the ground backwards along the Z axis
        transform.Translate(_speed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.position = spawnFloorPosition.transform.position;
        }
    }
    private void UpdateMoveSpeed()
    {
        _speed = GameManager.Instance.SetFloorSpeed();
    }
}
