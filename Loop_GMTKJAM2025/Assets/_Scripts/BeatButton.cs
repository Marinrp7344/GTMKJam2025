using UnityEngine;
using UnityEngine.UI;

public class BeatButton : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    Beat beat;
    PlayerWeapon weapon;

    WeaponManager weaponManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponManager = WeaponManager.Singleton;
    }

    public void Initialize(Beat beat, PlayerWeapon weapon)
    {
        this.beat = beat;
        this.weapon = weapon;

        toggle.isOn = weapon.HasFiringBeat(this.beat);
    }


    public void ToggleFiringBeat(bool on)
    {
        // toggle on, add firing beat
        if (on && !weapon.HasFiringBeat(beat))
        {
            weaponManager.TryAddFiringBeat(beat, weapon);
        }
        // toggle off, remove firing beat
        else if (!on && weapon.HasFiringBeat(beat))
        {
            weaponManager.TryRemoveFiringBeat(beat, weapon);
        }
    }

}
