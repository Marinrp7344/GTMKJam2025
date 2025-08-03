using UnityEngine;

public class PlayerWeapon : BeatAction
{
    [Space]
    public string weaponName = "weapon";
    public Sprite icon;
    public AudioClip sound;

    public uint maxBeats;
    [HideInInspector]
    public uint availableBeats;

    private void Awake()
    {
        availableBeats = maxBeats;
    }

    public void PlayFiringSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f);
    }
}
