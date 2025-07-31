using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PlayerWeapon selectedWeapon;

    [SerializeField] int budget = 100;

    public static WeaponManager Singleton;
    private void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }
    }

    public void SelectWeapon(PlayerWeapon weapon)
    {
        selectedWeapon = weapon;
    }

    public void ProcessBeatButtonPress(Beat beat)
    {
        Debug.Log($"beat button pressed with beat {beat}");

        if (selectedWeapon == null) { return; }

        // tries to remove the beat if its already registered to that weapon
        // tries to add the beat if it's not registered as a firingbeat for that weapon
        if (selectedWeapon.firingBeats.Contains(beat))
        {
            TryRemoveFiringBeat(beat);
        }
        else { TryAddFiringBeat(beat); }

    }

    public void TryAddFiringBeat(Beat beat)
    {
        if (selectedWeapon == null) { return; }

        if (CanAfford(selectedWeapon.cost) && !selectedWeapon.firingBeats.Contains(beat))
        {
            selectedWeapon.firingBeats.Add(beat);
            budget -= selectedWeapon.cost;
        }
    }

    public void TryRemoveFiringBeat(Beat beat)
    {
        if (selectedWeapon == null) { return; }

        if (selectedWeapon.firingBeats.Contains(beat))
        {
            selectedWeapon.firingBeats.Remove(beat);
            budget += selectedWeapon.cost;
        }

    }

    bool CanAfford(int cost)
    {
        if (budget >= cost)
        { return true; }
        else { return false; }
    }

}
