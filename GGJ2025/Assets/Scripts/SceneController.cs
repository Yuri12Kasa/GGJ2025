using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    
    [SerializeField] private float _fadeDuration = 0.2f;
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    
    public void NextScene()
    {
        if (SceneManager.sceneCountInBuildSettings-1 != SceneManager.GetActiveScene().buildIndex)
        {
            StartCoroutine(ControlledFade(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void PreviousScene()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 >= 0)
        {
            StartCoroutine(ControlledFade(SceneManager.GetActiveScene().buildIndex - 1));
        } 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #region Fade

        private IEnumerator ControlledFade(int buildIndex)
        {
            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(_fadeDuration);
            SceneManager.LoadScene(buildIndex);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeIn()
        {
            float time = 0;
            while (time <= _fadeDuration)
            {
                _canvasGroup.alpha = Mathf.Lerp(0, 1, time / _fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator FadeOut()
        {
            float time = 0;
            while (time <= _fadeDuration)
            {
                _canvasGroup.alpha = Mathf.Lerp(1, 0, time / _fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }
        }

    #endregion
}
