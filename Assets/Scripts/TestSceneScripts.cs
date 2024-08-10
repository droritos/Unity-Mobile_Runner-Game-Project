using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneScripts : MonoBehaviour
{ // Inventory Items Manager Script
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject ButtonPrefab;

    void Start()
    {
        InstantiateButtons();   
    }

    private void InstantiateButtons()
    {
        for (int i = 0;i < 18;i++) 
        {
            Instantiate(ButtonPrefab, inventoryMenu.transform);
        }
    }

}

[System.Serializable]
public class Inventory
{
    public string ItemName;
    public string ItemType;
    public int ItemGrade;

}