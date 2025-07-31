using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
public class WeaponSlotUI : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler,IPointerExitHandler
{

    public WeaponWheelUI weaponManager;
    public AvailableWeaponSlotUI.WeaponType weaponType;
    public GameObject tiedWeapon;
    public Sprite spriteIcon;
    public Transform weaponWheel;
    public Image slotImage;
    public int weaponAngle;
    public bool hovering;
    public bool clicked;
    public bool selected;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(hovering)
        {
            clicked = true;
        }
        weaponManager.heldSlot = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        weaponManager.hoveringSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        weaponManager.hoveringSlot = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(hovering && clicked && weaponType != AvailableWeaponSlotUI.WeaponType.None)
        {
            weaponManager.SelectSlot(this);
            selected = true;
            weaponManager.UnselectWeapons(this);
        }
        weaponManager.SwapSlots();
        weaponManager.heldSlot = null;
        
    }


    public void SetWeaponSlot(WeaponSlotUI weaponSlot)
    {
        weaponType = weaponSlot.weaponType;
        tiedWeapon = weaponSlot.tiedWeapon;
        spriteIcon = weaponSlot.spriteIcon;
        selected = weaponSlot.selected;

        if (selected && weaponManager != null) 
        {
            weaponManager.SelectSlot(this);
            weaponManager.UnselectWeapons(this);
        }


        if (tiedWeapon != null)
        {
            UpdateTiedWeapon();
        }
        
    }

    public void UpdateTiedWeapon()
    {
        tiedWeapon.transform.localRotation = Quaternion.Euler(0, 0, weaponAngle);
    }
    public void UpdateSprite()
    {
        slotImage.sprite = spriteIcon;
    }


    public void AddWeapon(AvailableWeaponSlotUI weapon)
    {
        Debug.Log("Adding Weapon");
        weaponType = weapon.weaponType;
        spriteIcon = weapon.weaponSprite;
        tiedWeapon = Instantiate(weapon.weapon, weaponWheel.position, Quaternion.identity);
        tiedWeapon.transform.SetParent(weaponWheel);
        UpdateTiedWeapon();
        if (spriteIcon != null)
        {
            UpdateSprite();
        }
    }
}
