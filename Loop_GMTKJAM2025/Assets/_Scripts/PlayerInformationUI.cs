using UnityEngine;
using TMPro;
public class PlayerInformationUI : MonoBehaviour
{
    public static PlayerInformationUI Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        Instance = this;
        LoadHighscore();
    }
    public void Update()
    {
        UpdateScoreText();
    }

    public void LoadHighscore()
    {
        highscoreText.text = "HighScore\n" + PlayerPrefs.GetInt("Highscore");
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + PlayerStats.Instance.score;
    }

    public void UpdateHighScore()
    {
        if (PlayerStats.Instance.score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", PlayerStats.Instance.score);
        }
    }
}
