using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Reading Saved Data")]
    [SerializeField] ReadListFromFile readListFromFile;
    [SerializeField] WebcamCapture webcamCapture;

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
        base.Awake();
        PlayerUIManager.Bind(Player.playerVitals); // Start GUI
    }

    private void Start()
    {
        PlayerManager.PlayerBehavior.PlayerStatsConfig.SetStats();
    }

    private void Update()
    {
        PauseGameWhenMenuVisible(upgradeMenu);
    }
    public void ResetStage()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.DeleteFileSavedFile();
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.LogWarning("No Save Manager Exits!");
        }
    }

    public void PauseGameWhenMenuVisible(GameObject menu)
    {
        if (menu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
