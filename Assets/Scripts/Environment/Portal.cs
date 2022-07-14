using UnityEngine;
/// <summary>
/// \brief Класс отвечающий за реализацию портала
/// </summary>
public class Portal : MonoBehaviour
{
    /// <summary>
    /// \param portalEnd Конечный портал
    /// \param Active Активирован ли портал
    /// </summary>
    [SerializeField] private Portal portalEnd;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [HideInInspector] public bool Active = true;
    /// <summary>
    /// \brief Метод получения позиции портала
    /// </summary>
    /// <returns>Позиция портала</returns>
    public Vector3 GetPortalPosition()
    {
        return portalEnd.transform.position;
    }

    public void CharacterTeleported()
    {
        portalEnd._spriteRenderer.sprite = ImageDBHelper.GetSprite("PortalOff");
        portalEnd.Active = false;
    }
    
    public void CharacterExit()
    {
        _spriteRenderer.sprite = ImageDBHelper.GetSprite("PortalOn");
        Active = true;
    }
}
