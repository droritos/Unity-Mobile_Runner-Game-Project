using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour , IPausable
{
    [field:SerializeField] public Animator Animator {get;private set;}
    [SerializeField] private ParticleSystem currencyCollectedVFX;
    [SerializeField] private Transform vfxSpawnPoint;
    
    private int _speedAnimation = Animator.StringToHash("Speed");

    private void Start()
    {
        PauseManager.Instance.Register(this);
        Animator.SetFloat(_speedAnimation, 1f);
    }

    private void OnDestroy()
    {
        PauseManager.Instance.Unregister(this);
    }

    public void Shoot()
    {
        Animator.SetLayerWeight(1, 1);
        Animator.SetTrigger("Attacking");
    }

    public float GetCurrentAnimationLength() => Animator.GetCurrentAnimatorStateInfo(0).length;

    public void ApplyCurrencyCollectedVFX(Vector3 position = default)
    {
        if(position == default) position = vfxSpawnPoint.position;
        
        Instantiate(currencyCollectedVFX,position,Quaternion.identity);
    }

    public void SetPaused(bool paused)
    {
        if(paused)
            Animator.SetFloat(_speedAnimation, 0.49f); 
        else
            Animator.SetFloat(_speedAnimation, 1f);
    }
}
