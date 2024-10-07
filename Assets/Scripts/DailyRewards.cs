using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{
    public int LastDate;
    new PlayerBehavior playerBehavior;
    [SerializeField] CoinCollected coinHandler;

    public int Day_1;
    public GameObject OFF_1;
    public GameObject ACTIVE_1;
    public GameObject CHECK_1;

    public int Day_2;
    public GameObject OFF_2;
    public GameObject ACTIVE_2;
    public GameObject CHECK_2;

    public int Day_3;
    public GameObject OFF_3;
    public GameObject ACTIVE_3;
    public GameObject CHECK_3;

    public int Day_4;
    public GameObject OFF_4;
    public GameObject ACTIVE_4;
    public GameObject CHECK_4;

    public int Day_5;
    public GameObject OFF_5;
    public GameObject ACTIVE_5;
    public GameObject CHECK_5;

    public int Day_6;
    public GameObject OFF_6;
    public GameObject ACTIVE_6;
    public GameObject CHECK_6;

    public int Day_7;
    public GameObject OFF_7;
    public GameObject ACTIVE_7;
    public GameObject CHECK_7;

    private void Start()
    {
        Day_1 = PlayerPrefs.GetInt("Day 1");
        Day_2 = PlayerPrefs.GetInt("Day 2");
        Day_3 = PlayerPrefs.GetInt("Day 3");
        Day_4 = PlayerPrefs.GetInt("Day 4");
        Day_5 = PlayerPrefs.GetInt("Day 5");
        Day_6 = PlayerPrefs.GetInt("Day 6");
        Day_7 = PlayerPrefs.GetInt("Day 7");
        LastDate = PlayerPrefs.GetInt("LastLoginDate");

        ManageReward();

        if(LastDate != System.DateTime.Now.Day)
        {
            if(Day_1 == 0)
            {
                Day_1 = 1;
            }
            else if(Day_2 == 0)
            {
                Day_2 = 1;
            }
            else if (Day_3 == 0)
            {
                Day_3 = 1;
            }
            else if (Day_4 == 0)
            {
                Day_4 = 1;
            }
            else if (Day_5 == 0)
            {
                Day_5 = 1;
            }
            else if (Day_6 == 0)
            {
                Day_6 = 1;
            }
            else if (Day_7 == 0)
            {
                Day_7 = 1;
            }

            ManageReward();
        }
    }
    public void ManageReward()
    {
        if (Day_1 == 0)
        {
            OFF_1.SetActive(true);
            ACTIVE_1.SetActive(false);
            CHECK_1.SetActive(false);
        }
        if (Day_1 == 1)
        {
            ACTIVE_1.SetActive(true);
            OFF_1.SetActive(false);
            CHECK_1.SetActive(false);
        }
        if (Day_1 == 2)
        {
            CHECK_1.SetActive(true);
            ACTIVE_1.SetActive(false);
            OFF_1.SetActive(false);
        }

        if (Day_2 == 0)
        {
            OFF_2.SetActive(true);
            ACTIVE_2.SetActive(false);
            CHECK_2.SetActive(false);
        }
        if (Day_2 == 1)
        {
            ACTIVE_2.SetActive(true);
            OFF_2.SetActive(false);
            CHECK_2.SetActive(false);
        }
        if (Day_2 == 2)
        {
            CHECK_2.SetActive(true);
            ACTIVE_2.SetActive(false);
            OFF_2.SetActive(false);
        }

        if (Day_3 == 0)
        {
            OFF_3.SetActive(true);
            ACTIVE_3.SetActive(false);
            CHECK_3.SetActive(false);
        }
        if (Day_3 == 1)
        {
            ACTIVE_3.SetActive(true);
            OFF_3.SetActive(false);
            CHECK_3.SetActive(false);
        }
        if (Day_3 == 2)
        {
            CHECK_3.SetActive(true);
            ACTIVE_3.SetActive(false);
            OFF_3.SetActive(false);
        }

        if (Day_4 == 0)
        {
            OFF_4.SetActive(true);
            ACTIVE_4.SetActive(false);
            CHECK_4.SetActive(false);
        }
        if (Day_4 == 1)
        {
            ACTIVE_4.SetActive(true);
            OFF_4.SetActive(false);
            CHECK_4.SetActive(false);
        }
        if (Day_4 == 2)
        {
            CHECK_4.SetActive(true);
            ACTIVE_4.SetActive(false);
            OFF_4.SetActive(false);
        }

        if (Day_5 == 0)
        {
            OFF_5.SetActive(true);
            ACTIVE_5.SetActive(false);
            CHECK_5.SetActive(false);
        }
        if (Day_5 == 1)
        {
            ACTIVE_5.SetActive(true);
            OFF_5.SetActive(false);
            CHECK_5.SetActive(false);
        }
        if (Day_5 == 2)
        {
            CHECK_5.SetActive(true);
            ACTIVE_5.SetActive(false);
            OFF_5.SetActive(false);
        }

        if (Day_6 == 0)
        {
            OFF_6.SetActive(true);
            ACTIVE_6.SetActive(false);
            CHECK_6.SetActive(false);
        }
        if (Day_6 == 1)
        {
            ACTIVE_6.SetActive(true);
            OFF_6.SetActive(false);
            CHECK_6.SetActive(false);
        }
        if (Day_6 == 2)
        {
            CHECK_6.SetActive(true);
            ACTIVE_6.SetActive(false);
            OFF_6.SetActive(false);
        }

        if (Day_7 == 0)
        {
            OFF_7.SetActive(true);
            ACTIVE_7.SetActive(false);
            CHECK_7.SetActive(false);
        }
        if (Day_7 == 1)
        {
            ACTIVE_7.SetActive(true);
            OFF_7.SetActive(false);
            CHECK_7.SetActive(false);
        }
        if (Day_7 == 2)
        {
            CHECK_7.SetActive(true);
            ACTIVE_7.SetActive(false);
            OFF_7.SetActive(false);
        }
    }

    public void GetReward_1()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 1");

        Day_1 = 2;
        PlayerPrefs.SetInt("Day 1", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 5;
        coinHandler.AddCoinByDailyRewards(5);
    }

    public void GetReward_2()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 2");

        Day_2 = 2;
        PlayerPrefs.SetInt("Day 2", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 10;
        coinHandler.AddCoinByDailyRewards(10);

    }

    public void GetReward_3()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 3");

        Day_3 = 2;
        PlayerPrefs.SetInt("Day 3", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 15;
        coinHandler.AddCoinByDailyRewards(15);

    }

    public void GetReward_4()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 4");

        Day_4 = 2;
        PlayerPrefs.SetInt("Day 4", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 20;
        coinHandler.AddCoinByDailyRewards(20);

    }

    public void GetReward_5()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 5");

        Day_5 = 2;
        PlayerPrefs.SetInt("Day 5", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 25;
        coinHandler.AddCoinByDailyRewards(25);

    }

    public void GetReward_6()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 6");

        Day_6 = 2;
        PlayerPrefs.SetInt("Day 6", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 30;
        coinHandler.AddCoinByDailyRewards(30);

    }

    public void GetReward_7()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastLoginDate", LastDate);

        print("Reward 7");

        Day_7 = 2;
        PlayerPrefs.SetInt("Day 7", 2);

        ManageReward();
        //playerBehavior.CoinsGathered += 40;
        coinHandler.AddCoinByDailyRewards(40);

    }
}
