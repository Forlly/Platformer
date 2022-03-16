using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    
    private readonly List<AnimationData> _animationDatas = new List<AnimationData>
    {
        new AnimationData
        {
            id = "Idle",
            layer = 0,
            state = true
        },
        new AnimationData
        {
            id = "Run",
            layer = 1,
            state = false
        },
        new AnimationData
        {
            id = "Jump",
            layer = 2,
            state = false
        }, 
        new AnimationData
        {
            id = "Ladder",
            layer = -1,
            state = false
        }
    };

    public void ForceActiveAnimationState(string id)
    {
        foreach (AnimationData animData in _animationDatas)
        {
            animData.state = animData.id == id;
        }
        
        characterAnimator.CrossFade(id, 0.0f);
    }
    public void SetAnimationState(string id, bool state)
    {
        _animationDatas.FirstOrDefault(data => data.id == id)!.state = state;
        
        string highestId = "Idle";
        int layer = 0;
        foreach (var animData in _animationDatas.Where(animData => animData.layer > layer && animData.state))
        {
            layer = animData.layer;
            highestId = animData.id;    
        }
        
        characterAnimator.CrossFade(highestId, 0.0f);
    }
}

public class AnimationData
{
    public string id;
    public int layer;
    public bool state;
}