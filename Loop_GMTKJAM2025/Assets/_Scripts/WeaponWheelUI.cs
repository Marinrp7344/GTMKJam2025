using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelUI : MonoBehaviour
{
    public Transform player;
    public List<WeaponSlotUI> weaponSlots;
    public WeaponSlotUI selectedSlot;
    public WeaponSlotUI heldSlot;
    public WeaponSlotUI hoveringSlot;
    public int slots;
    public GameObject weaponSlot;
    public Transform weaponParent;
    public Transform physicalWeaponWheel;
    public AvailableWeaponSlotUI heldAvailableWeapon;
    public Composer composer;
    public void Start()
    {
        CreateWeaponSlots();
        player = gameObject.transform.parent.gameObject.transform;
    }
    public void CreateWeaponSlots()
    {
        int rotationAmount = 360 / slots;
        for (int i = 0; i < slots; i++)
        {
            GameObject slot = Instantiate(weaponSlot, transform.position, Quaternion.identity);
            slot.transform.SetParent(weaponParent);
            RectTransform slotTransform = slot.GetComponent<RectTransform>();
            slotTransform.rotation = Quaternion.Euler(0, 0, rotationAmount * i);
            slotTransform.localPosition = Vector2.zero;
            slot.GetComponent<WeaponSlotUI>().weaponManager = this;
            slot.GetComponent<WeaponSlotUI>().weaponAngle = rotationAmount * i;
            slot.GetComponent<WeaponSlotUI>().weaponWheel = physicalWeaponWheel;
        }
    }

    public void UnselectWeapons(WeaponSlotUI selectedWeapon)
    {
        foreach(WeaponSlotUI slot in weaponSlots)
        {
            if(selectedWeapon != selectedSlot)
            {
                slot.selected = false;
            }
        }
    }

    public void SelectSlot(WeaponSlotUI weaponSlot)
    {
        selectedSlot = weaponSlot;
        WeaponManager.Singleton.SelectWeapon(selectedSlot.tiedWeapon.GetComponent<PlayerWeapon>());
    }
    public void SwapSlots()
    {
        if (hoveringSlot != heldSlot)
        {


            if (heldSlot != null && hoveringSlot != null)
            {
                GameObject tempGO = new GameObject("TempSlot");
                WeaponSlotUI tempSlot = tempGO.AddComponent<WeaponSlotUI>();
                tempSlot.weaponManager = this;
                tempSlot.SetWeaponSlot(heldSlot);
                heldSlot.SetWeaponSlot(hoveringSlot);
                hoveringSlot.SetWeaponSlot(tempSlot);
                Destroy(tempGO);

                heldSlot.UpdateSprite();
                hoveringSlot.UpdateSprite();

            }
        }
    }

    public void AddWeaponToSlot()
    {
        if(hoveringSlot != null && hoveringSlot.weaponType == AvailableWeaponSlotUI.WeaponType.None)
        {
            hoveringSlot.AddWeapon(heldAvailableWeapon);
            heldAvailableWeapon.AddedWeaponToInventory();
        }
    }
}
