using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Reading Saved Data")]
    [SerializeField] ReadListFromFile readListFromFile;
    [SerializeField] WebcamCapture webcamCapture;

    [Header("Spidy")]
    public PlayerBehavior Player;
    public SpidyMorals SpidyMorals;
    public UpgradeMenu UpgradeMenuScript;

    [Header("Enemy")]
    public ObjectPoolManager EnemyPool;
    public ObjectPoolManager BulletPool;

    [Header("Menus")]
    [SerializeField] GameObject generalMenu;
    [SerializeField] GameObject upgradeMenu;


    private void Start()
    {
        Player.PlayerStatsConfig.SetStats();
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
