using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVisuals : MonoBehaviour , IPausable
{
    [field:SerializeField] public Animator Animator {get;private set;}
    [SerializeField] private ParticleSystem currencyCollectedVFX;
    [SerializeField] private Transform vfxSpawnPoint;
    
    private int _speedAnimation = Animator.StringToHash("Speed");
    private int _die = Animator.StringToHash("Die");

    private void Start()
    {
        PauseManager.Instance.Register(this);
        Animator.SetFloat(_speedAnimation, 1f);

        //GameManager.Instance.Player.OnDeath += ApplyDieAnimation;
    }

    private void OnDestroy()
    {
        PauseManager.Instance.Unregister(this);
        //GameManager.Instance.Player.OnDeath -= ApplyDieAnimation;
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

    public void InvokeGameOver()
    {
        SceneManager.LoadScene(3); // GameOver
    }

    private void ApplyDieAnimation()
    {
        // Do Vfx
        Animator.SetLayerWeight(1, 0);
        Animator.SetTrigger(_die);        
    }
}
