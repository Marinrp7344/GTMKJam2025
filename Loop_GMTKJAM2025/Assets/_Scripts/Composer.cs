using UnityEngine;
using UnityEngine.Events;

public class Composer : MonoBehaviour
{

    public uint measureCount;
    public Beat currentBeat;

    Metronome metronome;

    public bool running { get; private set; } = false;

    private void Start()
    {
        metronome = Metronome.Singleton;

        StartRunning(); // temporary

        // for debugging
        // metronome.beat.AddListener(PrintCurrentBeat);
    }

    void NextMeasure()
    {
        currentBeat.measure++;

        if (currentBeat.measure > measureCount) { currentBeat.measure = 1; }
    }

    void NextQuarter()
    {
        currentBeat.quarter++;

        if (currentBeat.quarter > metronome.quartersPerMeasure) { currentBeat.quarter = 1; }
    }

    void NextEighth()
    {
        currentBeat.eighth++;

        if (currentBeat.eighth > metronome.quartersPerMeasure * 2) { currentBeat.eighth = 1; }
    }
    void NextSixteenth()
    {
        currentBeat.sixteenth++;

        if (currentBeat.sixteenth > metronome.quartersPerMeasure * 4) { currentBeat.sixteenth = 1; }
    }

    void PrintCurrentBeat()
    {
        Debug.Log($"measure {currentBeat.measure}, quarter {currentBeat.quarter}, eighth {currentBeat.eighth}, sixteenth {currentBeat.sixteenth}");
    }

    /// <summary>
    /// starts composer. measure will be zero until the next measure begins,
    /// but all other beat trackers are accurate and current
    /// </summary>
    public void StartRunning()
    {
        // if new measure is starting, set the measure to 1.
        // if new measure is NOT starting, then the metronome is currently mid-measure. so measure is 0
        int currentMeasure = 0;
        if (metronome.quartersThisMeasure <= 1 &&
            metronome.eighthsThisMeasure <= 1 &&
            metronome.sixteenthsThisMeasure <= 1)
        {
            currentMeasure = 1;
        }

        // fetch current beat information
        currentBeat = new Beat(currentMeasure, metronome.quartersThisMeasure, metronome.eighthsThisMeasure, metronome.sixteenthsThisMeasure);

        // subscribe to all beat events
        metronome.measure.AddListener(NextMeasure);
        metronome.quarter.AddListener(NextQuarter);
        metronome.eighth.AddListener(NextEighth);
        metronome.sixteenth.AddListener(NextSixteenth);

        running = true;
    }


}
