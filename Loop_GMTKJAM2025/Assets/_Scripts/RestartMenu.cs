using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartMenu : MonoBehaviour
{
    [SerializeField] private int menuScene;
    public static RestartMenu Instance;

    private void Start()
    {
        RestartMenu.Instance = this;
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
