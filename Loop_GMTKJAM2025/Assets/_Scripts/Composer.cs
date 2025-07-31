using UnityEngine;
using UnityEngine.Events;

public class Composer : MonoBehaviour
{

    public uint measureCount;
    public Beat currentBeat;

    Metronome metronome;

    public bool running = false;

    private void Start()
    {
        metronome = Metronome.Singleton;

        StartRunningOnNextMeasure();

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

        if (currentBeat.quarter > metronome.quartersInMeasure) { currentBeat.quarter = 1; }
    }

    void NextEighth()
    {
        currentBeat.eighth++;

        if (currentBeat.eighth > metronome.quartersInMeasure * 2) { currentBeat.eighth = 1; }
    }
    void NextSixteenth()
    {
        currentBeat.sixteenth++;

        if (currentBeat.sixteenth > metronome.quartersInMeasure * 4) { currentBeat.sixteenth = 1; }
    }

    void PrintCurrentBeat()
    {
        Debug.Log($"measure {currentBeat.measure}, quarter {currentBeat.quarter}, eighth {currentBeat.eighth}, sixteenth {currentBeat.sixteenth}");
    }

    /// <summary>
    /// sets composer to begin running at the start of next measure
    /// </summary>
    /// <param name="isRunning"></param>
    public void StartRunningOnNextMeasure()
    {
        currentBeat = new Beat(0, 0, 0, 0);

        // listener added now because otherwise it would not trigger until the measure AFTER the composer starts tracking
        // adding it now makes sure all nextBeat methods trigger appropriately on the beat that the composer starts on
        metronome.measure.AddListener(NextMeasure);

        metronome.measure.AddListener(TryStartComposer);
    }

    /// <summary>
    /// to be subscribed to measure event.
    /// starts the composer's beat tracking.
    /// </summary>
    public void TryStartComposer()
    {
        // does not add nextMeasure as listener because that was done in StartRunningOnNextMeasure

        metronome.quarter.AddListener(NextQuarter);
        metronome.eighth.AddListener(NextEighth);
        metronome.sixteenth.AddListener(NextSixteenth);

        metronome.measure.RemoveListener(TryStartComposer);
    }

}
