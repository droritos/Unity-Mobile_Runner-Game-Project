using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Interfaces;

namespace UI_UX_Canvas
{
    public class AbilityButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPausable
    {
        [Header("Visuals")]
        [SerializeField] Image fillRing;
        [SerializeField] PlayerStatsConfig playerStatsConfig;
        [SerializeField] private Color color = Color.cyan; // Default to Cyan if unset

        [Header("Juice Settings")]
        [SerializeField] float pressScale = 0.9f;
        [SerializeField] float punchStrength = 0.2f;
        [SerializeField] float shakeStrength = 15f; 

        private float _cooldownTime => playerStatsConfig.FireCooldown;

        [Header("Input Settings")]
        [SerializeField] float tapMaxTime = 0.2f;
        [SerializeField] float tapMaxDistance = 50f;

        [Header("References")]
        [SerializeField] PlayerCombatController playerAbilityScript;

        private float touchStartTime;
        private Vector2 touchStartPos;
        private bool isCooldown = false;
        private float cooldownTimer = 0f;
        private Vector3 initialScale;

        void Start()
        {
            PauseManager.Instance.Register(this);
            initialScale = transform.localScale;
            
            // FIX 1: Apply the chosen color immediately so it doesn't start White
            if (fillRing != null) fillRing.color = color;
        }

        private void OnDestroy()
        {
            if (PauseManager.Instance != null)
                PauseManager.Instance.Unregister(this);
        }

        public void SetPaused(bool paused)
        {
            // FIX 2: Reset visual state when unpausing
            // Prevents button from being stuck "shrunk" if you paused while holding it
            if (!paused) 
            {
                transform.localScale = initialScale; 
                if (fillRing != null) fillRing.color = color;
            }
            
            this.gameObject.SetActive(!paused);
        }

        void Update()
        {
            if (isCooldown)
            {
                // Note: Since gameObject is disabled on Pause, Update stops running.
                // This is good! Cooldown effectively "pauses" too.
                cooldownTimer -= Time.deltaTime;
            
                if (fillRing != null)
                    fillRing.fillAmount = 1 - (cooldownTimer / _cooldownTime);

                if (cooldownTimer <= 0)
                {
                    FinishCooldown();
                }
            }
        }

        private void FinishCooldown()
        {
            isCooldown = false;
            if (fillRing != null) fillRing.fillAmount = 1;

            // JUICE: Visual "Pop"
            transform.DOScale(initialScale * 1.1f, 0.1f).OnComplete(() => 
            {
                transform.DOScale(initialScale, 0.1f);
            });
            
            // JUICE: Flash White then fade to your chosen Color
            if (fillRing != null)
                fillRing.DOColor(color, 0.2f).From(Color.white);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill(); 

            if (isCooldown) 
            {
                // Shake indicates "Not Ready"
                transform.DOShakePosition(0.2f, new Vector3(shakeStrength, 0, 0), 20, 90);
                return; 
            }
            
            // Press down effect
            transform.DOScale(initialScale * pressScale, 0.1f).SetEase(Ease.OutQuad);

            touchStartTime = Time.time;
            touchStartPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isCooldown) return;

            // Reset Scale if we didn't fire (if we fire, the Punch effect handles scale)
            transform.DOScale(initialScale, 0.1f);

            float timePressed = Time.time - touchStartTime;
            float distMoved = Vector2.Distance(eventData.position, touchStartPos);

            if (timePressed < tapMaxTime && distMoved < tapMaxDistance)
            {
                Fire();
            }
        }

        void Fire()
        {
            isCooldown = true;
            cooldownTimer = _cooldownTime;
            if (fillRing != null) fillRing.fillAmount = 0;

            // JUICE: Strong Punch (Recoil)
            transform.DOKill();
            transform.localScale = initialScale; 
            transform.DOPunchScale(Vector3.one * punchStrength, 0.2f, 10, 1);

            if (playerAbilityScript != null)
            {
                playerAbilityScript.Shoot();
            }
        }
    }
}