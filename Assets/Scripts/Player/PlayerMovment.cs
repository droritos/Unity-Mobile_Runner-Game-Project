using System;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [Header("Public Data")]
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider boxCollider;

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
    private void Start()
    {
        _startPosition = this.transform.position;
        _startBoxPosition = boxCollider.center;
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
        //Debug.Log("Player Moving Left");
    }

    public void MoveLeft()
    {
        if (this.transform.position.x <= horizontalOffset && !_isMoving)
        {
            _startPosition = this.transform.position;
            _targetPosition = new Vector3(this.transform.position.x + horizontalMoveRange, this.transform.position.y, this.transform.position.z);
            _isMoving = true;
        }
        //Debug.Log("Player Moving Right");
    }
    private IEnumerator WaitForAnimationToFinish()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Move the player collison box to deafult
        while (boxCollider.center != _startBoxPosition)
        {
            boxCollider.center = _startBoxPosition;
            yield return null;
        }
    }

    private void OnValidate()
    {
        if(!boxCollider)
            boxCollider = GetComponent<BoxCollider>();
        if(!animator)
            animator = GetComponent<Animator>();
    }
}
