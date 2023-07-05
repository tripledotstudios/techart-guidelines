using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatorStateNotifier))]
public class UIScreen : MonoBehaviour
{
    protected static readonly int AnimatorParameterIsVisible = Animator.StringToHash("IsVisible");
    protected static readonly int AnimatorStateOpen = Animator.StringToHash("Open");
    protected static readonly int AnimatorStateClose = Animator.StringToHash("Close");

    public event Action OnOpened;
    public event Action OnClosed;
    
    public bool IsOpen { get; private set; }
    
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
        if (IsOpen) {
            return;
        }

        IsOpen = true;
        animator.SetBool(AnimatorParameterIsVisible, IsOpen);
    }

    public void Close()
    {
        if (!IsOpen) {
            return;
        }

        IsOpen = false;
        animator.SetBool(AnimatorParameterIsVisible, IsOpen);
    }

    private void OnStateEntered(int stateHash)
    {
        
    }
    
    private void OnStateExited(int stateHash)
    {
        if (stateHash == AnimatorStateOpen) {
            Debug.Log("Opened");
            OnOpened?.Invoke();
        }

        if (stateHash == AnimatorStateClose) {
            Debug.Log("Closed");
            OnClosed?.Invoke();
        }
    }
}
