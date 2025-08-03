using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject fullMenu;
    public GameObject mainMenu;
    public GameObject soundMenu;
    public GameObject gameplayMenu;
    public bool onMainMenu;

    private void Start()
    {
        Instance = this;
        fullMenu.SetActive(false);
    }
    public void Exit()
    {
        if(onMainMenu)
        {
            fullMenu.SetActive(false);
            BeatMakerManager.Singleton.gameObject.SetActive(true);

        }
        else
        {
            onMainMenu = true;
            
            mainMenu.SetActive(true);
            soundMenu.SetActive(false);
            gameplayMenu.SetActive(false);
        }
    }

    public void TurnOnSoundMenu()
    {
        onMainMenu = false;
        mainMenu.SetActive(false);
        soundMenu.SetActive(true);
        gameplayMenu.SetActive(false);
    }

    public void TurnOnGameplayMenu()
    {
        onMainMenu = false;
        mainMenu.SetActive(false);
        soundMenu.SetActive(false);
        gameplayMenu.SetActive(true);
    }

    public void ToggleMenu()
    {
        if (fullMenu.activeSelf)
        {
            
            Debug.Log("Pressed");
            Exit();
        }
        else
        {
            fullMenu.SetActive(true);
            BeatMakerManager.Singleton.gameObject.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
