using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource eventAudioSource;

    [SerializeField] private Sound[] audioClips;

    public enum SoundName
    {
        GamePlayBGM,
        UIbgm,
        ButtonClick,
        BouncePad,
        ChestOpen,
        DoorOpen,
        SwordSlash,
        HurtSound
    }

    [System.Serializable]
    public class Sound
    {
        public SoundName name;
        public AudioClip clip;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        bgAudioSource.enabled = true;
        eventAudioSource.enabled = true;
    }
    public void PlaySound(SoundName name)
    {
        foreach (var item in audioClips)
        {
            if (item.name == name)
            {
                eventAudioSource.PlayOneShot(item.clip);
                break;
            }
        }
    }
    private AudioClip GetAudioClip(SoundName name)
    {
        foreach (var item in audioClips)
        {
            if (item.name == name)
            {
                return item.clip;
            }
        }
        return null;
    }
    public void SoundMute(bool value) { eventAudioSource.mute = value; }

    public void OnButtonClick() { PlaySound(SoundName.ButtonClick); }

    public void OnBouncePadSound() { PlaySound(SoundName.BouncePad); }

    public void OnDoorOpenSound() { PlaySound(SoundName.DoorOpen); }

    public void OnChestOpenSound() { PlaySound(SoundName.ChestOpen); }

    public void OnUIScreenOpened() { bgAudioSource.clip = GetAudioClip(SoundName.UIbgm); bgAudioSource.Play(); }

    public void OnGamePlayScreen() { bgAudioSource.clip = GetAudioClip(SoundName.GamePlayBGM); bgAudioSource.Play(); }

    public void OnAttackButtonClicked() { PlaySound(SoundName.SwordSlash); }

    public void OnHurt() { PlaySound(SoundName.HurtSound); }

    public void OnVolumeOn(bool value) { if (value) eventAudioSource.mute = false; else eventAudioSource.mute = true; }

    public void OnMusicOn(bool value) { if (value) bgAudioSource.mute = false; else bgAudioSource.mute = true; }

    public void OnGameOverSound()
    {
        bgAudioSource.enabled = false;
        StartCoroutine(Timer(1.5f));
    }
    private IEnumerator Timer(float seconds) // Coroutine For Giving Delay Of Certain Seconds
    {
        yield return new WaitForSecondsRealtime(seconds);
        eventAudioSource.enabled = false;
    }
}