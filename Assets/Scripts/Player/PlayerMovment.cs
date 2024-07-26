using UnityEngine;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime;
    [SerializeField] float duration = 0.5f;
    private bool _isMoving;
    [SerializeField] float horizontalOffset = 1f;
    [SerializeField] float horizontalMoveRange = 1.6f;
    [SerializeField] float verticalOffset;
    [SerializeField] float verticallMoveRange;

    private void Start()
    {
        _startPosition = this.transform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            this.transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            if (t >= 1.0f)
            {
                _isMoving = false;
                _elapsedTime = 0;
            }
        }
    }

    public void MoveRight()
    {
        if (this.transform.position.x >= -horizontalOffset && !_isMoving)
        {
            _startPosition = this.transform.position;
            _targetPosition = new Vector3(this.transform.position.x - horizontalMoveRange, this.transform.position.y, this.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Moving Left");
    }

    public void MoveLeft()
    {
        if (this.transform.position.x <= horizontalOffset && !_isMoving)
        {
            _startPosition = this.transform.position;
            _targetPosition = new Vector3(this.transform.position.x + horizontalMoveRange, this.transform.position.y, this.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Moving Right");
    }

    public void CheckJump()
    {
        if (this.transform.position.y <= 1f && !_isMoving)
        {
            _startPosition = this.transform.position;
            _targetPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Jumping");
    }

    public void CheckSlide()
    {
        if (this.transform.position.y > -0.03999996f && !_isMoving)
        {
            _startPosition = this.transform.position;
            _targetPosition = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
            _isMoving = true;
        }
        Debug.Log("Player Sliding");
    }
}
