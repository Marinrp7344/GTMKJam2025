using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;

    private void Start()
    {
        float master = 0f;
        float soundFX = 0f;
        float music = 0f;
        if (PlayerPrefs.HasKey("master"))
        {
            // Load saved values or default to full volume
             master = PlayerPrefs.GetFloat("master", 1f);
             soundFX = PlayerPrefs.GetFloat("soundFX", 1f);
             music = PlayerPrefs.GetFloat("music", 1f);
        }
        else
        {
            // Load saved values or default to full volume
            master = PlayerPrefs.GetFloat("master", .5f);
            soundFX = PlayerPrefs.GetFloat("soundFX", .5f);
            music = PlayerPrefs.GetFloat("music", .5f);
        }

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
