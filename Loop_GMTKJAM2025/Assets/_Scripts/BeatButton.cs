using UnityEngine;

public class BeatButton : MonoBehaviour
{

    public Beat beat;

    WeaponManager weaponManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponManager = WeaponManager.Singleton;
    }

    public void BeatButtonPress()
    {
        weaponManager.ProcessBeatButtonPress(beat);
    }

}
