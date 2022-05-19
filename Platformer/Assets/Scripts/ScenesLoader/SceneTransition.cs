using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text loadingPercent;
    [SerializeField] private Image progressBar;
    private static SceneTransition instance;
    private Animator animator;
    private AsyncOperation loadSceneOperation;
    private static bool shoudPlayOpeningAnimation = false;
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
        if (shoudPlayOpeningAnimation)
            animator.SetTrigger("sceneOpening");
    }

    private void Update()
    {
        if (loadSceneOperation != null)
        {
            loadingPercent.text = Mathf.RoundToInt(loadSceneOperation.progress * 100) + "%";
            progressBar.fillAmount = loadSceneOperation.progress;
        }
    }

    public static void SwitchToScene(string sceneName)
    {
        instance.animator.SetTrigger("sceneEnding");
        instance.loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadSceneOperation.allowSceneActivation = false;
    }

    public void OnAnimationOver()
    {
        shoudPlayOpeningAnimation = true;
        loadSceneOperation.allowSceneActivation = true;
    }

}
