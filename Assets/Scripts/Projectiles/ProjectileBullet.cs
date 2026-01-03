using GlobalClasses;
using Interfaces;
using Manager;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour , IPausable
{
    [SerializeField] MovingObjectsConfig speed;

    public float ProjectileScaler;
    public int RemainingPierces;

    private Vector3 _originalScale;
    
    private bool _isPaused;

    private void Start()
    {
        PauseManager.Instance?.Register(this);
        
        _originalScale = this.transform.localScale;
        ProjectileScaler = GameManager.Instance.PlayerManager.PlayerBehavior.PlayerStatsConfig.CobwebScaler;
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.PlayerManager.PlayerBehavior.PlayerStatsConfig.CobwebPiercingLevel;
    }

    private void OnDestroy()
    {
        PauseManager.Instance?.Unregister(this);
    }
    void Update()
    {
        if(_isPaused) return;
        this.transform.Translate(Vector3.forward * (speed.CobwebSpeed * Time.deltaTime * WorldSpeed.SpeedMultiplier));
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
            GameManager.Instance.PlayerManager.PlayerCombatController.ProjectilePoolScript.ReleaseObject(this.gameObject);
            ResetWeb();
            AudioManager.Instance.PlaySFXByType(RandomAudioType.Hit);
        }
        else if (other.CompareTag("Wall"))
        {
            GameManager.Instance.PlayerManager.PlayerCombatController.ProjectilePoolScript.ReleaseObject(this.gameObject);
            ResetWeb();
        }
    }
    private void SetProjectileSize(float percentage)
    {
        float scalingFactor = 1 + (percentage / 100f);

        // Reset to original scale first, then apply the scaling factor
        this.transform.localScale = _originalScale * scalingFactor;
        //Debug.Log($"Web localScale = {this.transform.localScale}");
    }
    private void ResetWeb()
    {
        ProjectileScaler = GameManager.Instance.Player.PlayerStatsConfig.CobwebScaler;
        //Debug.Log($"Web ProjectileScaler = {ProjectileScaler}");
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.Player.PlayerStatsConfig.CobwebPiercingLevel;
    }

    public void SetPaused(bool paused)
    {
        _isPaused =  paused;
    }
}
