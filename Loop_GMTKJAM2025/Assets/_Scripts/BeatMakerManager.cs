using UnityEngine;
using UnityEngine.UI;

public class BeatMakerManager : MonoBehaviour
{
    [SerializeField] LayoutGroup layout;
    [SerializeField] GameObject beatMakerPrefab;
    
    [ContextMenu("spawn beat maker menu")]
    public void SpawnBeatMakerMenu()
    {
        ClearBeatMakerMenu();

        foreach(PlayerWeapon weapon in WeaponWheelUI.Instance.spawnedWeapons)
        {
            BeatMaker newBeatMaker = Instantiate(beatMakerPrefab, layout.transform).GetComponent<BeatMaker>();
            newBeatMaker.Initialize(weapon);
        }
    }

    public void ClearBeatMakerMenu()
    {
        // clear all beat makers
        while (GetComponentsInChildren<BeatMaker>().Length > 0)
        {
            Destroy(GetComponentInChildren<BeatMaker>().gameObject);
        }
    }

    
}
