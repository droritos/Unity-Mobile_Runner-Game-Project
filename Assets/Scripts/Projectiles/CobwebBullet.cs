using UnityEngine;

public class CobwebBullet : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig speed;

    public float ProjectileScaler;
    public int RemainingPierces;

    private void Start()
    {
        ProjectileScaler = GameManager.Instance.Player.CobwebScaler;
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.Player.CobwebPiercingLevel;
    }

    private void SetProjectileSize(float percentage)
    {
        float scalingFactor = 1 + (percentage / 100f);
        this.transform.localScale *= scalingFactor;
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

    // Method to set both ricochet and piercing levels when the bullet is fired
    public void SetPiercingAndRicochetLevels(int piercingLevel, int ricochetLevel)
    {
        RemainingPierces = piercingLevel;
        ProjectileScaler = ricochetLevel;
        Debug.Log("Bullet initialized with " + RemainingPierces + " pierces and " + ProjectileScaler + " ricochets.");
    }

    private void ResetWeb()
    {
        ProjectileScaler = GameManager.Instance.Player.CobwebScaler;
        SetProjectileSize(ProjectileScaler);
        RemainingPierces = GameManager.Instance.Player.CobwebPiercingLevel;
    }
}
