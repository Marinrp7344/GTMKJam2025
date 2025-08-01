using UnityEngine;
using System.Collections.Generic;
public class AvailableWeaponsManager : MonoBehaviour
{
    public List<AvailableWeaponSlotUI> availableWeaponSlots;
    
    public void AddWeapon(Upgrade upgrade)
    {
        foreach (AvailableWeaponSlotUI slot in availableWeaponSlots)
        {
            if(slot.weaponType == upgrade.weaponType)
            {
                slot.AddWeapon();
            }
        }
    }
}
