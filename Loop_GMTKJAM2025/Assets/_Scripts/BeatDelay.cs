using UnityEngine;
using UnityEngine.Events;

public class BeatDelay : MonoBehaviour
{
    [SerializeField] BeatBasedDuration delay;

    public UnityEvent delayComplete;

    private void Start()
    {
        Metronome metronome = Metronome.Singleton;

        metronome.quarter.AddListener(ProcessQuarter);
        metronome.eighth.AddListener(ProcessEighth);
        metronome.sixteenth.AddListener(ProcessSixteenth);
    }

    public void SetDelay(BeatBasedDuration delay)
    {
        this.delay = delay;
    }

    void ProcessQuarter()
    {
        if (delay.quarter > 0) { delay.quarter--; }
        CheckDelayComplete();
    }

    void ProcessEighth()
    {
        if (delay.eighth > 0) { delay.eighth--; }
        CheckDelayComplete();
    }

    void ProcessSixteenth()
    {
        if (delay.sixteenth > 0) { delay.sixteenth--; }
        CheckDelayComplete();
    }

    /// <summary>
    /// invokes the delayComplete event if the specified delay has been completed
    /// </summary>
    void CheckDelayComplete()
    {
        if (delay.quarter <= 0 &&
            delay.eighth <= 0 &&
            delay.sixteenth <= 0)
        {
            delayComplete.Invoke();
        }
    }
}
