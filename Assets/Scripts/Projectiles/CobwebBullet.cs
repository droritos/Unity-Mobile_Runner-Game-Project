using UnityEngine;

public class CobwebBullet : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig speed;

    public float ProjectileScaler;
    public int RemainingPierces;

    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = this.transform.localScale;
        ProjectileScaler = GameManager.Instance.Player.playerStatsConfig.CobwebScaler;
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.Player.playerStatsConfig.CobwebPiercingLevel;
    }
    void Update()
    {
        this.transform.Translate(speed.CobwebSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Check if the bullet can pierce more enemies
            if (RemainingPierces > 0)
            {
                RemainingPierces--;
                Debug.Log("Bullet pierced through. Remaining pierces: " + RemainingPierces);
                return; // Continue moving forward without releasing the bullet
            }
            // If no more pierces or ricochets are left, release the bullet back to the pool
            GameManager.Instance.SpidyMorals.CobwebPoolScript.ReleaseObject(this.gameObject);
            ResetWeb();
        }
        else if (other.CompareTag("Wall"))
        {
            GameManager.Instance.SpidyMorals.CobwebPoolScript.ReleaseObject(this.gameObject);
            ResetWeb();
        }
    }
    private void SetProjectileSize(float percentage)
    {
        float scalingFactor = 1 + (percentage / 100f);

        // Reset to original scale first, then apply the scaling factor
        this.transform.localScale = _originalScale * scalingFactor;
        Debug.Log($"Web localScale = {this.transform.localScale}");
    }
    private void ResetWeb()
    {
        ProjectileScaler = GameManager.Instance.Player.playerStatsConfig.CobwebScaler;
        Debug.Log($"Web ProjectileScaler = {ProjectileScaler}");
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.Player.playerStatsConfig.CobwebPiercingLevel;
    }
}
