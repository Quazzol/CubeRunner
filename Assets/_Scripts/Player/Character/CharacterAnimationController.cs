using UnityEngine;

public class CharacterAnimationController
{
    private Animator _animator;

    public CharacterAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void Idle()
    {
        _animator.SetBool("Run", false);
        _animator.SetBool("Dance", false);
    }

    public void Run()
    {
        _animator.SetBool("Dance", false);
        _animator.SetBool("Run", true);
    }

    public void Dance()
    {
        _animator.SetBool("Dance", true);
        _animator.SetBool("Run", false);
    }
}