using System.Collections.Generic;
using UnityEngine;

public class ValueIndicators : MonoBehaviour 
{
    public static ValueIndicators Instance;

    [Header("Settings")]
    public List<Indicator> indicators = new List<Indicator>();
    [SerializeField] private int maxPoolSize = 15;
    [SerializeField] private Indicator IndicatorPrefab;
    
    // We don't need a specific WorldCanvas for 3D Text, 
    // we can just parent them to this object to keep hierarchy clean.
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        // Pre-fill the pool
        for(int i = 0; i < maxPoolSize; i++)
        {
            CreateNewIndicator();
        }
    }

    public void SpawnIndicator(Vector3 worldPosition, int value, ValueTypes type)
    {
        if (value <= 0) return; 

        Indicator indicatorToUse = null;

        // 1. Find an inactive indicator
        foreach(Indicator indicator in indicators)
        {
            if(!indicator.gameObject.activeInHierarchy)
            {
                indicatorToUse = indicator;
                break;
            }
        }

        // 2. If none found, create a new one (Expand pool)
        if (indicatorToUse == null)
        {
            indicatorToUse = CreateNewIndicator();
        }

        // 3. Position and Activate
        // We add Vector3.up * 2 so it appears above the enemy, not inside them
        indicatorToUse.transform.position = worldPosition + (Vector3.up * 2f); 
        indicatorToUse.gameObject.SetActive(true);
        
        // 4. Set Logic
        indicatorToUse.SetText(value, type);
    }

    private Indicator CreateNewIndicator()
    {
        Indicator newIndicator = Instantiate(IndicatorPrefab, transform);
        indicators.Add(newIndicator);
        newIndicator.gameObject.SetActive(false);
        return newIndicator;
    }
}