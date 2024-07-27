using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpidyMorals : MonoBehaviour
{

    [Header("Public Data")]
    [HideInInspector] public bool IsGrabbing = false;
    
    [Header("Serialize Data")]
    [SerializeField] Transform[] cobwebSpawnPoint;
    [SerializeField] GameObject cobweb;
    [SerializeField] Transform playerTarget;

    [Header("Numbers Data")]

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int fireColdown = 3;
    [SerializeField] float maxColdown = 2.5f;

    [Header("Private Data")]
    private Animator _animator;
    private float _fire = 0;
    private float moveColdwon;
    private float _move = 0;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _fire += Time.deltaTime;
        _move += Time.deltaTime;
        //Shoot();
        Chase();
    }

    private void Shoot()
    {
        if (_fire >= fireColdown && !IsGrabbing)
        {
            _animator.SetTrigger("Attacking");
            StartCoroutine(WaitForShoot());
            _fire = 0;
        }
    }

    private void Chase()
    {
        if (_move >= 2)
        {
            Debug.Log("Chasing");
            Vector3 tragetX = new Vector3(playerTarget.position.x, transform.position.y, transform.position.z);
            if (playerTarget.position.x != transform.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, tragetX, moveSpeed ); //*Time.deltaTime
            }
                _move = 0;
        }
    }

    private float RandomMoveColdown()
    {
        return moveColdwon = Random.Range(0.5f, maxColdown);
    }

    private IEnumerator WaitForShoot() 
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Instantiate(cobweb, cobwebSpawnPoint[0].position, cobweb.transform.rotation);
    }  // Instaite Cobweb after finish Animation

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
        PlayerMovement.Instance.Animator.SetTrigger("Grabbed");
    }
    #endregion
}
