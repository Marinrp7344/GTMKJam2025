using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Beat
{
    public int measure;
    public int quarter;
    public int eighth;
    public int sixteenth;

    /// <summary>
    /// compares a source beat to an other beat.
    /// if the source beat has a field that equals 0, it is ignored in the comparison
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Beat other)
    {
        if ((measure == other.measure || measure == 0) &&
            (quarter == other.quarter || quarter == 0) &&
            (eighth == other.eighth || eighth == 0) &&
            (sixteenth == other.sixteenth || sixteenth == 0))
        {
            return true;
        }
        else { return false; }
    }

    public Beat(int measure, int quarter, int eighth, int sixteenth)
    {
        this.measure = measure;
        this.quarter = quarter;
        this.eighth = eighth;
        this.sixteenth = sixteenth;
    }

    public void Increment()
    {
        sixteenth++;

        // enough sixteenths to make an eighth note
        if (sixteenth % 2 != 0)
        {
            eighth++;

            // enough eighths to make a quarter note
            if (eighth % 2 != 0)
            {
                quarter++;

                // enough quarter notes to end measure
                if (quarter > Metronome.Singleton.quartersInMeasure)
                {
                    quarter = 1;
                    eighth = 1;
                    sixteenth = 1;
                    measure++;
                }
            }
        }
        
    }
}

public enum Precision { measure, quarter, eighth, sixteenth }

public class Metronome : MonoBehaviour
{

    public float bpm = 150;
    public uint quartersInMeasure { get; private set; } = 4;

    // beat events
    public UnityEvent measure;
    public UnityEvent quarter;
    public UnityEvent eighth;
    public UnityEvent sixteenth;

    float beatDuration;

    uint quartersThisMeasure = 0;


    public static Metronome Singleton;
    private void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }

        beatDuration = 60 / bpm;
    }

    [ContextMenu("start metronome")]
    void StartMetronome()
    {
        // send measure event without calling measure method
        // sets measure to 1 to match with the rest of the notes
        measure.Invoke();

        Quarter();
    }

    void Quarter()
    {
        // increments measure if the measure ends
        quartersThisMeasure++;
        if (quartersThisMeasure > quartersInMeasure)
        {
            Measure();
        }


        quarter.Invoke();
        Invoke(nameof(Quarter), beatDuration);


        // invokes an eighth this beat, and queues the eighth
        // that will play in between this quarter and the next quarter
        Eighth();
        Invoke(nameof(Eighth), beatDuration/2);

    }

    void Measure()
    {
        measure.Invoke();

        quartersThisMeasure = 1;
    }

    void Eighth()
    {
        eighth.Invoke();

        Invoke(nameof(Sixteenth), beatDuration/4);
    }

    void Sixteenth()
    {
        sixteenth.Invoke();
    }

}
