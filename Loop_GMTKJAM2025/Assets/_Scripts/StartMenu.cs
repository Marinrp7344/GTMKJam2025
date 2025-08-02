using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject soundMenu;
    public GameObject gameplayMenu;

    public GameObject exitButton;
    public Image soundButton;
    public Image gameplayButton;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        exitButton.SetActive(false);
    }

    public void ToggleSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        exitButton.SetActive(true);
    }

    public void ToggleSoundMenu()
    {
        soundMenu.SetActive(true);
        gameplayMenu.SetActive(false);

        Color soundColor = soundButton.color;
        soundColor.a = 0.22f;
        soundButton.color = soundColor;

        Color gameColor = gameplayButton.color;
        gameColor.a = 0f;
        gameplayButton.color = gameColor;

    }

    public void ToggleGameplayMenu()
    {
        soundMenu.SetActive(false);
        gameplayMenu.SetActive(true);

        Color soundColor = soundButton.color;
        soundColor.a = 0f;
        soundButton.color = soundColor;

        Color gameColor = gameplayButton.color;
        gameColor.a = 0.22f;
        gameplayButton.color = gameColor;
    }
}
