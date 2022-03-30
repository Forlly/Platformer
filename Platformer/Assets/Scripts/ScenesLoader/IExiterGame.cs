using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class IExiterGame : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
