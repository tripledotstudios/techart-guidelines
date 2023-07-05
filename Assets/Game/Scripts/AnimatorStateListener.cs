using System;
using UnityEngine;

public class AnimatorStateListener : StateMachineBehaviour
{
    public event Action<int> OnStateEntered;
    public event Action<int> OnStateExited;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        OnStateEntered?.Invoke(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnStateExited?.Invoke(stateInfo.shortNameHash);
    }
}

