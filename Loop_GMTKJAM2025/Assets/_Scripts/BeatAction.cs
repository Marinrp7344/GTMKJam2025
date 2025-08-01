using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class BeatAction : MonoBehaviour
{
    public Composer composer;
    [SerializeField] List<Beat> firingBeats;
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

    public bool HasFiringBeat(Beat beat)
    {
        bool hasBeat = false;

        foreach (Beat firingBeat in firingBeats)
        {
            if (firingBeat.Equals(beat)) { hasBeat = true; break; }
        }

        return hasBeat;
    }

    /// <summary>
    /// removes a specified beat from the firing beats list. returns true if successfully removed, false if unsuccessful
    /// </summary>
    /// <param name="beat"></param>
    /// <returns></returns>
    public bool RemoveFiringBeat(Beat beat)
    {
        bool beatRemoved = false;

        if (!HasFiringBeat(beat)) { return beatRemoved; }

        // remove the beat
        foreach (Beat firingBeat in firingBeats)
        {
            if (firingBeat.Equals(beat)) 
            { 
                firingBeats.Remove(beat); 
                beatRemoved = true;
                break;
            }
        }

        return beatRemoved;
    }

    /// <summary>
    /// tries to add a beat to the firing beat list. returns true if successful, false if unsuccessful
    /// </summary>
    /// <param name="beat"></param>
    /// <returns></returns>
    public bool AddFiringBeat(Beat beat)
    {
        bool beatAdded = false;

        // beat already in firing beats
        if (HasFiringBeat(beat)) { return beatAdded; }

        if (!HasFiringBeat(beat))
        {
            firingBeats.Add(beat);
            // beat added successfully
            beatAdded = true;
        }

        return beatAdded;

    }

    public void ClearFiringBeats()
    {
        firingBeats.Clear();
    }

}
