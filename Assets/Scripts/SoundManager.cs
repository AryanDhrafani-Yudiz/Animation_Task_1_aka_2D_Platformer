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
        BackgroundMusic,
        BouncePad,
        ChestOpen,
        DoorOpen
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
    public void SoundMute(bool val)
    {
        eventAudioSource.mute = val;
    }

    public void onBouncePadSound()
    {
        PlaySound(SoundName.BouncePad);
    }
    public void onDoorOpenSound()
    {
        PlaySound(SoundName.DoorOpen);
    }
    public void onChestOpenSound()
    {
        PlaySound(SoundName.ChestOpen);
    }
    public void onGameOverSound()
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