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

    private void Start()
    {
        Player.PlayerStatsConfig.SetStats();
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
}
