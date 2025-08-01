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
    public bool isTrashSlot;

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
        if (isTrashSlot)
        {
            tiedWeapon.SetActive(false);
        }
        else
        {
            tiedWeapon.SetActive(true);
        }
    }
    public void UpdateSprite()
    {
        slotImage.sprite = spriteIcon;
    }

    public void ClearSlot()
    {
        weaponType = AvailableWeaponSlotUI.WeaponType.None;
        spriteIcon = null;
        if(tiedWeapon != null)
        {
            Destroy(tiedWeapon);
        }
    }

    public void AddWeapon(Upgrade weapon)
    {
        Debug.Log("Adding Weapon");
        weaponType = weapon.weaponType;
        spriteIcon = weapon.icon;
        tiedWeapon = Instantiate(weapon.weaponPrefab, weaponManager.physicalWeaponWheel.position, Quaternion.identity);
        PlayerWeapon tiedWeaponBeatAction = tiedWeapon.GetComponent<PlayerWeapon>();
        tiedWeaponBeatAction.composer = weaponManager.composer;

        if (weaponType != AvailableWeaponSlotUI.WeaponType.Laser)
        {
            Shoot tiedWeaponShoot = tiedWeapon.GetComponent<Shoot>();
            tiedWeaponShoot.player = weaponManager.player;
        }

        tiedWeapon.transform.SetParent(weaponManager.physicalWeaponWheel);
        UpdateTiedWeapon();

        if (spriteIcon != null)
        {
            UpdateSprite();
        }

        weaponManager.UpdateWeaponList();
    }
}
