using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class BeatMakerManager : MonoBehaviour
{
    public static BeatMakerManager Singleton;
    [SerializeField] LayoutGroup layout;
    [SerializeField] GameObject beatMakerPrefab;

    private void Awake()
    {
        Singleton = this;    
    }

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
        List<BeatMaker> beatMakers = GetComponentsInChildren<BeatMaker>().ToList();

        // clear all beat makers
        while (beatMakers.Any())
        {
            BeatMaker beatMakerToDelete = beatMakers.First();
            beatMakers.Remove(beatMakerToDelete);
            Destroy(beatMakerToDelete.gameObject);
        }
    }

    
}
