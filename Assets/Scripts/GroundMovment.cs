using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] RoadParameters roadParameters;
    Transform floorsParent;
    private void Awake()
    {
        floorsParent = GameObject.Find("SpawnFloors").transform;
    }
    void Update()
    {
        MoveFloor();
    }

    private void MoveFloor()
    {
        // Move the ground backwards along the Z axis
        transform.Translate(roadParameters.Speed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.position = floorsParent.transform.position;
        }
    }
}
