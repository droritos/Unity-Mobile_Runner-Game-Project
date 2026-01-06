using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Terrain")]
    [field: SerializeField] public Transform LeftLane{get ; private set;}
    [field: SerializeField] public Transform MidLane{get ; private set;}
    [field: SerializeField] public Transform RightLane{get ; private set;}
    
    [Header("GUI")]
    [field: SerializeField] public PlayerUIManager PlayerUIManager {get ; private set;}
    
    [Header("Player")]
    [field: SerializeField] public PlayerManager PlayerManager { get; private set; }
    public PlayerBehavior Player => PlayerManager.PlayerBehavior; // Instead of changing the entire names in all files
    public UpgradeMenu UpgradeMenuScript;

    [Header("Enemy")]
    public ObjectPoolManager EnemyPool;
    public ObjectPoolManager BulletPool;

    [Header("Menus")]
    [SerializeField] GameObject generalMenu;
    [SerializeField] GameObject upgradeMenu;

    protected override void Awake()
    {
        Application.targetFrameRate = 61;
        QualitySettings.vSyncCount = 0;
        
        base.Awake();
        PlayerUIManager.Bind(Player.playerVitals); // Start GUI
        
        //PauseManager.Instance.SetPaused(false); // Unpause when Start
    }
    [ContextMenu("Take ScreenShot")]
    public void ScreenShot()
    {
        ScreenCapture.CaptureScreenshot("ScreenShot Of In Game.png", 1);
        Debug.Log("ScreenShot Of In Game.png");
    }
    private void Start()
    {
        PlayerManager.PlayerBehavior.PlayerStatsConfig.SetStats();
    }
    public void ResetStage()
    {
        Player.playerVitals.TakeDamage(999);
        /*
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.DeleteFileSavedFile();
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.LogWarning("No Save Manager Exits!");
        }
        */
    }

    public void PauseGameWhenMenuVisible(GameObject menu)
    {
        PauseManager.Instance.SetPaused(menu.activeSelf);
    }
}
