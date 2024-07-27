using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [Header("Public Data")]
    [HideInInspector] public Animator Animator;

    [Header("Serialize Data")]
    [SerializeField] float duration = 0.5f;
    [SerializeField] float horizontalOffset = 1f;
    [SerializeField] float horizontalMoveRange = 1.6f;
    [SerializeField] float verticalOffset;
    [SerializeField] float verticalMoveRange;
    
    [Header("Private Data")]
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private Vector3 _startBoxPosition;
    private float _elapsedTime;
    private bool _isMoving;
    private BoxCollider _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        Animator = GetComponent<Animator>();
        _startPosition = this.transform.position;
        _startBoxPosition = _boxCollider.center;
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

    public void Jump()
    {
        if (!_isMoving)
        {
            _boxCollider.center *= 2;
            Animator.SetTrigger("Jumping");
            StartCoroutine(WaitForAnimationToFinish());
        }
        Debug.Log("Player Jumping");
    }

    public void Slide()
    {
        if (!_isMoving)
        {
            _boxCollider.center /= 2;
            Animator.SetTrigger("Sliding");
            StartCoroutine(WaitForAnimationToFinish());
        }
        Debug.Log("Player Sliding");
    }
    private IEnumerator WaitForAnimationToFinish()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

        // Move the player collison box to deafult
        while (_boxCollider.center != _startBoxPosition)
        {
            _boxCollider.center = _startBoxPosition;
            yield return null;
        }
    }
}
