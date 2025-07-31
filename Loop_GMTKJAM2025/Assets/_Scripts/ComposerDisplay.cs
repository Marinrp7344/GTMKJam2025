using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposerDisplay : MonoBehaviour
{
    [SerializeField] Composer composer;
    [Space]
    [SerializeField] GameObject measureIconPrefab;
    [SerializeField] GameObject quarterIconPrefab;
    [SerializeField] GameObject eighthIconPrefab;
    [Space]
    [SerializeField] float beatHighlightDuration = .1f;


    List<GameObject> beatIcons = new List<GameObject>();

    private void Start()
    {
        CreateBeatIcons(composer.measureCount * Metronome.Singleton.quartersInMeasure * 2);

        Metronome.Singleton.beat.AddListener(HighlightCurrentBeat);
    }

    void CreateBeatIcons(uint count)
    {
        Beat beatToSpawn = new Beat(1,1,1);

        for (uint i = 0; i < count; i++)
        {
            SpawnBeatIcon(beatToSpawn);
            beatToSpawn.Increment();
        }
    }

    private void SpawnBeatIcon(Beat currentBeat)
    {
        GameObject iconPrefabToSpawn = null;

        // figure out what icon to spawn
        if (currentBeat.quarter == 1 &&
            currentBeat.eighth == 1)
        {
            iconPrefabToSpawn = measureIconPrefab;
        }
        else if (currentBeat.eighth % 2 == 0)
        {
            iconPrefabToSpawn = eighthIconPrefab;
        }
        else { iconPrefabToSpawn = quarterIconPrefab; }

        // spawn icon
        if (iconPrefabToSpawn != null)
        {
            GameObject newIcon = Instantiate(iconPrefabToSpawn, transform);
            beatIcons.Add(newIcon);
        }
    }

    void HighlightCurrentBeat()
    {
        HighlightBeat(composer.currentBeat);
    }

    void HighlightBeat(Beat beat)
    {
        int indexOfBeat = 8 * (beat.measure - 1) + beat.eighth - 1;
        beatIcons[indexOfBeat].GetComponent<Image>().color = Color.green;


        Invoke(nameof(ClearBeatHighlight), beatHighlightDuration);
    }

    void ClearBeatHighlight()
    {
        foreach (GameObject icon in beatIcons)
        {
            icon.GetComponent<Image>().color = Color.white;
        }
    }


}
