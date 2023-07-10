using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatorStateNotifier))]
public class UIScreen : MonoBehaviour
{
    protected static readonly int AnimatorParameterIsVisible = Animator.StringToHash("IsVisible");
    protected static readonly int AnimatorStateInvisible = Animator.StringToHash("Invisible");
    protected static readonly int AnimatorStateShow = Animator.StringToHash("Show");
    protected static readonly int AnimatorStateVisible = Animator.StringToHash("Visible");
    protected static readonly int AnimatorStateHide = Animator.StringToHash("Hide");

    public event Action<bool> OnVisibilityChanged;
    
    public bool IsVisible { get; private set; }
    
    private Animator animator;
    private AnimatorStateNotifier animatorStateNotifier;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        
        animatorStateNotifier = GetComponent<AnimatorStateNotifier>();
        animatorStateNotifier.OnStateEntered += OnStateEntered;
        animatorStateNotifier.OnStateExited += OnStateExited;
    }

    private void OnDisable()
    {
        animatorStateNotifier.OnStateEntered -= OnStateEntered;
        animatorStateNotifier.OnStateExited -= OnStateExited;
    }

    public void Open()
    {
        if (IsVisible) {
            return;
        }

        IsVisible = true;
        animator.SetBool(AnimatorParameterIsVisible, IsVisible);
    }

    public void Close()
    {
        if (!IsVisible) {
            return;
        }

        IsVisible = false;
        animator.SetBool(AnimatorParameterIsVisible, IsVisible);
    }

    private void OnStateEntered(int stateHash)
    {
        
    }
    
    private void OnStateExited(int stateHash)
    {
        if (stateHash == AnimatorStateShow) {
            Debug.Log("Visible");
            OnVisibilityChanged?.Invoke(true);
        }

        if (stateHash == AnimatorStateHide) {
            Debug.Log("Hidden");
            OnVisibilityChanged?.Invoke(false);
        }
    }
}
