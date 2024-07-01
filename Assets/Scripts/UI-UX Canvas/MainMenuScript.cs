using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    int num = 0;
    public void SetInputButtons()
    {
        PlayerPrefs.SetInt("ButtonSelected", 1);
        Debug.Log("buttons");
    }
    public void SetInputTouch()
    {
        PlayerPrefs.SetInt("ButtonSelected", 0);
        Debug.Log("touch");
    }
    public void SetInputGyro()
    {
        PlayerPrefs.SetInt("ButtonSelected", 2);
        Debug.Log("Gyro");
    }
    public void OpenSetingsMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayTheGameButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
