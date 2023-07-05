using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorStateNotifier : MonoBehaviour
{
    public event Action<int> OnStateEntered;
    public event Action<int> OnStateExited;
    
    public Animator Animator { get; private set; }

    private AnimatorStateListener[] behaviours;

    private void OnEnable()
    {
        if (ReferenceEquals(Animator, null)) {
            Animator = GetComponent<Animator>();
        }

        behaviours = Animator.GetBehaviours<AnimatorStateListener>();

        foreach (var behaviour in behaviours) {
            behaviour.OnStateEntered += NotifyStateEntered;
            behaviour.OnStateExited += NotifyStateExited;
        }
    }

    private void OnDisable()
    {
        foreach (var behaviour in behaviours) {
            behaviour.OnStateEntered -= NotifyStateEntered;
            behaviour.OnStateExited -= NotifyStateExited;
        }
    }

    private void NotifyStateEntered(int stateNameHash)
    {
        OnStateEntered?.Invoke(stateNameHash);
    }
    
    private void NotifyStateExited(int stateNameHash)
    {
        OnStateExited?.Invoke(stateNameHash);
    }
}
