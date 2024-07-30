
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    
    private void Start()
    {
        PlayerData.Instance.LoadProfile();

    }
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
        Debug.Log("Gyroo");
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

    public void MenuSwitcher(GameObject menu)
    {
        if(menu.activeInHierarchy)
            menu.SetActive(false);
        else
            menu.SetActive(true);
    }
}
