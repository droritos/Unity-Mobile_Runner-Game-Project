using UnityEngine;
using DG.Tweening; // Uses DoTween

public class CoinFlyEffect : MonoSingleton<CoinFlyEffect>
{
    // Singleton for easy access
    [Header("References")]
    [SerializeField] private GameObject flyingCoinPrefab; // The UI Image Prefab
    [SerializeField] private Transform coinTarget; // The Coin Icon in the top-right corner
    [SerializeField] private Transform spawnParent; // Usually the Canvas itself

    [Header("Settings")]
    [SerializeField] private float flyDuration = 0.8f;
    [SerializeField] private Ease flyEase = Ease.InBack; // "InBack" makes it pull back before shooting

    public void FlyCoinToTarget(Vector3 worldPosOf3DCoin)
    {
        // 1. Convert 3D World Position to 2D Screen Position
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosOf3DCoin);

        // 2. Spawn the fake UI Coin
        GameObject uiCoin = Instantiate(flyingCoinPrefab, spawnParent);
        uiCoin.transform.position = screenPosition; // Place it exactly where the real coin was

        // 3. Animate!
        uiCoin.transform.DOMove(coinTarget.position, flyDuration)
            .SetEase(flyEase)
            .OnComplete(() =>
            {
                // When it hits the target:
                
                // A. Add Score HERE (Visual feedback sync)
                // ScoreManager.Instance.AddScore(10); <-- Optional: Call your add score here for perfect timing
                
                // B. Punch effect on the target icon (Shake it!)
                coinTarget.DOPunchScale(Vector3.one * 0.15f, 0.2f, 10, 1);

                // C. Destroy the fake coin
                Destroy(uiCoin);
            });
    }
}