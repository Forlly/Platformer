using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text loadingPercent;
    [SerializeField] private Image progressBar;
    [SerializeField] private Animator animator;

    public static SceneTransition instance;
    
    private bool isOnLoad;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(transform.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    public void SwitchToScene(string sceneName)
    {
        if (isOnLoad)
            return;
        
        StartCoroutine(SceneLoading(sceneName));
    }

    private IEnumerator SceneLoading(string sceneName)
    {
        isOnLoad = true;

        Coroutine animCorotine = StartCoroutine(AnimationPlay("SceneClosing", 0f));
        yield return animCorotine;
        
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadSceneOperation.isDone)
        {
            Debug.Log("HI");
            loadingPercent.text = Mathf.RoundToInt(loadSceneOperation.progress * 100) + "%";
            progressBar.fillAmount = loadSceneOperation.progress;

            yield return null;
        }

        animCorotine = StartCoroutine(AnimationPlay("SceneOpening", 0f));
        yield return animCorotine;
        
        isOnLoad = false;
    }

    private IEnumerator AnimationPlay(string animName, float normalizedTransitionDuration)
    {
        animator.CrossFade(animName, normalizedTransitionDuration);
        float currentAnimTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        while (currentAnimTime < 0.99999f)
        {
            Debug.Log("hi");
            Debug.Log(currentAnimTime);
            yield return null;
            currentAnimTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
}
