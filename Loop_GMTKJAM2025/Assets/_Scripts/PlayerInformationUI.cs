using UnityEngine;
using TMPro;
public class PlayerInformationUI : MonoBehaviour
{
    public static PlayerInformationUI Instance;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI scoreText;

    public void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + PlayerStats.Instance.score;
    }
}
