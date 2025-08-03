using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public static WeaponManager Singleton;
    private void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }
    }

    public bool TryAddFiringBeat(Beat beat, PlayerWeapon weapon)
    {
        bool success = false;
        if (weapon == null) { return success; }

        if (weapon.availableBeats > 0 && !weapon.HasFiringBeat(beat))
        {
            if (weapon.AddFiringBeat(beat) == true)
            {
                // deduct from budget if successfully added
                weapon.availableBeats--;
                success = true;
            }
        }

        return success;
    }

    public bool TryRemoveFiringBeat(Beat beat, PlayerWeapon weapon)
    {
        bool success = false;
        if (weapon == null) { return success; }

        if (weapon.HasFiringBeat(beat))
        {
            if (weapon.RemoveFiringBeat(beat) == true)
            {
                // refund to budget if successfully removed
                weapon.availableBeats++;
                success = true;
            }
        }

        return success;

    }

}
