using UnityEngine;
using System.Collections.Generic;
public class UpdgradeMenu : MonoBehaviour
{
    [SerializeField] private List<Upgrade> upgrades;
    [SerializeField] private List<UpgradeSlot> upgradeSlots;
    public bool updateSlots;
    public AvailableWeaponsManager availableWeaponsManager;
    public GameObject weaponsUI;
    private void Start()
    {
        ShowNewUpgrades();
    }

    public void ShowNewUpgrades()
    {
        ClearSlots();
        FillWithRandomUpgrades();
    }

    private void ClearSlots()
    {
        foreach (UpgradeSlot slot in upgradeSlots)
        {
            slot.ClearSlot();
        }
    }

    public void ChooseUpgrade(int slotIndex)
    {
        availableWeaponsManager.AddWeapon(upgradeSlots[slotIndex].currentUpgrade);

        gameObject.SetActive(false);
        weaponsUI.SetActive(true);
    }

    public void FillWithRandomUpgrades()
    {
        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            int chosenRandomUpgrade = Random.Range(0, upgrades.Count);
            bool unchosenUpgrade = true;
            for (int j = 0; j < upgradeSlots.Count; j++)
            {
                if(upgradeSlots[j].currentUpgrade == upgrades[chosenRandomUpgrade])
                {
                    unchosenUpgrade = false;
                }
            }

            if(unchosenUpgrade)
            {
                upgradeSlots[i].UpdateSlot(upgrades[chosenRandomUpgrade]);
            }
            else
            {
                i -= 1;
            }
        }
    }
}

[System.Serializable]
public class Upgrade
{
    public AvailableWeaponSlotUI.WeaponType weaponType;
    public GameObject weaponPrefab;
    public string title;
    public string desciption;
    public Sprite icon;
}
