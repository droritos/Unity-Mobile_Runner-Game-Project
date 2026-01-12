using UnityEngine;

namespace Effects
{
    public class BodyExploder : MonoBehaviour
    {
        [Header("Explosion Settings")]
        [SerializeField] private ParticleSystem explosionVFX;
        [SerializeField] private Transform vfxSpawnPoint;
        [SerializeField] private float explosionForce = 500f;
        [SerializeField] private float explosionRadius = 5f;
        
        // NEW: Controls how fast they spin
        [SerializeField] private float rotationForce = 50f; 

        [Header("Body Parts")]
        [SerializeField] private Transform dummyRoot;
        [SerializeField] private Rigidbody[] parts;

        public void DieAndExplode()
        {
            this.gameObject.SetActive(true);

            // 1. Spawn VFX
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, vfxSpawnPoint.position, Quaternion.identity);
            }

            // 2. Explode Parts
            foreach (Rigidbody rb in parts)
            {
                if(rb == null) continue; // Safety check

                rb.isKinematic = false;
                rb.transform.SetParent(null);

                // A. The Push (Explosion)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                // B. The Twist (Random Rotation) - THIS FIXES THE STIFFNESS
                // We add a random torque in a random direction (X, Y, or Z)
                rb.AddTorque(Random.insideUnitSphere * rotationForce, ForceMode.Impulse);
            }

            Debug.Log("PLAYER EXPLODED!");
        }
    }
}