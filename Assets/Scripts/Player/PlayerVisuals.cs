using System.Collections;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [field:SerializeField] public Animator Animator {get;private set;}
    [SerializeField] private ParticleSystem currencyCollectedVFX;
    [SerializeField] private Transform vfxSpawnPoint;
    
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

}
