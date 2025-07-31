using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class BeatAction : MonoBehaviour
{
    public Composer composer;
    public List<Beat> firingBeats;
    [Space]
    public UnityEvent Activate;

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
                Activate.Invoke();
                return; // stop processing once it activates
            }
        }
    }

}
