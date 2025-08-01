using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] int budget = 100;

    public static WeaponManager Singleton;
    private void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }
    }

    public void TryAddFiringBeat(Beat beat, PlayerWeapon weapon)
    {
        if (weapon == null) { return; }

        if (CanAfford(weapon.cost) && !weapon.HasFiringBeat(beat))
        {
            if (weapon.AddFiringBeat(beat) == true)
            {
                // deduct from budget if successfully added
                budget -= weapon.cost;
            }
        }
    }

    public void TryRemoveFiringBeat(Beat beat, PlayerWeapon weapon)
    {
        if (weapon == null) { return; }

        if (weapon.HasFiringBeat(beat))
        {
            if (weapon.RemoveFiringBeat(beat) == true)
            {
                // refund to budget if successfully removed
                budget += weapon.cost;
            }
        }

    }

    bool CanAfford(int cost)
    {
        if (budget >= cost)
        { return true; }
        else { return false; }
    }

    public int GetBudget()
    {
        return budget;
    }

}
