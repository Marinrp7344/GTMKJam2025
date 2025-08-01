using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeSlot : MonoBehaviour
{
    public Upgrade currentUpgrade;
    public AvailableWeaponSlotUI.WeaponType weaponType;
    public GameObject weaponPrefab;
    public string title;
    public string description;
    [SerializeField]private Sprite icon;
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    public void UpdateSlot(Upgrade upgrade)
    {
        currentUpgrade = upgrade;
        weaponType = upgrade.weaponType;
        weaponPrefab = upgrade.weaponPrefab;
        title = upgrade.title;
        description = upgrade.desciption;
        icon = upgrade.icon;
        displayImage.sprite = icon;
    }

    public void UpdateDisplayUI()
    { 
        titleText.text = title;
        descriptionText.text = description;
    }

    public void ClearDisplayUI()
    {
        titleText.text = "";
        descriptionText.text = "";

    }

    public void ClearSlot()
    {
        currentUpgrade = null;
        weaponType = AvailableWeaponSlotUI.WeaponType.None;
        weaponPrefab = null;
        title = "";
        description = "";
        icon = null;
        displayImage.sprite = null;
    }
}
