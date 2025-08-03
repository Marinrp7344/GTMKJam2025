using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
struct SongData
{
    public AudioClip song;
    public float bpm;
}

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<SongData> playlist = new List<SongData>();

    [SerializeField] uint stagesPerSong = 3;

    uint stagesClearedSinceLastSongChange = 0;

    private void Start()
    {
        SpawningManager.Instance.stageClear.AddListener(ProcessStageClear);
        Invoke(nameof(StartRandomSong), 3);
    }

    SongData PickRandomSong()
    {
        int index = Random.Range(0, playlist.Count);
        return playlist[index];
    }

    void StartSong(SongData song)
    {
        Metronome.Singleton.StartMusic(song.song, song.bpm);
    }

    void StartRandomSong()
    {
        StartSong(PickRandomSong());
    }

    void ProcessStageClear()
    {
        stagesClearedSinceLastSongChange++;
        if (stagesClearedSinceLastSongChange >= stagesPerSong)
        {
            stagesClearedSinceLastSongChange = 0;
            StartRandomSong();
        }
    }

}
