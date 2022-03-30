using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ISettingerGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Settings");
    }
}
