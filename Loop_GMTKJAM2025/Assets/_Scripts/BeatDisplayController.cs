using UnityEngine;

public class BeatDisplayController : MonoBehaviour
{
    [SerializeField] Composer composer;

    [SerializeField] BeatDisplay measureDisplay;
    [SerializeField] BeatDisplay quarterDisplay;
    [SerializeField] BeatDisplay eighthDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        measureDisplay.CreateBeatIcons(composer.measureCount);
        measureDisplay.currentBeat = composer.currentBeat.measure;

        quarterDisplay.CreateBeatIcons(composer.measureCount * Metronome.Singleton.quartersInMeasure);
        quarterDisplay.currentBeat = composer.currentBeat.quarter;

        eighthDisplay.CreateBeatIcons(composer.measureCount * Metronome.Singleton.quartersInMeasure * 2);
        eighthDisplay.currentBeat = composer.currentBeat.eighth;
    }
}
