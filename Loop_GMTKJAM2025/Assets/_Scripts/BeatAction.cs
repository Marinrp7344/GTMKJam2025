using UnityEngine;
using System.Collections.Generic;

public class BeatAction : MonoBehaviour
{
    public Composer composer;
    public List<Beat> firingBeats;

    private void Start()
    {
        // tries to activate every beat. if is supposed to activate on current beat, activates
        Metronome.Singleton.beat.AddListener(TryActivate);
    }

    public void TryActivate()
    {
        foreach (Beat firingBeat in firingBeats)
        {
            if (firingBeat.Equals(composer.currentBeat))
            {
                Activate();
                return; // stop processing once it activates
            }
        }
    }

    protected virtual void Activate()
    {
    }

}
