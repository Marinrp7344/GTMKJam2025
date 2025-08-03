using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatMaker : MonoBehaviour
{
    PlayerWeapon weapon;
    [SerializeField] GameObject beatButtonPrefab;
    [SerializeField] LayoutGroup layout;
    [SerializeField] Image icon;
    [SerializeField] GameObject beatCheckmarkPrefab;
    [SerializeField] GridLayoutGroup availableBeatsLayout;

    bool sixteenthsUnlocked = false;


    public void Initialize(PlayerWeapon weapon)
    {

        this.weapon = weapon;

        icon.sprite = weapon.icon;

        CreateBeatButtons(this.weapon.composer.measureCount * Metronome.Singleton.quartersPerMeasure * 4);

        UpdateAvailableBeats(true);
    }

    // bool is only here because you cant listen to the toggle without it
    void UpdateAvailableBeats(bool toggleResult)
    {
        foreach (Transform transform in availableBeatsLayout.GetComponentsInChildren<Transform>())
        {
            if (transform != availableBeatsLayout.transform)
            {
                Destroy(transform.gameObject);
            }
        }

        for (int i = 0; i < weapon.availableBeats; i++)
        {
            Instantiate(beatCheckmarkPrefab, availableBeatsLayout.transform);
        }
    }

    /// <summary>
    /// spawns all beat buttons for a given weapon
    /// </summary>
    /// <param name="count"></param>
    void CreateBeatButtons(uint count)
    {
        Beat beatToSpawn = new Beat(1, 1, 1, 1);

        for (uint i = 0; i < count; i++)
        {
            SpawnBeatButton(beatToSpawn);
            beatToSpawn.Increment();
        }
    }


    void SpawnBeatButton(Beat beatToSpawn)
    {
        GameObject newButton = Instantiate(beatButtonPrefab, layout.transform);

        // set up beat button component fields
        BeatButton beatBtn = newButton.GetComponent<BeatButton>();
        beatBtn.Initialize(beatToSpawn, weapon);

        if (beatToSpawn.sixteenth % 2 == 0 && // if button is a sixteenth,
            !sixteenthsUnlocked) // if sixteenths are not unlocked
        {
            // disable sixteenth button
            newButton.SetActive(false);
        }

        newButton.GetComponentInChildren<Toggle>().onValueChanged.AddListener(UpdateAvailableBeats);
    }
}
