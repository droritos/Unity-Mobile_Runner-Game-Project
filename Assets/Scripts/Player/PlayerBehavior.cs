using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour, ISavabale
{
    [Header("Public Fields")]
    public PlayerArtitube PlayerArtitube;
    public PlayerStatsConfig PlayerStatsConfig;
    [HideInInspector] public int CoinsGathered = 0;

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
            CoinsGathered++;
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
        if (chance <= PlayerStatsConfig.CriticalChance)
        {
            Debug.Log($"U did Crit nice one!");
            return PlayerStatsConfig.CobwebDamage * 2;
        }
        else
        {
            return PlayerStatsConfig.CobwebDamage;
        }
    }


    #region << Upgrade Methods - Buttons >> 
    public void RestoreHealth()
    {
        PlayerArtitube.RestoreHealth();
    }

    public void IncreasedDamage(TextMeshProUGUI levelText)
    {
        PlayerStatsConfig.CobwebDamage += 2;
        _cobwebDamageLevel++;
        levelText.text = $"LVL : {_cobwebDamageLevel}";
    }

    public void IncreasedAttackSpeed(TextMeshProUGUI levelText)
    {
        PlayerStatsConfig.FireCooldown -= 0.2f;
        _attackSpeedLevel++;
        levelText.text = $"LVL : {_attackSpeedLevel.ToString()}";
        Debug.Log($"Fire Cooldown upgraded to {PlayerStatsConfig.FireCooldown} from {PlayerStatsConfig.FireCooldown + 0.2f}");

    }

    public void IncreaseWebSize(TextMeshProUGUI levelText)
    {
        PlayerStatsConfig.CobwebScaler += 30f;
        _cobwebScaleLevel++;
        levelText.text = $"LVL : {_cobwebScaleLevel.ToString()}";

    }
    public void Piercing(TextMeshProUGUI levelText)
    {
        PlayerStatsConfig.CobwebPiercingLevel++;
        _piercingLevel++;
        levelText.text = $"LVL : {_piercingLevel.ToString()}";
    }

    public void IncreseCriticalChance(TextMeshProUGUI levelText)
    {
        PlayerStatsConfig.CriticalChance += 10;
        Debug.Log($"Critical Chance : {PlayerStatsConfig.CriticalChance}");
    }

    #endregion

    #region << In Game Save & Load Function - Resume >>
    public void Save(ref GameData data)
    {
        data.PlayerPositionX = this.transform.position.x;
        data.CoinsCollected = this.CoinsGathered;

        data.CurrentHealhPoint = PlayerArtitube.CurrentHealhPoint;
        data.PlayerCurrentLevel = PlayerArtitube.PlayerCurrentLevel;

        data.ExperiencePoints = PlayerArtitube.ExperiencePoints;

        data.CobwebDamage = PlayerStatsConfig.CobwebDamage;
        data.FireCooldown = PlayerStatsConfig.FireCooldown;
        data.CobwebScaler = PlayerStatsConfig.CobwebScaler;
        data.CobwebPiercingLevel = PlayerStatsConfig.CobwebPiercingLevel;
        data.CriticalChance = PlayerStatsConfig.CriticalChance;
        Debug.Log($"Saved CR {PlayerStatsConfig.CriticalChance} On Game Data - {data.CriticalChance}");
    }
    public void Load(GameData data)
    {
        // Loading Player Posotion , only X is matter
        Vector3 newPosition = this.transform.position;
        newPosition.x = data.PlayerPositionX;
        this.transform.position = newPosition;

        // Loading Player collected coins
        CoinsGathered = data.CoinsCollected;

        // Loading Player health point + update UI
        PlayerArtitube.CurrentHealhPoint = data.CurrentHealhPoint;
        PlayerArtitube.UpdateHealthText();

        // Loading Player level + update UI
        PlayerArtitube.PlayerCurrentLevel = data.PlayerCurrentLevel;
        PlayerArtitube.UpdateLevelText();

        // Loading Player current experience + update UI
        PlayerArtitube.ExperiencePoints = data.ExperiencePoints;

        // Loading Player current stats
        PlayerStatsConfig.CobwebDamage = data.CobwebDamage;
        PlayerStatsConfig.FireCooldown = data.FireCooldown;
        PlayerStatsConfig.CobwebScaler = data.CobwebScaler;
        PlayerStatsConfig.CobwebPiercingLevel = data.CobwebPiercingLevel;
        PlayerStatsConfig.CriticalChance = data.CriticalChance;
    }
    #endregion
}
