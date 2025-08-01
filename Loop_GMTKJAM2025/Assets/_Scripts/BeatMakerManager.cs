using UnityEngine;
using UnityEngine.UI;

public class BeatMakerManager : MonoBehaviour
{
    [SerializeField] LayoutGroup layout;
    [SerializeField] GameObject beatMakerPrefab;
    
    [ContextMenu("spawn beat maker menu")]
    void SpawnBeatMakerMenu()
    {
        foreach(PlayerWeapon weapon in WeaponWheelUI.Instance.spawnedWeapons)
        {
            BeatMaker newBeatMaker = Instantiate(beatMakerPrefab, layout.transform).GetComponent<BeatMaker>();
            newBeatMaker.Initialize(weapon);
        }
    }

    
}
