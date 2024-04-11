using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Image volumeBtnImage;
    [SerializeField] private Sprite volumeOnBtn;
    [SerializeField] private Sprite volumeOffBtn;

    [SerializeField] private Image musicBtnImage;
    [SerializeField] private Sprite musicOnBtn;
    [SerializeField] private Sprite musicOffBtn;

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
        HurtSound,
        HealSound,
        DeathSound
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

    public void OnUIScreenOpened() { if (bgAudioSource.enabled) { bgAudioSource.clip = GetAudioClip(SoundName.UIbgm); bgAudioSource.Play(); } }

    public void OnGamePlayScreen() { if (bgAudioSource.enabled) { bgAudioSource.clip = GetAudioClip(SoundName.GamePlayBGM); bgAudioSource.Play(); } }

    public void OnAttackButtonClicked() { PlaySound(SoundName.SwordSlash); }

    public void OnHurt() { PlaySound(SoundName.HurtSound); }

    public void OnHeal() { PlaySound(SoundName.HealSound); }

    public void OnVolumeOn(bool value, float sliderValue) { if (value) eventAudioSource.volume = sliderValue; else eventAudioSource.volume = 0f; }

    public void OnMusicOn(bool value, float sliderValue) { if (value) bgAudioSource.volume = sliderValue; else bgAudioSource.volume = 0; }

    public void ChangeMusicVolume(float musicVolume)
    {
        bgAudioSource.volume = musicVolume;
        if (bgAudioSource.volume == 0) musicBtnImage.sprite = musicOffBtn;
        else musicBtnImage.sprite = musicOnBtn;
    }
    public void ChangeGameplaySoundVolume(float soundVolume)
    {
        eventAudioSource.volume = soundVolume;
        if (eventAudioSource.volume == 0) volumeBtnImage.sprite = volumeOffBtn;
        else volumeBtnImage.sprite = volumeOnBtn;
    }
    public void OnGameOverSound() // When Player Dies Or Game Is Over
    {
        PlaySound(SoundName.DeathSound);
        bgAudioSource.enabled = false;
        StartCoroutine(Timer(GetAudioClip(SoundName.DeathSound).length));
    }
    private IEnumerator Timer(float seconds) // Coroutine For Giving Delay Of Certain Seconds
    {
        yield return new WaitForSecondsRealtime(seconds);
        eventAudioSource.enabled = false;
    }
}