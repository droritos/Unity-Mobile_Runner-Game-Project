using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        [field: SerializeField] public PlayerBehavior PlayerBehavior { get; private set; }
        [field: SerializeField] public PlayerCombatController PlayerCombatController { get; private set; }
        [field: SerializeField] public PlayerVisuals PlayerVisuals { get; private set; }
        
        public Animator MyAnimator => PlayerVisuals.Animator;
    }
