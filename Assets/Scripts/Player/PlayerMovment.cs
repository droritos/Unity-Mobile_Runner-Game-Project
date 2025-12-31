using System;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    public bool IsMoving {get; private set;}
    
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
    private int _lane;

    private void Start()
    {
        _startPosition = this.transform.position;
        _startBoxPosition = boxCollider.center;
    }

    private void Update()
    {
        if (IsMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            this.transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            if (t >= 1.0f)
            {
                IsMoving = false;
                _elapsedTime = 0;
            }
        }
    }

    public void MoveLeft()
    {
        if (IsMoving) return;
        _lane = Mathf.Clamp(_lane + 1, -1, 1);
        StartLaneMove();
    }

    public void MoveRight()
    {
        if (IsMoving) return;
        _lane = Mathf.Clamp(_lane - 1, -1, 1);
        StartLaneMove();
    }

    private void StartLaneMove()
    {
        _startPosition = transform.position;
        _targetPosition = new Vector3(_lane * horizontalMoveRange, transform.position.y, transform.position.z);
        IsMoving = true;
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
