using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text expirience;

    public void UpdateScore(int score)
    {
        expirience.text = score.ToString();
        StartCoroutine(AfterUpdateScore());
    }
    
    private IEnumerator AfterUpdateScore()
    {
        float step_sec = 0.1f;
        expirience.fontSize += 50;
        for (float i = 0; i < 1; i += step_sec)
        {
            expirience.color = new Color(0.2f,0.9f,0.3f,0.6f);
            yield return new WaitForSeconds(step_sec/2);
            expirience.color = new Color(0.2f,0.9f,0.3f,0.9f);
            yield return new WaitForSeconds(step_sec/2);
        }
        expirience.color = new Color(0.98f, 0.98f, 0.26f, 1);
        expirience.fontSize -= 50;
    }
}
