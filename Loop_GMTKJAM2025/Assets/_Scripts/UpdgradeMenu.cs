using UnityEngine;
using System.Collections.Generic;
public class UpdgradeMenu : MonoBehaviour
{
    public static UpdgradeMenu Instance;
    [SerializeField] private List<Upgrade> upgrades;
    [SerializeField] private List<UpgradeSlot> upgradeSlots;
    public bool updateSlots;
    private void Start()
    {
        ShowNewUpgrades();
        Instance = this;
    }


    public void StartNewRound()
    {
        gameObject.SetActive(true);
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
        AvailableWeaponsManager.Instance.AddWeapon(upgradeSlots[slotIndex].currentUpgrade);

        gameObject.SetActive(false);
        WeaponWheelUI.Instance.gameObject.SetActive(true);
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
