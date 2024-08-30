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
            Debug.Log("CobwebBullet hit an enemy: " + other.name);

            // Apply damage or other logic here

            // Check if the bullet can pierce more enemies
            if (RemainingPierces > 0)
            {
                RemainingPierces--;
                Debug.Log("Bullet pierced through. Remaining pierces: " + RemainingPierces);
                return; // Continue moving forward without releasing the bullet
            }
            // If no more pierces or ricochets are left, release the bullet back to the pool
            GameManager.Instance.SpidyMorals.CobwebPoolScript.ReleaseObject(this.gameObject);
            Debug.Log("Bullet released back to the pool.");
        }
        else if (other.CompareTag("Wall"))
        {
            GameManager.Instance.SpidyMorals.CobwebPoolScript.ReleaseObject(this.gameObject);
        }
    }

    private RobotEnemyScript FindNewTarget(Transform currentTarget)
    {
        float currentXPosition = currentTarget.position.x; // Get the x position of the current target
        float currentYPosition = currentTarget.position.y; // Get the y position of the current target
        float closestDistance = Mathf.Infinity;
        RobotEnemyScript closestEnemy = null;

        foreach (Transform enemyTransform in GameManager.Instance.EnemySpawnerScript.EnemyParent)
        {
            // Ensure the enemy is not the current target, is active, and is on the same Y level
            if (enemyTransform.gameObject != currentTarget.gameObject && enemyTransform.gameObject.activeInHierarchy)
            {
                float enemyXPosition = enemyTransform.position.x;
                float enemyYPosition = enemyTransform.position.y;

                // Check if the enemy is in a different lane (different X position) but on the same Y level
                if (enemyXPosition != currentXPosition && Mathf.Abs(enemyYPosition - currentYPosition) < Mathf.Epsilon)
                {
                    float distance = Vector3.Distance(transform.position, enemyTransform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyTransform.GetComponent<RobotEnemyScript>(); // Get the RobotEnemyScript component
                    }
                }
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log("Found new target in a different lane and on the same Y level: " + closestEnemy.name);
        }
        else
        {
            Debug.Log("No new target found in a different lane and on the same Y level.");
        }

        return closestEnemy;
    }

    private void RicochetToTarget(GameObject newTarget)
    {
        // Calculate direction to the new target
        Vector3 directionToTarget = (newTarget.transform.position - transform.position).normalized;

        // Update the bullet's rotation to face the new target
        transform.rotation = Quaternion.LookRotation(directionToTarget);

        // Decrease the remaining ricochets
        ProjectileScaler--;

        Debug.Log("Bullet redirected to: " + newTarget.name + ". Remaining ricochets: " + ProjectileScaler);
    }

    // Method to set both ricochet and piercing levels when the bullet is fired
    public void SetPiercingAndRicochetLevels(int piercingLevel, int ricochetLevel)
    {
        RemainingPierces = piercingLevel;
        ProjectileScaler = ricochetLevel;
        Debug.Log("Bullet initialized with " + RemainingPierces + " pierces and " + ProjectileScaler + " ricochets.");
    }
}
