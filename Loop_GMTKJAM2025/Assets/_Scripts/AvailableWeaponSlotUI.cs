using UnityEngine;
using UnityEngine.EventSystems;
public class AvailableWeaponSlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum WeaponType { None, RocketLauncher, MachineGun, Shotgun }
    public WeaponType weaponType;
    public WeaponWheelUI weaponManager;
    public int availableAmountOfWeapons;
    public Sprite weaponSprite;
    public GameObject weapon;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (availableAmountOfWeapons > 0)
        {
            weaponManager.heldAvailableWeapon = this;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (availableAmountOfWeapons > 0)
        {
            weaponManager.AddWeaponToSlot();
            weaponManager.heldAvailableWeapon = null;
        }
    }

    public void AddedWeaponToInventory()
    {
        availableAmountOfWeapons -= 1;
    }

    public void AddWeapon()
    {
        availableAmountOfWeapons += 1;
    }
}
