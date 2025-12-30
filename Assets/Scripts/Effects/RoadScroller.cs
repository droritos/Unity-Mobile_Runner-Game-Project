using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [Header("Settings")]
    // Adjust this to match the visual speed of your moving obstacles
    [SerializeField] MovingObjectsConfig _config;
    
    [SerializeField] Renderer _roadMaterial;
    private Vector2 _currentOffset;

    void Update()
    {
        // Calculate how much to move based on WorldSpeed
        // We use 'y' for vertical texture scrolling (common for roads)
        // If your road texture moves sideways, change 'y' to 'x'
        float offsetStep = -_config.RoadSpeed * WorldSpeed.SpeedMultiplier * Time.deltaTime;

        // Add to the current offset
        // We use y because usually UVs map Up/Down for road length
        _currentOffset.y += offsetStep; 
        
        // Apply the offset to the material
        // The '% 1' keeps the number small to prevent floating point errors after hours of play
        _currentOffset.y %= 1; 
        
        _roadMaterial.material.mainTextureOffset = _currentOffset;
    }
}