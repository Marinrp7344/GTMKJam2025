using UnityEngine;

public class BeatAction : MonoBehaviour
{
    Composer composer;
    Beat firingBeat;

    /// <summary>
    /// sets the BeatAction's composer and firing beat, so it can fire on the right beat
    /// </summary>
    /// <param name="composer"></param>
    /// <param name="firingBeat"></param>
    public void Construct(Composer composer, Beat firingBeat)
    {
        this.composer = composer;
        this.firingBeat = firingBeat;
    }

    public void TryActivate()
    {
        if (composer.currentBeat.Equals(firingBeat))
        {
            Activate();
        }
    }

    protected virtual void Activate()
    {

    }

}
