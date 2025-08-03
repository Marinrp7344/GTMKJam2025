using UnityEngine;

public class PlayerWeapon : BeatAction
{
    [Space]
    public int cost = 10;
    public string weaponName = "weapon";
    public Sprite icon;
    public AudioClip sound;

    public uint maxBeats;
    public uint availableBeats;

    protected override void Start()
    {
        base.Start();
        availableBeats = maxBeats;
    }

    public void PlayFiringSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f);
    }
}
