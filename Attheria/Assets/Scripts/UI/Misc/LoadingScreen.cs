using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private CanvasGroup group;
    [SerializeField] private Image fillImage;
    private float progress = 0;
    
    void Start()
    {
        group = GetComponent<CanvasGroup>();

        DontDestroyOnLoad(gameObject);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += fadeOutScreen;
        StartCoroutine(animateLoadBar());
    }

    /// <summary>
    /// Wait for all data to load then fade out the loading screen
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="loadSceneMode"></param>
    private void fadeOutScreen(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= fadeOutScreen;
        if(gameObject != null)
        StartCoroutine(WaitForInstance());

    }
    /// <summary>
    /// Wait for SaveLoadManager instance to be initiated and for Loaded boolean because it threw null exception error in build
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForInstance()
    {
        while (SaveLoadManager.Instance == null)
        {
            yield return null;
        }
        while (!SaveLoadManager.Instance.Loaded)
        {
            yield return null;
        }
        StartCoroutine(fadeOutScreenCoroutine());
    }

    /// <summary>
    /// Animates the remaining .1 of load time since LoadSceneAsync loads to .9
    /// Disables load screen
    /// </summary>
    IEnumerator fadeOutScreenCoroutine()
    {
        while (progress < 1)
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime / 2);
            fillImage.fillAmount = progress;
            yield return null;
        }

        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        //progress = 0;
        //gameObject.SetActive(false);
        //group.alpha = 0;
    }

    /// <summary>
    /// Animates load bar based on async load progress
    /// </summary>
    /// <returns></returns>
    IEnumerator animateLoadBar()
    {
        while (NetworkManager.loadingSceneAsync == null) yield return null;
        while (/*SceneManager.GetActiveScene().buildIndex == 0 &&*/ !NetworkManager.loadingSceneAsync.isDone && progress < .9f)
        {
            progress = Mathf.MoveTowards(progress, NetworkManager.loadingSceneAsync.progress, Time.deltaTime);
            fillImage.fillAmount = progress;
            if (progress >= .9f) NetworkManager.loadingSceneAsync.allowSceneActivation = true;
            yield return null;
        }
    }
    
    
}