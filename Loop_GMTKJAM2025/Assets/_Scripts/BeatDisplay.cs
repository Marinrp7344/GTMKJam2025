using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BeatDisplay : MonoBehaviour
{
    [SerializeField] GameObject beatIconPrefab;
    [SerializeField] float beatHighlightDuration = .1f;

    public int currentBeat = 0;

    List<GameObject> beatIcons = new List<GameObject>();

    public void CreateBeatIcons(uint count)
    {
        for (uint i = 0; i < count; i++)
        {
            GameObject newIcon = Instantiate(beatIconPrefab, transform);
            beatIcons.Add(newIcon);
        }
    }

    public void NextBeat()
    {
        ClearBeatHighlight();

        currentBeat++;
        if (currentBeat > beatIcons.Count) { currentBeat = 1; }
        beatIcons[currentBeat - 1].GetComponent<Image>().color = Color.green;

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
