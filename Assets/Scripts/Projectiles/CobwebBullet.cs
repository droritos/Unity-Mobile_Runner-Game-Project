using UnityEngine;

public class CobwebBullet : MonoSingleton<CobwebBullet>
{
    [SerializeField] MovingObjectsConfig speed;
    public int RemainingRicochets = 0;
    public int RemainingPierces = 0;

    void Update()
    {
        this.transform.Translate(speed.CobwebSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Reduce the health of the enemy or apply damage logic here

            // Check if the bullet can pierce more enemies
            if (RemainingPierces > 0)
            {
                RemainingPierces--;
                // Continue moving forward; don't release the bullet yet
                return;
            }

            // Check if the bullet can ricochet to another enemy
            if (RemainingRicochets > 0)
            {
                RobotEnemyScript newTarget = FindNewTarget(other.transform);
                if (newTarget != null)
                {
                    RicochetToTarget(newTarget.gameObject);
                    return; // Exit the method to avoid releasing the bullet
                }
            }

            // If no more pierces or ricochets are left, release the bullet back to the pool
            GameManager.Instance.SpidyMorals.CobwebPoolScript.ReleaseObject(this.gameObject);
        }
    }

    private RobotEnemyScript FindNewTarget(Transform currentTarget)
    {
        // Logic to find a new target (another enemy in a different lane)
        float closestDistance = Mathf.Infinity;
        RobotEnemyScript closestEnemy = null;

        foreach (RobotEnemyScript enemy in GameManager.Instance.EnemySpawnerScript.EnemiesList)
        {
            if (enemy.gameObject != currentTarget.gameObject && enemy.gameObject.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
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
        RemainingRicochets--;
    }

    // Method to set both ricochet and piercing levels when the bullet is fired
    public void SetPiercingAndRicochetLevels(int piercingLevel, int ricochetLevel)
    {
        RemainingPierces = piercingLevel;
        RemainingRicochets = ricochetLevel;
    }
}
