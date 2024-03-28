using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image pauseBtnImage;
    [SerializeField] private Sprite pauseBtn;
    [SerializeField] private Sprite resumeBtn;

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
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
