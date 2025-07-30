using UnityEngine;
using UnityEngine.Events;

public class Metronome : MonoBehaviour
{

    [SerializeField] float bpm = 150;
    [SerializeField] uint beatsInMeasure = 4;


    public UnityEvent beat;
    public UnityEvent measure;
    public UnityEvent eighth;

    float beatDuration;
    float eighthDuration;

    uint beatsThisMeasure = 0;


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
        Beat();
        Measure();
    }

    void Beat()
    {
        beat.Invoke();


        Invoke(nameof(Beat), beatDuration);

        Eighth();
        Invoke(nameof(Eighth), eighthDuration);

        beatsThisMeasure++;
        if (beatsThisMeasure >= beatsInMeasure)
        {
            Measure();
        }

    }

    void Measure()
    {
        measure.Invoke();

        beatsThisMeasure = 0;
    }

    void Eighth()
    {
        eighth.Invoke();
    }

}
