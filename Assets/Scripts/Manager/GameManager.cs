using System;
using System.Collections;
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
        Player.OnDeath += MoveToGameOver;
        PlayerManager.PlayerBehavior.PlayerStatsConfig.SetStats();
    }

    private void OnDestroy()
    {
        Player.OnDeath -= MoveToGameOver;
    }

    public void ResetStage()
    {
        Player.playerVitals.TakeDamage(999);
    }

    private void MoveToGameOver()
    {
        StartCoroutine(MoveToGameOver(1f));  // Death Effect Duration
    }
    private IEnumerator MoveToGameOver(float delay )
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}
