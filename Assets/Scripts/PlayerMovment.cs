using UnityEngine;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    public GameObject PlayerPrefab;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime;
    [SerializeField] float duration = 0.5f;
    private bool _isMoving;

    private void Start()
    {
        _startPosition = PlayerPrefab.transform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            PlayerPrefab.transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            if (t >= 1.0f)
            {
                _isMoving = false;
                _elapsedTime = 0;
            }
        }
    }

    public void CheckLeft()
    {
        if (PlayerPrefab.transform.position.x >= -0.5f && !_isMoving)
        {
            _startPosition = PlayerPrefab.transform.position;
            _targetPosition = new Vector3(PlayerPrefab.transform.position.x - 1f, PlayerPrefab.transform.position.y, PlayerPrefab.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Moving Left");
    }

    public void CheckRight()
    {
        if (PlayerPrefab.transform.position.x <= 1f && !_isMoving)
        {
            _startPosition = PlayerPrefab.transform.position;
            _targetPosition = new Vector3(PlayerPrefab.transform.position.x + 1f, PlayerPrefab.transform.position.y, PlayerPrefab.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Moving Right");
    }

    public void CheckJump()
    {
        if (PlayerPrefab.transform.position.y <= 1f && !_isMoving)
        {
            _startPosition = PlayerPrefab.transform.position;
            _targetPosition = new Vector3(PlayerPrefab.transform.position.x, PlayerPrefab.transform.position.y + 1f, PlayerPrefab.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Jumping");
    }

    public void CheckSlide()
    {
        if (PlayerPrefab.transform.position.y > -0.03999996f && !_isMoving)
        {
            _startPosition = PlayerPrefab.transform.position;
            _targetPosition = new Vector3(PlayerPrefab.transform.position.x, PlayerPrefab.transform.position.y - 1f, PlayerPrefab.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Sliding");
    }
}
