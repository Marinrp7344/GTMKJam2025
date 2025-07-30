using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Beat
{
    public int measure;
    public int quarter;
    public int eighth;

    public bool Equals(Beat other)
    {
        if (measure == other.measure &&
            quarter == other.quarter &&
            eighth == other.eighth)
        {
            return true;
        }
        else { return false; }
    }
}

public class Metronome : MonoBehaviour
{

    public float bpm = 150;
    public uint quartersInMeasure { get; private set; } = 4;

    public UnityEvent beat; // any time a beat event is fired

    // beat events
    public UnityEvent quarter;
    public UnityEvent measure;
    public UnityEvent eighth;

    float beatDuration;
    float eighthDuration;

    uint quartersThisMeasure = 0;


    public static Metronome Singleton;
    private void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }

        beatDuration = 60 / bpm;
        eighthDuration = beatDuration / 2;

        quarter.AddListener(beat.Invoke);
        eighth.AddListener(beat.Invoke);
        measure.AddListener(beat.Invoke);
    }

    [ContextMenu("start metronome")]
    void StartMetronome()
    {
        Quarter();
        Measure();
    }

    void Quarter()
    {
        quarter.Invoke();


        Invoke(nameof(Quarter), beatDuration);

        // invokes an eighth this beat, and queues the eighth
        // that will play in between this quarter and the next quarter
        Eighth();
        Invoke(nameof(Eighth), eighthDuration);

        // increments measure if the measure ends
        quartersThisMeasure++;
        if (quartersThisMeasure >= quartersInMeasure)
        {
            Measure();
        }

    }

    void Measure()
    {
        measure.Invoke();

        quartersThisMeasure = 0;
    }

    void Eighth()
    {
        eighth.Invoke();
    }

}
