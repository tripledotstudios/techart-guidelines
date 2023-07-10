using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TripledotButton : Button
{
    public UnityAction<bool> onSetInteractable;
    public UnityAction<bool> onFingerMoveOutWhilePressed;
    public UnityAction onPointerUp;
    public UnityAction onPointerDown;
    public UnityAction onClearState;

    private bool previouslyEnabled;
    private bool pressed;

    public bool FingerMovedOutWhilePressed { get; private set; }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        // Handling enabled/disabled state
        if (state == SelectionState.Disabled) {
            previouslyEnabled = false;
            onSetInteractable?.Invoke(false);
        } else if (!previouslyEnabled) {
            previouslyEnabled = true;
            onSetInteractable?.Invoke(true);
        }

        // Handling move finger out
        if (pressed && state == SelectionState.Highlighted && !FingerMovedOutWhilePressed) {
            onFingerMoveOutWhilePressed?.Invoke(true);
            FingerMovedOutWhilePressed = true;
        }

        if (pressed && state == SelectionState.Pressed) {
            onFingerMoveOutWhilePressed?.Invoke(false);
            FingerMovedOutWhilePressed = false;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onPointerDown?.Invoke();

        pressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onPointerUp?.Invoke();

        pressed = false;
        FingerMovedOutWhilePressed = false;
    }

    protected override void InstantClearState()
    {
        base.InstantClearState();

        onClearState?.Invoke();
    }
}
