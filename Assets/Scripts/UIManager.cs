using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Image pauseBtnImage;
    [SerializeField] private Sprite pauseBtn;
    [SerializeField] private Sprite resumeBtn;

    [SerializeField] private Image volumeBtnImage;
    [SerializeField] private Sprite volumeOnBtn;
    [SerializeField] private Sprite volumeOffBtn;

    [SerializeField] private Image musicBtnImage;
    [SerializeField] private Sprite musicOnBtn;
    [SerializeField] private Sprite musicOffBtn;

    [SerializeField] private Canvas StartingScreenCanvas;
    [SerializeField] private Canvas GamePlayCanvas;
    [SerializeField] private Canvas SettingsCanvas;
    [SerializeField] private Canvas GameOverCanvas;
    public bool gamePlayScreen = false;
    public bool isPaused = false;

    [SerializeField] private Slider gameplaySoundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource eventAudioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        Time.timeScale = 1;
    }
    private void Start()
    {
        StartingScreenCanvas.enabled = true;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        GameOverCanvas.enabled = false;

        gameplaySoundSlider.value = eventAudioSource.volume;
        musicSlider.value = bgAudioSource.volume;
    }
    public void OnPlayBtnClick()
    {
        gamePlayScreen = true;
        GamePlayCanvas.enabled = true;
        StartingScreenCanvas.enabled = false;
        SoundManager.Instance.OnGamePlayScreen();
        SoundManager.Instance.OnButtonClick();
    }
    public void OnSettingsBtnClick() // Pause The Game On Settings Btn Click
    {
        gamePlayScreen = false;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = true;
        Time.timeScale = 0;
        SoundManager.Instance.OnUIScreenOpened();
        SoundManager.Instance.OnButtonClick();
    }
    public void OnResume() // Unpause The Game On Resume Btn Click
    {
        gamePlayScreen = true;
        GamePlayCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        Time.timeScale = 1;
        SoundManager.Instance.OnGamePlayScreen();
        SoundManager.Instance.OnButtonClick();
    }
    public void OnGameOverScreen()
    {
        gamePlayScreen = false;
        StartingScreenCanvas.enabled = false;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        GameOverCanvas.enabled = true;
        SoundManager.Instance.OnGameOverSound();
        SoundManager.Instance.OnUIScreenOpened();
        SoundManager.Instance.OnButtonClick();
    }
    public void OnExitBtnClick()
    {
        Application.Quit();
    }
    public void OnPauseBtn()
    {
        SoundManager.Instance.OnButtonClick();
        if (pauseBtnImage.sprite == pauseBtn)
        {
            SoundManager.Instance.OnUIScreenOpened();
            pauseBtnImage.sprite = resumeBtn;
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (pauseBtnImage.sprite == resumeBtn)
        {
            SoundManager.Instance.OnGamePlayScreen();
            pauseBtnImage.sprite = pauseBtn;
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    public void OnVolumeOnOffBtn()
    {
        if (volumeBtnImage.sprite == volumeOnBtn)
        {
            SoundManager.Instance.OnVolumeOn(false, gameplaySoundSlider.value);
            volumeBtnImage.sprite = volumeOffBtn;
        }
        else if (volumeBtnImage.sprite == volumeOffBtn)
        {
            SoundManager.Instance.OnVolumeOn(true, gameplaySoundSlider.value);
            volumeBtnImage.sprite = volumeOnBtn;
        }
    }
    public void OnMusicOnOffBtn()
    {
        if (musicBtnImage.sprite == musicOnBtn)
        {
            SoundManager.Instance.OnMusicOn(false, musicSlider.value);
            musicBtnImage.sprite = musicOffBtn;
        }
        else if (musicBtnImage.sprite == musicOffBtn)
        {
            SoundManager.Instance.OnMusicOn(true, musicSlider.value);
            musicBtnImage.sprite = musicOnBtn;
        }
    }
    public void ChangeGameplaySoundVolume()
    {
        SoundManager.Instance.ChangeGameplaySoundVolume(gameplaySoundSlider.value);
    }
    public void ChangeMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(musicSlider.value);
    }
    public void OnRestartBtn()
    {
        StartCoroutine(LoadYourAsyncScene());
        SoundManager.Instance.OnButtonClick();
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        Time.timeScale = 1;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
