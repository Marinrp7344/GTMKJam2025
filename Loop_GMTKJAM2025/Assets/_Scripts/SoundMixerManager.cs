using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;

    private void Start()
    {
        // Load saved values or default to full volume
        float master = PlayerPrefs.GetFloat("master", 1f);
        float soundFX = PlayerPrefs.GetFloat("soundFX", 1f);
        float music = PlayerPrefs.GetFloat("music", 1f);

        SetMasterVolume(master);
        SetSoundFXVolume(soundFX);
        SetMusicVolume(music);
    }

    public void SetMasterVolume(float level)
    {
        PlayerPrefs.SetFloat("master", level); // Save linear value
        audiomixer.SetFloat("masterVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f);
    }

    public void SetSoundFXVolume(float level)
    {
        PlayerPrefs.SetFloat("soundFX", level);
        audiomixer.SetFloat("soundFXVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        PlayerPrefs.SetFloat("music", level);
        audiomixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Max(level, 0.0001f)) * 20f);
    }
}
