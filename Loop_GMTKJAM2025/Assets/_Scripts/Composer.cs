using UnityEngine;

public class Composer : MonoBehaviour
{

    public uint measureCount;
    public Beat currentBeat;

    public BeatAction selectedBeatAction;

    Metronome metronome;

    private void Start()
    {
        metronome = Metronome.Singleton;

        metronome.measure.AddListener(NextMeasure);
        metronome.quarter.AddListener(NextQuarter);
        metronome.eighth.AddListener(NextEighth);

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

    void PrintCurrentBeat()
    {
        Debug.Log($"measure {currentBeat.measure}, quarter {currentBeat.quarter}, eighth {currentBeat.eighth}");
    }

    public void SelectBeatAction(BeatAction action)
    {
        selectedBeatAction = action;
    }

}
