using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TripledotToggle))]
public class ToggleAnimation : BaseControlAnimation
{
    private TripledotToggle toggle;

    protected override void Awake()
    {
        base.Awake();

        toggle = GetComponent<TripledotToggle>();

        toggle.onValueChanged.RemoveListener(OnValueChanged);
        toggle.onValueChanged.AddListener(OnValueChanged);

        toggle.onSetInteractable -= OnSetInteractable;
        toggle.onSetInteractable += OnSetInteractable;
    }

    protected override void SetupInitialState()
    {
        // Set the initial On or Off state of the object by sampling the last frame of the corresponding animation.
        var clip = toggle.isOn ? activeStateAnimation : normalStateAnimation;
        var interactClip = toggle.interactable ? enabledAnimation : disabledAnimation;

        SampleAnimation(clip.name, 1f);
        SampleAnimation(interactClip.name, 1f);
    }

    private void OnValueChanged(bool isOn)
    {
        var mainClip = isOn ? activeStateAnimation : normalStateAnimation;
        PlayClip(mainClip);
    }

    private void OnSetInteractable(bool interactable)
    {
        var mainClip = toggle.isOn ? activeStateAnimation : normalStateAnimation;
        StartCoroutine(WaitAndCheckInteractable(mainClip.length));
    }

    IEnumerator WaitAndCheckInteractable(float time)
    {
        yield return new WaitForSeconds(time + 0.1f);
        var interactClip = toggle.interactable ? enabledAnimation : disabledAnimation;
        PlayClip(interactClip);
    }
    }