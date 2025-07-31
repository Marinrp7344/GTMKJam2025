using UnityEngine;

public class WeaponWheelUI : MonoBehaviour
{
    public WeaponSlotUI heldSlot;
    public WeaponSlotUI hoveringSlot;
    public int slots;
    public GameObject weaponSlot;
    public Transform weaponParent;
    public Transform physicalWeaponWheel;
    public AvailableWeaponSlotUI heldAvailableWeapon;
    public void Start()
    {
        CreateWeaponSlots();

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
            slot.GetComponent<WeaponSlotUI>().weaponManager = this;
            slot.GetComponent<WeaponSlotUI>().weaponAngle = rotationAmount * i;
            slot.GetComponent<WeaponSlotUI>().weaponWheel = physicalWeaponWheel;
        }
    }
    public void SwapSlots()
    {
        if(heldSlot != null && hoveringSlot != null)
        {
            WeaponSlotUI tempSlot = new WeaponSlotUI();
            tempSlot.SetWeaponSlot(heldSlot);
            heldSlot.SetWeaponSlot(hoveringSlot);
            hoveringSlot.SetWeaponSlot(tempSlot);
            Destroy(tempSlot);

            heldSlot.UpdateSprite();
            hoveringSlot.UpdateSprite();
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
