using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class IStarterGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("lvl1");
    }
}
