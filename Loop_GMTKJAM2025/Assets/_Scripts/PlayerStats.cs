using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int score;

    private void Awake()
    {
        Instance = this;
    }
    public void IncreaseScore(int recievedScore)
    {
        score += recievedScore;
    }

}
