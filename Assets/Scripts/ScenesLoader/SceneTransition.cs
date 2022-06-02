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
        
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        loadSceneOperation.allowSceneActivation = false;
        
        animator.CrossFade("SceneClosing", 0f);
        float myTime = 0;
        
        while (myTime < 0.99f || !loadSceneOperation.isDone)
        {
            if (myTime < 0.99f)
            {
                myTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
            else if (!loadSceneOperation.allowSceneActivation)
            {
                loadSceneOperation.allowSceneActivation = true;
            }
            
            loadingPercent.text = Mathf.RoundToInt(loadSceneOperation.progress * 100) + "%";
            progressBar.fillAmount = loadSceneOperation.progress;

            yield return null;
        }
        
        animator.CrossFade("SceneOpening", 0f);
        myTime = 0;
        while (myTime < 0.99f)
        {
            myTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            yield return null;
        }
        
        isOnLoad = false;
    }
}
