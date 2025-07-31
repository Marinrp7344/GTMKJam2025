using UnityEngine;
using UnityEngine.EventSystems;
public class WeaponSlotUI : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler,IPointerExitHandler
{
    public WeaponWheelUI weaponManager;
    public AvailableWeaponSlotUI.WeaponType weaponType;
    public GameObject tiedWeapon;
    public Sprite spriteIcon;
    public Transform weaponWheel;
    public int weaponAngle;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        weaponManager.heldSlot = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        weaponManager.hoveringSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        weaponManager.hoveringSlot = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        weaponManager.SwapSlots();
        weaponManager.heldSlot = null;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    public void SetWeaponSlot(WeaponSlotUI weaponSlot)
    {
        weaponType = weaponSlot.weaponType;
        tiedWeapon = weaponSlot.tiedWeapon;
        spriteIcon = weaponSlot.spriteIcon;
        if (tiedWeapon != null)
        {
            UpdateTiedWeapon();
        }
    }

    public void UpdateTiedWeapon()
    {
        tiedWeapon.transform.localRotation = Quaternion.Euler(0, 0, weaponAngle);
    }


    public void AddWeapon(AvailableWeaponSlotUI weapon)
    {
        Debug.Log("Adding Weapon");
        weaponType = weapon.weaponType;
        spriteIcon = weapon.weaponSprite;
        tiedWeapon = Instantiate(weapon.weapon, weaponWheel.position, Quaternion.identity);
        tiedWeapon.transform.SetParent(weaponWheel);
        UpdateTiedWeapon();
    }
}
