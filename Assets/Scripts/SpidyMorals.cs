using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidyMorals : MonoBehaviour
{
    [Header("Public Data")]
    [HideInInspector] public bool IsGrabbing = false;

    [Header("Serialize Data")]
    [SerializeField] Transform[] cobwebSpawnPoint;
    [SerializeField] GameObject cobweb;
    [SerializeField] Transform player;

    [Header("Numbers Data")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int fireCooldown = 3;
    [SerializeField] float maxCooldown = 2.5f;

    [Header("Private Data")]
    private Animator _animator;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _fire = 0;
    private float _lerpTime = 0;

    [Header("Moving Data")]
    private float _move = 0;
    private float _randomMove = 2;
    private bool _isMoving = false;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _fire += Time.deltaTime;
        _move += Time.deltaTime;
        Shoot();
        Chase();
    }

    private void Shoot()
    {
        if (_fire >= fireCooldown && !IsGrabbing && !_isMoving)
        {
            _animator.SetTrigger("Attacking");
            StartCoroutine(WaitForShoot());
            _fire = 0;
        }
    }

    private void Chase()
    {
        _isMoving = true;
        if (_move >= _randomMove)
        {
            Debug.Log("Chasing");

            // Set the start and target positions
            _startPosition = transform.position;
            _targetPosition = new Vector3(GetPlayerLane(), transform.position.y, transform.position.z);
            _lerpTime = 0;

            // Reset move counter
            _move = 0f; _isMoving = false;
            _randomMove = RandomMoveCooldown();
        }

        // Perform the lerp operation if the target is set
        if (_targetPosition != Vector3.zero)
        {
            _lerpTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _lerpTime);

            // Check if we've reached the target position
            if (_lerpTime >= 1f)
            {
                _targetPosition = Vector3.zero; // Reset target position to stop lerping
            }
        }
    }

    private float RandomMoveCooldown()
    {
        float random = Random.Range(0.5f, maxCooldown);
        //Debug.Log($"The Ramdom Value {random}");
        return random;
    }

    private IEnumerator WaitForShoot()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Instantiate(cobweb, cobwebSpawnPoint[0].position, cobweb.transform.rotation);
    }

    private float GetPlayerLane()
    {
        float[] lane = new float[3];

        if (player.position.x > 0)
        {
            lane[0] = 1.6f;
            return lane[0];
        }
        else if (player.position.x < 0)
        {
            lane[1] = -1.6f;
            return lane[1];
        }
        else
        {
            lane[2] = 0f;
            return lane[2];
        }
    }

    #region << Animation Methods >>
    public void Grab()
    {
        IsGrabbing = true;
        _animator.SetTrigger("Grabing");
        StartCoroutine(WaitForGrab());
    }

    public void Slam()
    {
        if (IsGrabbing)
        {
            _animator.SetTrigger("Slamming");
        }
    }

    private IEnumerator WaitForGrab()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        AlignPlayerWithChaser();
        PlayerMovement.Instance.Animator.SetTrigger("Grabbed");
    }

    private void AlignPlayerWithChaser()
    {
        // Assuming you want to align the player's position to the chaser
        PlayerMovement.Instance.transform.position = transform.position + new Vector3(0, 0, 1); // Adjust the offset as needed
        PlayerMovement.Instance.transform.rotation = transform.rotation; // Optionally align rotation
    }
    #endregion
}
