using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GhostTrail : MonoBehaviour
{
    [Header("Settings")]
   [SerializeField] float activeTime = 0.5f; 
   [SerializeField] float meshRefreshRate = 0.05f;
   [SerializeField] float ghostLifeTime = 0.5f;
   [SerializeField] private float _ghostScale = 0.9f;

    [Header("References")]
    [SerializeField] private ObjectPoolManager ghostPool; // <--- Drag your "GhostPool" object here
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    [SerializeField] private Transform spawnPoint;


    public void ActivateTrail()
    {
        StartCoroutine(ActivateTrailRoutine());
    }

    IEnumerator ActivateTrailRoutine()
    {
        float elapsed = 0f;
        while (elapsed < activeTime)
        {
            SpawnGhost();
            elapsed += meshRefreshRate;
            yield return new WaitForSeconds(meshRefreshRate);
        }
    }

    private void SpawnGhost()
    {
        // 1. GET FROM YOUR POOL
        GameObject ghostObj = ghostPool.GetObject();

        // 2. Position and Rotate
        ghostObj.transform.position = spawnPoint.position;
        ghostObj.transform.rotation = transform.rotation;
        ghostObj.transform.localScale = playerMesh.transform.localScale * _ghostScale;

        // 3. Setup Mesh
        MeshFilter mf = ghostObj.GetComponent<MeshFilter>();
        MeshRenderer mr = ghostObj.GetComponent<MeshRenderer>();
        
        // --- CRITICAL OPTIMIZATION ---
        // Even though we reused the GameObject, we must check if it already has a Mesh.
        // If we do "new Mesh()" every time, the pool is useless for performance.
        Mesh meshToBake = mf.sharedMesh;
        if (meshToBake == null)
        {
            meshToBake = new Mesh();
            mf.mesh = meshToBake;
        }

        // 4. Bake the snapshot onto the EXISTING mesh
        playerMesh.BakeMesh(meshToBake);

        // 5. Reset Alpha (Because the previous use faded it to 0)
        Color resetColor = mr.material.GetColor("_BaseColor");
        resetColor.a = 0.5f; // Set your desired start alpha
        mr.material.SetColor("_BaseColor", resetColor);

        // 6. DoTween Fade -> RETURN TO POOL
        mr.material.DOFade(0f, ghostLifeTime)
            .SetId("_BaseColor")
            .OnComplete(() => 
            {
                // Return to your specific pool manager
                ghostPool.ReleaseObject(ghostObj);
            });
    }
}