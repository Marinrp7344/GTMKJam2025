using UnityEngine;

public class PlayerWeapon : BeatAction
{
    [Space]
    public int cost = 10;
    public string weaponName = "weapon";
    public Sprite icon;
    public AudioClip sound;

    public void PlayFiringSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f);
    }
}
