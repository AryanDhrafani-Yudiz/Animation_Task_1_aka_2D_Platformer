using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

public class SceneLoader : MonoBehaviour
{
    public Canvas loadingScreenCanvas;
    private AsyncOperation asyncLoad;
    public Slider progressBar;

    public void Start()
    {
        loadingScreenCanvas.enabled = false;
        //DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        //progressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
        //Debug.Log(progressBar.value);
    }
    public void LoadNextLevel()
    {
        loadingScreenCanvas.enabled = true;
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        Time.timeScale = 1;
        asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        progressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
        //Task.Delay(5000);
        Debug.Log(progressBar.value);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}