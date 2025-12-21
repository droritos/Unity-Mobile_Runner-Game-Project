using System.Collections;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [field:SerializeField] public Animator Animator {get;private set;}
    
    public void Shoot()
    {
        Animator.SetLayerWeight(1, 1);
        Animator.SetTrigger("Attacking");
    }

    public float GetCurrentAnimationLength() => Animator.GetCurrentAnimatorStateInfo(0).length;

}
