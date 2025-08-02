using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class AvailableWeaponSlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum WeaponType { None, RocketLauncher, MachineGun, Shotgun }
    public WeaponType weaponType;
    public WeaponWheelUI weaponManager;
    public int availableAmountOfWeapons;
    public Sprite weaponSprite;
    public GameObject weapon;
    public TextMeshProUGUI amountText;

    private void Start()
    {
        UpdateText();
    }
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
            //weaponManager.AddWeaponToSlot();
            weaponManager.heldAvailableWeapon = null;
        }
    }

    public void AddedWeaponToInventory()
    {
        availableAmountOfWeapons -= 1;
        UpdateText();
    }

    public void AddWeapon()
    {
        availableAmountOfWeapons += 1;
        UpdateText();
    }

    public void UpdateText()
    {
        amountText.text = availableAmountOfWeapons.ToString();
    }
}
