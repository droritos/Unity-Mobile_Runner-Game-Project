using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class UpgradeMenuAnimator : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup menuCanvasGroup; // Drag the main Panel 
    [SerializeField] private Transform containerTransform; // Drag the Panel (for scaling)

    [Header("Settings")]
    [SerializeField] private float popDuration = 0.4f;
    [SerializeField] private float cardDelay = 0.1f; // Delay between each card appearing

    private Sequence currentSequence;

    // REMOVE the old OnEnable() if you have it, so it doesn't auto-play incorrectly.
    // private void OnEnable() { PlayOpenAnimation(); } <--- DELETE THIS

    // Update the function to accept the list of cards
    public void PlayOpenAnimation(List<Transform> cardsToAnimate)
    {
        // 1. Safety Kill
        if (currentSequence != null) currentSequence.Kill();
        
        // 2. Reset the Menu Container (The Parent)
        menuCanvasGroup.alpha = 0;
        containerTransform.localScale = Vector3.zero;
        
        // 3. Reset ONLY the cards we are about to show
        foreach (var card in cardsToAnimate)
        {
            card.localScale = Vector3.zero; 
        }

        // 4. Build Sequence
        currentSequence = DOTween.Sequence();
        currentSequence.SetUpdate(true); // Ignore Time.timeScale

        // Pop the Main Container
        currentSequence.Append(containerTransform.DOScale(1f, popDuration).SetEase(Ease.OutBack));
        currentSequence.Join(menuCanvasGroup.DOFade(1f, popDuration * 0.5f));

        // Pop the specific Cards
        for (int i = 0; i < cardsToAnimate.Count; i++)
        {
            Transform card = cardsToAnimate[i];
            float startTime = (popDuration * 0.7f) + (i * cardDelay);
            
            // Note: We use Insert so they overlap with the container pop nicely
            currentSequence.Insert(startTime, card.DOScale(1f, 0.3f).SetEase(Ease.OutBack));
        }
    }

    public void CloseMenu()
    {
        // Simple scale down when closing
        containerTransform.transform.DOScale(0f, 0.2f)
            .SetUpdate(true)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                containerTransform.gameObject.SetActive(false);
                // Resume game logic here if needed
            });

        menuCanvasGroup.DOFade(0f, popDuration * 0.5f);
    }
}