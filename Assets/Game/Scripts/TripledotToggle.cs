using UnityEngine.Events;
using UnityEngine.UI;

/// <inheritdoc />
/// <summary>
/// Identical to Unity's default Toggle component, except that it fires UnityAction events when 'Interactable' is toggled.
/// </summary>
public class TripledotToggle : Toggle
{
    public UnityAction<bool> onSetInteractable;

    private bool previouslyEnabled;
    
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (state == SelectionState.Disabled) {
            previouslyEnabled = false;
            onSetInteractable?.Invoke(false);
        } else if (!previouslyEnabled) {
            previouslyEnabled = true;
            onSetInteractable?.Invoke(true);
        }
    }
}