using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string name;
    public int maxLevel = 5;
    public int currentLevel = 0;

    [SerializeField] TextMeshProUGUI level;

    public bool CanUpgrade()
    {
        return currentLevel < maxLevel;
    }
}
