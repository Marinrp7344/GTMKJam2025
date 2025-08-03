using UnityEngine;

public class OneTwo : MonoBehaviour
{
    [SerializeField] BeatBasedDuration delayDuration;
    [SerializeField] BeatDelay secondShotDelay;

    public void PrepareSecondShot()
    {
        secondShotDelay.StartDelay(delayDuration);
    }
}
