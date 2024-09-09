using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour, ISavabale
{
    [Header("Public Fields")]
    public PlayerArtitube PlayerArtitube;
    public PlayerStatsConfig playerStatsConfig;
    [HideInInspector] public int coinsCollected = 0;

    [Header("In Game Upgrade Levels")]
    private int _piercingLevel = 1;
    private int _cobwebDamageLevel = 1;
    private int _cobwebScaleLevel = 1;
    private int _attackSpeedLevel = 1;

    [Header("Private Editable Fields")]
    [SerializeField] ObjectPoolManager coinPool;
    [SerializeField] ObjectPoolManager lvlUpPool;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinPool.ReleaseObject(other.gameObject);
            coinsCollected++;
        }
        else if (other.CompareTag("LvLUp"))
        {
            lvlUpPool.ReleaseObject(other.gameObject);
            GameManager.Instance.UpgradeMenuScript.HandleLevelUp();
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            PlayerArtitube.TakeDamage(1);
            GameManager.Instance.BulletPool.ReleaseObject(other.gameObject);
        }
    }
    #region << Dying Methods >> 
    public int LevelReachWhenDied()
    {
        if (!PlayerArtitube.IsAlive())
        {
            return PlayerArtitube.GetLevel();
        }
        else
        {
            return -1;
        }
    }
    #endregion

    public int ApplyDamage()
    {
        int chance = Random.Range(0, 100);
        if (chance >= playerStatsConfig.CriticalChance)
        {
            Debug.Log($"U did Crit nice one!");
            return playerStatsConfig.CobwebDamage * 2;
        }
        else
        {
            return playerStatsConfig.CobwebDamage;
        }
    }


    #region << Upgrade Methods - Buttons >> 
    public void RestoreHealth(int amountInPercentage)
    {
        PlayerArtitube.RestoreHealth(amountInPercentage);
    }

    public void IncreasedDamage(TextMeshProUGUI levelText)
    {
        playerStatsConfig.CobwebDamage += 2;
        _cobwebDamageLevel++;
        levelText.text = $"LVL : {_cobwebDamageLevel}";
    }

    public void IncreasedAttackSpeed(TextMeshProUGUI levelText)
    {
        GameManager.Instance.SpidyMorals.FireCooldown -= 0.2f;
        _attackSpeedLevel++;
        levelText.text = $"LVL : {_attackSpeedLevel.ToString()}";

    }

    public void IncreaseWebSize(TextMeshProUGUI levelText)
    {
        playerStatsConfig.CobwebScaler += 30f;
        _cobwebScaleLevel++;
        levelText.text = $"LVL : {_cobwebScaleLevel.ToString()}";

    }
    public void Piercing(TextMeshProUGUI levelText)
    {
        playerStatsConfig.CobwebPiercingLevel++;
        _piercingLevel++;
        levelText.text = $"LVL : {_piercingLevel.ToString()}";
        Debug.Log($"Piercing : { playerStatsConfig.CobwebPiercingLevel}"); // Neeed to be followed if not reset
    }
    #endregion

    #region << In Game Save & Load Function - Resume >>
    public void Save(ref GameData data)
    {
        data.PlayerPositionX = this.transform.position.x;
        data.CoinsCollected = this.coinsCollected;

        data.CurrentHealhPoint = PlayerArtitube.CurrentHealhPoint;
        data.PlayerCurrentLevel = PlayerArtitube.PlayerCurrentLevel;

        data.ExperiencePoints = PlayerArtitube.ExperiencePoints;
        //Debug.Log("Saved Pos");
    }
    public void Load(GameData data)
    {
        // Loading Player Posotion , only X is matter
        Vector3 newPosition = this.transform.position;
        newPosition.x = data.PlayerPositionX;
        this.transform.position = newPosition;

        // Loading Player collected coins
        coinsCollected = data.CoinsCollected;

        // Loading Player health point + update UI
        PlayerArtitube.CurrentHealhPoint = data.CurrentHealhPoint;
        PlayerArtitube.UpdateHealthText();

        // Loading Player level + update UI
        PlayerArtitube.PlayerCurrentLevel = data.PlayerCurrentLevel;
        PlayerArtitube.UpdateLevelText();

        // Loading Player current experience + update UI
        PlayerArtitube.ExperiencePoints = data.ExperiencePoints;
    }
    #endregion
}
