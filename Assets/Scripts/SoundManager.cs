using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip bounceClip;
    [SerializeField] private AudioClip chestOpenClip;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip bgMusic;

    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource eventAudioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        bgAudioSource.enabled = true;
        eventAudioSource.enabled = true;
    }
    public void onBouncePadSound()
    {
        eventAudioSource.PlayOneShot(bounceClip);
    }
    public void onDoorOpenSound()
    {
        eventAudioSource.PlayOneShot(doorOpen);
    }
    public void onChestOpenSound()
    {
        eventAudioSource.PlayOneShot(chestOpenClip);
    }
    public void onGameOverSound()
    {
        bgAudioSource.enabled = false;
        StartCoroutine(Timer(2));
    }
    private IEnumerator Timer(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        eventAudioSource.enabled = false;
    }
}