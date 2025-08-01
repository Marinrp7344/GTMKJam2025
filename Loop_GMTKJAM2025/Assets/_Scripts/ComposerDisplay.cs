using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]

public class ComposerDisplay : MonoBehaviour
{
    [SerializeField] Composer composer;
    [Space]
    [SerializeField] GameObject measurePrefab;
    [SerializeField] GameObject quarterPrefab;
    [SerializeField] GameObject eighthPrefab;
    [SerializeField] GameObject sixteenthPrefab;
    [Space]
    bool sixteenthsUnlocked = false;
    [SerializeField] float sixteenthLayoutSpacing = 10;
    [SerializeField] float beatHighlightDuration = .1f;// TODO: replace with animation call


    List<GameObject> beatIcons = new List<GameObject>();

    private void Start()
    {
        CreateBeatIcons(composer.measureCount * Metronome.Singleton.quartersPerMeasure * 4);

        // highlight beat after lowest length beat goes off (to ensure up-to-date information)
        Metronome.Singleton.sixteenthLate.AddListener(HighlightCurrentBeat);
    }

    void CreateBeatIcons(uint count)
    {
        Beat beatToSpawn = new Beat(1,1,1,1);

        for (uint i = 0; i < count; i++)
        {
            SpawnBeatIcon(beatToSpawn);
            beatToSpawn.Increment();
        }
    }

    private void SpawnBeatIcon(Beat beatToSpawn)
    {
        GameObject iconPrefabToSpawn = null;

        // figure out what icon to spawn

        // start of new measure, spawn measure icon
        if (beatToSpawn.quarter == 1 &&
            beatToSpawn.eighth == 1 &&
            beatToSpawn.sixteenth == 1)
        {
            iconPrefabToSpawn = measurePrefab;
        }

        // if sixteenth is even, spawn sixteenth
        else if (beatToSpawn.sixteenth % 2 == 0)
        {
            iconPrefabToSpawn = sixteenthPrefab;
        }

        // if eighth is even, spawn eighth
        else if (beatToSpawn.eighth % 2 == 0)
        {
            iconPrefabToSpawn = eighthPrefab;
        }

        // if not a new mesure, and eighth and sixteenth are odd, spawn quarter
        else { iconPrefabToSpawn = quarterPrefab; }



        // spawn icon
        if (iconPrefabToSpawn != null)
        {
            GameObject newIcon = Instantiate(iconPrefabToSpawn, transform);
            beatIcons.Add(newIcon);

            newIcon.GetComponent<BeatButton>().beat = beatToSpawn;


            // if its a sixteenth, disable icon if sixteenths are not unlocked
            if (iconPrefabToSpawn == sixteenthPrefab &&
                !sixteenthsUnlocked)
            {
                newIcon.gameObject.SetActive(false);
            }
        }
    }

    void HighlightCurrentBeat()
    {
        HighlightBeat(composer.currentBeat);
    }

    void HighlightBeat(Beat beat)
    {
        // do nothing if measure is not valid
        if (beat.measure < 1) { return; }

        int indexOfBeat = ((int)Metronome.Singleton.quartersPerMeasure * 4) * (beat.measure - 1) + beat.sixteenth - 1;

        // do nothing if beat icon is disabled
        if (!beatIcons[indexOfBeat].gameObject.activeSelf) { return; }

        beatIcons[indexOfBeat].GetComponent<Animator>().Play("Pulse");
    }

    void ClearBeatHighlight()
    {
        foreach (GameObject icon in beatIcons)
        {
            icon.GetComponent<Image>().color = Color.white;
        }
    }

    [ContextMenu("unlock sixteenths")]
    void UnlockSixteenths()
    {
        sixteenthsUnlocked = true;

        // set all icons as active (will need to change later IF we add even smaller notes... unlikely)
        foreach(GameObject icon in beatIcons)
        {
            icon.gameObject.SetActive(true);
        }

        GetComponent<HorizontalLayoutGroup>().spacing = sixteenthLayoutSpacing;
    }

}
