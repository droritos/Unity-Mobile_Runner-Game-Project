using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Make sure DOTween is imported

namespace Effects
{
    public class DamageFeedback : MonoBehaviour
    {
        [Header("Setup")]
        public Image flashImage;

        [Header("Settings")]
        [Range(0f, 1f)]
        public float maxAlpha = 0.8f; 

        // Changed from "Speed" to "Duration" (seconds)
        public float fadeInDuration = 0.1f;  // Fast impact
        public float fadeOutDuration = 0.5f; // Slower decay

        void Start()
        {
            // Initialize: Ensure alpha is 0
            if (flashImage != null)
            {
                flashImage.DOFade(0f, 0f); 
            }
        }

        public void TriggerDamageFlash()
        {
            if (flashImage == null) return;

            // 1. DOKill: Instantly stop any previous flash if damage is spammed
            flashImage.DOKill();

            // 2. Force alpha to 0 (optional, or let it ramp up from current if you prefer)
            // flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);

            // 3. Create a Sequence for better control
            Sequence flashSequence = DOTween.Sequence();

            // Step A: Fade IN fast (Linear is usually fine for impact, or OutQuad)
            flashSequence.Append(flashImage.DOFade(maxAlpha, fadeInDuration).SetEase(Ease.OutQuad));

            // Step B: Fade OUT slowly (InQuad makes it linger slightly then disappear)
            flashSequence.Append(flashImage.DOFade(0f, fadeOutDuration).SetEase(Ease.InQuad));
        }
    
        // Safety check: Kill tweens if object is destroyed to prevent errors
        void OnDestroy()
        {
            if (flashImage != null) flashImage.DOKill();
        }

        [ContextMenu("Test Flash")]
        public void TestFlash()
        {
            TriggerDamageFlash();
        }
    }
}