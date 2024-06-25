using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int key;
    private string _optionChoosen = "InputSetting";

    public void SwipeOption()
    {
        _optionChoosen = "Swipe";
        key = 0;
        SetOptionsChoosen(key);
        SceneManager.LoadScene(1);
    }

    public void ButtonsOption()
    {
        _optionChoosen = "Buttons";
        key = 1;
        SetOptionsChoosen(key);
        SceneManager.LoadScene(1);
    }
    private void SetOptionsChoosen(int key)
    {
        PlayerPrefs.SetInt(_optionChoosen, key);
    }
}
