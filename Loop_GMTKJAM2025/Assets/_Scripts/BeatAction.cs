using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class BeatAction : MonoBehaviour
{
    public Composer composer;
    public List<Beat> firingBeats;
    [SerializeField] Precision precision;
    [Space]
    public UnityEvent Activate;

    private void Start()
    {
        // configure precision event logic
        SetPrecision(precision);
    }

    private void SetPrecision(Precision newPrecision)
    {
        precision = newPrecision;

        // clear any beat events
        Metronome.Singleton.measureLate.RemoveListener(TryActivate);
        Metronome.Singleton.quarterLate.RemoveListener(TryActivate);
        Metronome.Singleton.eighthLate.RemoveListener(TryActivate);
        Metronome.Singleton.sixteenthLate.RemoveListener(TryActivate);

        // subscribe to late beat events according to precision value
        // using LATE events to ensure composer has correct info when the beat action is checking current beat
        switch (precision)
        {
            case Precision.measure:
                Metronome.Singleton.measureLate.AddListener(TryActivate);
                break;

            case Precision.quarter:
                Metronome.Singleton.quarterLate.AddListener(TryActivate);
                break;

            case Precision.eighth:
                Metronome.Singleton.eighthLate.AddListener(TryActivate);
                break;

            case Precision.sixteenth:
                Metronome.Singleton.sixteenthLate.AddListener(TryActivate);
                break;
        }
    }

    public void TryActivate()
    {
        // do nothing if linked composer is not running
        if (!composer.running) { return; }

        foreach (Beat firingBeat in firingBeats)
        {
            if (firingBeat.Equals(composer.currentBeat))
            {
                Activate.Invoke();
                return; // stop processing once it activates
            }
        }
    }

}
