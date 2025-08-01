using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatMaker : MonoBehaviour
{
    PlayerWeapon weapon;
    [SerializeField] GameObject beatButtonPrefab;
    [SerializeField] LayoutGroup layout;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI costText;

    bool sixteenthsUnlocked = false;


    public void Initialize(PlayerWeapon weapon)
    {

        this.weapon = weapon;

        icon.sprite = weapon.icon;
        costText.text = $"{weapon.cost}";

        CreateBeatButtons(this.weapon.composer.measureCount * Metronome.Singleton.quartersPerMeasure * 4);
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
    }
}
