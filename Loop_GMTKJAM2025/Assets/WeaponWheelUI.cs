using UnityEngine;

public class WeaponWheelUI : MonoBehaviour
{
    public WeaponSlotUI heldSlot;
    public WeaponSlotUI hoveringSlot;
    public int slots;
    public GameObject weaponSlot;
    public Transform weaponParent;
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
        }
    }
    public void SwapSlots()
    {
        if(heldSlot != null && hoveringSlot != null)
        {

        }
    }
}
