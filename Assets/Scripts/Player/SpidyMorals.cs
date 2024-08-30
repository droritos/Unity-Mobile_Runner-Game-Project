using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidyMorals : MonoBehaviour
{
    [Header("Public Data")]
    [SerializeField] Transform[] CobwebSpawnPoint;
    public ObjectPoolManager CobwebPoolScript;

    [Header("Numbers Data")]
    public float FireCooldown = 2f;

    [Header("Private Data")]
    private Animator _animator;
    private float _fire = 0;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _fire += Time.deltaTime;
        AutoShoot();
    }
    private void AutoShoot()
    {
        if (_fire >= FireCooldown)
        {
            _animator.SetTrigger("Attacking");
            StartCoroutine(WaitForShoot());
            _fire = 0;
        }
    }
    private IEnumerator WaitForShoot()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        // Attempt to get an object from the pool
        GameObject web = CobwebPoolScript.GetObject();
        //cobwebPoolScript.ActiveObjects.Enqueue(web);
        web.transform.position = CobwebSpawnPoint[0].position;
    }
}
