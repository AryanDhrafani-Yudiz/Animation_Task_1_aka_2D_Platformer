using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image pauseBtnImage;
    [SerializeField] private Sprite pauseBtn;
    [SerializeField] private Sprite resumeBtn;

    [SerializeField] private Canvas StartingScreenCanvas;
    [SerializeField] private Canvas GamePlayCanvas;
    [SerializeField] private Canvas SettingsCanvas;
    [SerializeField] private Canvas GameOverCanvas;
    public bool gamePlayScreen = false;

    void Awake()
    {
        StartingScreenCanvas.enabled = true;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        GameOverCanvas.enabled = false;
    }
    public void OnPlayBtnClick()
    {
        gamePlayScreen = true;
        GamePlayCanvas.enabled = true;
        StartingScreenCanvas.enabled = false;
    }
    public void OnSettingsBtnClick() // Pause The Game On Settings Btn Click
    {
        gamePlayScreen = false;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = true;
        Time.timeScale = 0;
    }
    public void OnResume() // Unpause The Game On Resume Btn Click
    {
        gamePlayScreen = true;
        GamePlayCanvas.enabled = true;
        SettingsCanvas.enabled = false;
        Time.timeScale = 1;
    }
    public void OnGameOverScreen()
    {
        gamePlayScreen = false;
        StartingScreenCanvas.enabled = false;
        GamePlayCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        GameOverCanvas.enabled = true;
    }
    public void OnExitBtnClick()
    {
        Application.Quit();
    }
    public void onPauseBtn()
    {
        if (pauseBtnImage.sprite == pauseBtn)
        {
            pauseBtnImage.sprite = resumeBtn;
            Time.timeScale = 0;
        }
        else if (pauseBtnImage.sprite == resumeBtn)
        {
            pauseBtnImage.sprite = pauseBtn;
            Time.timeScale = 1;
        }
    }
    public void onRestartBtn()
    {
        StartCoroutine(LoadYourAsyncScene());
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
