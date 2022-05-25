using System.Collections.Generic;
using UnityEngine;

public static class AnimationState
{
    private static Dictionary<Animator, string> _currentAnimations;

    static AnimationState()
    {
        _currentAnimations = new Dictionary<Animator, string>();
    }

    public static void ChangeAnimation(Animator animator, string newAnimation)
    {
        if (_currentAnimations.ContainsKey(animator))
            _currentAnimations[animator] = newAnimation;
        else
            _currentAnimations.Add(animator, newAnimation);

        animator.Play(newAnimation);
    }
}
