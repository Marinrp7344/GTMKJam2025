using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelUI : MonoBehaviour
{
    public static WeaponWheelUI Instance;
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
    public List<PlayerWeapon> spawnedWeapons;
    public WeaponSlotUI trashWeaponSlot;
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        composer = PlayerComponents.Instance.playerComposer;
        physicalWeaponWheel = PlayerComponents.Instance.wheel.transform;

        CreateWeaponSlots();
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        BeatMakerManager.Singleton.ClearBeatMakerMenu();
        trashWeaponSlot.ClearSlot();
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
            weaponSlots.Add(slot.GetComponent<WeaponSlotUI>());
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
        UpdateWeaponList();
    }
    public void UpdateWeaponList()
    {
        spawnedWeapons = new List<PlayerWeapon>();
       
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (weaponSlots[i].tiedWeapon != null)
            {
                spawnedWeapons.Add(weaponSlots[i].tiedWeapon.GetComponent<PlayerWeapon>());
            }

        }
        BeatMakerManager.Singleton.SpawnBeatMakerMenu();
    }

    public void AddWeaponToSlot(Upgrade upgrade)
    {
        bool foundSlot = false;
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if(weaponSlots[i].weaponType == AvailableWeaponSlotUI.WeaponType.None)
            {
                Debug.Log("Found a normal Slot");
                weaponSlots[i].AddWeapon(upgrade);
                foundSlot = true;
                break;
            }
        }

        if (!foundSlot)
        {
            Debug.Log("Sent to trash");
            trashWeaponSlot.AddWeapon(upgrade);
        }
        
    }
}
