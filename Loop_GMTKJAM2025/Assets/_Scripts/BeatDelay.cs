using UnityEngine;
using UnityEngine.Events;

public class BeatDelay : MonoBehaviour
{
    [SerializeField] BeatBasedDuration delay;


    bool running = false;
    public UnityEvent delayComplete;

    private void Start()
    {
        Metronome metronome = Metronome.Singleton;

        metronome.quarter.AddListener(ProcessQuarter);
        metronome.eighth.AddListener(ProcessEighth);
        metronome.sixteenth.AddListener(ProcessSixteenth);
    }

    public void StartDelay(BeatBasedDuration delay)
    {
        this.delay = delay;
        running = true;
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
        if (running &&
            delay.quarter <= 0 &&
            delay.eighth <= 0 &&
            delay.sixteenth <= 0)
        {
            delayComplete.Invoke();
            running = false;
        }
    }
}
