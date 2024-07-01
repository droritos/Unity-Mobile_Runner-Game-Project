using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] RoadParameters floorConfig;
    private bool _canSpawnGround = true;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MoveFloor();
    }

    private void MoveFloor()
    {
        transform.position += floorConfig.Speed * Time.deltaTime * Vector3.back;

        if (transform.position.z <= floorConfig.objectDistance && transform.tag == "Ground" && _canSpawnGround)
        {
            FloorSpawner.Instance.SpawnGround();
            _canSpawnGround = false;
        }

        if (transform.position.z <= floorConfig.despawnDistance)
        {
            _canSpawnGround = true;
            gameObject.SetActive(false);
        }
    }
}
