using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal portalEnd;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [HideInInspector] public bool Active = true;

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
