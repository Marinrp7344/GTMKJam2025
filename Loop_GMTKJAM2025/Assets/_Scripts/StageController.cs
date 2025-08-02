using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] Countdown countdown;
    [SerializeField] BeatDelay countdownFinish;

    public void ReadyNextStage()
    {
        foreach (Composer composer in FindObjectsByType<Composer>(FindObjectsSortMode.None))
        {
            composer.StopRunning();
        }

        SpawnDirector.Singleton.SetUpNewStage();

        Metronome.Singleton.measure.AddListener(StartStage);
        countdown.ReadyCountdown();

    }

    void StartStage()
    {
        countdownFinish.StartDelay(new BeatBasedDuration(5,0,0));

        Metronome.Singleton.measure.RemoveListener(StartStage);
    }

    public void StartAllComposers()
    {

        foreach (Composer composer in FindObjectsByType<Composer>(FindObjectsSortMode.None))
        {
            composer.StartRunning();
        }
    }
}
