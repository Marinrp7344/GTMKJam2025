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
        Metronome.Singleton.measure.RemoveListener(TryActivate);
        Metronome.Singleton.quarter.RemoveListener(TryActivate);
        Metronome.Singleton.eighth.RemoveListener(TryActivate);
        Metronome.Singleton.sixteenth.RemoveListener(TryActivate);

        // subscribe to beat events according to precision value
        switch (precision)
        {
            case Precision.measure:
                Metronome.Singleton.measure.AddListener(TryActivate);
                break;

            case Precision.quarter:
                Metronome.Singleton.quarter.AddListener(TryActivate);
                break;

            case Precision.eighth:
                Metronome.Singleton.eighth.AddListener(TryActivate);
                break;

            case Precision.sixteenth:
                Metronome.Singleton.sixteenth.AddListener(TryActivate);
                break;
        }
    }

    public void TryActivate()
    {
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
