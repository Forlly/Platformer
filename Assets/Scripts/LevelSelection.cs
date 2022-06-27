using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform test;
    [SerializeField] private Button lvlButton;
    [SerializeField] private GameObject scrollView;
    
    [SerializeField] private Button closeView;
    [SerializeField] private SettingsSystem settingsSystem;

    private List<Button> lvlsButtons;
    private LvlSettings lvlSettings;
    private float sizeOfLvlsImg = 0f;
    private float sizeOfLvlsImgY = 0f;
    private bool AddLine = false;
    void Start()
    {
        closeView.onClick.AddListener(CloseScrollView);
        
        lvlSettings = new LvlSettings();
        lvlSettings =  
            SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"), "LvlSettings.json");
        
        lvlsButtons = new List<Button>();
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        
        
        for (int i = 1; i < lvlSettings.lvl; i++)
        {
            var i1 = i;
            Button tmp = Instantiate(lvlButton, content);
            tmp.GetComponentInChildren<Text>().text = i1.ToString();
            tmp.onClick.AddListener(() => { LoadLevel(i1); });
            
            
            if (sizeOfLvlsImg >= (test.rect.width - (3 * sizeOfLvlsImg /gridLayout.cellSize.x) - 70))
            {
                
                sizeOfLvlsImg = 0f;
                sizeOfLvlsImgY += gridLayout.cellSize.x + 10f;
                
                if (AddLine)
                {
                    content.sizeDelta = new Vector2(content.sizeDelta.x,
                        content.sizeDelta.y + gridLayout.cellSize.y);
                }
            }

            if (sizeOfLvlsImgY >= test.rect.height - 75 && !AddLine)
            {
                content.sizeDelta = new Vector2(content.sizeDelta.x,
                    content.sizeDelta.y + gridLayout.cellSize.y );
                sizeOfLvlsImgY = 0f;
                AddLine = true;
            }
            
            lvlsButtons.Add(tmp);
            
            sizeOfLvlsImg += gridLayout.cellSize.x;
        }


    }

    private void CloseScrollView()
    {
        scrollView.SetActive(false);
    }

    private void LoadLevel(int numLvl)
    {

        lvlSettings.currentLvl = numLvl;
        SaveSystem.SaveFile<LvlSettings>(lvlSettings, Path.Combine(Application.dataPath, "Json"), "LvlSettings.json");
        SceneTransition.instance.SwitchToScene("lvl1");
    }

    
}
