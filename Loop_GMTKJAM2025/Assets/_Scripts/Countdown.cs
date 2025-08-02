using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] AudioClip four;
    [SerializeField] AudioClip three;
    [SerializeField] AudioClip two;
    [SerializeField] AudioClip one;

    [Space]
    [SerializeField] BeatDelay threeDelay;
    [SerializeField] BeatDelay twoDelay;
    [SerializeField] BeatDelay oneDelay;

    [ContextMenu("ready countdown")]
    public void ReadyCountdown()
    {
        Metronome.Singleton.measure.AddListener(StartCountdown);
    }

    void StartCountdown()
    {
        PlayCountdownClip(4);

        threeDelay.StartDelay(new BeatBasedDuration(2, 0, 0));
        twoDelay.StartDelay(new BeatBasedDuration(3, 0, 0));
        oneDelay.StartDelay(new BeatBasedDuration(4, 0, 0));

        Metronome.Singleton.measure.RemoveListener(StartCountdown);

    }

    public void PlayCountdownClip(int number)
    {
        switch (number)
        {
            case 4:
                SoundFXManager.Instance.PlaySoundFXClip(four, transform, 1);
                break;

            case 3:
                SoundFXManager.Instance.PlaySoundFXClip(three, transform, 1);
                break;

            case 2:
                SoundFXManager.Instance.PlaySoundFXClip(two, transform, 1);
                break;

            case 1:
                SoundFXManager.Instance.PlaySoundFXClip(one, transform, 1);
                break;

        }
    }
}
