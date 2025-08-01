using UnityEngine;
using TMPro;
public class PlayerInformationUI : MonoBehaviour
{
    public static PlayerInformationUI Instance;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI scoreText;

    public void Update()
    {
        UpdateBudgetText();
        UpdateScoreText();
    }

    private void UpdateBudgetText()
    {
        budgetText.text = "Budget\n" + WeaponManager.Singleton.GetBudget();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + PlayerStats.Instance.score;
    }
}
