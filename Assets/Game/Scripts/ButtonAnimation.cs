using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(TripledotButton))]
public class ButtonAnimation : BaseControlAnimation
{
    private TripledotButton button;

    protected override void Awake()
    {
        base.Awake();
        button = GetComponent<TripledotButton>();

        button.onSetInteractable += OnSetInteractable;
        button.onFingerMoveOutWhilePressed += OnFingerMoveOutWhilePressed;
        button.onPointerDown += OnButtonDown;
        button.onPointerUp += OnButtonUp;
        button.onClearState += SetupInitialState;
    }

    private void OnButtonDown()
    {
        if (!button.IsInteractable())
            return;

        PlayClip(activeStateAnimation);
    }

    private void OnButtonUp()
    {
        if (!button.IsInteractable())
            return;

        if (!button.FingerMovedOutWhilePressed) {
            PlayClip(normalStateAnimation);
        }
    }

    protected override void SetupInitialState()
    {
        // Set the initial On or Off state of the object by sampling the last frame of the corresponding animation.
        var interactClip = button.interactable ? enabledAnimation : disabledAnimation;

        SampleAnimation(normalStateAnimation.name, 1f);
        SampleAnimation(interactClip.name, 1f);
    }

    private void OnSetInteractable(bool interactable)
    {
        // Coroutine will not start if game object is not active and only error will be logged.
        if (gameObject.activeInHierarchy) {
            StartCoroutine(WaitAndCheckInteractable(activeStateAnimation.length));
        }
        }

        private void OnFingerMoveOutWhilePressed(bool fingerMoveOut)
        {
            if (!button.interactable)
                return;

            var clip = fingerMoveOut ? normalStateAnimation : activeStateAnimation;
            PlayClip(clip);
        }

        IEnumerator WaitAndCheckInteractable(float time)
        {
            yield return new WaitForSeconds(time);
            var interactClip = button.interactable ? enabledAnimation : disabledAnimation;
            PlayClip(interactClip);
        }

        #if UNITY_EDITOR
        [CustomEditor(typeof(ButtonAnimation))]
        public class ButtonAnimationInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                
                var buttonAnimation = (ButtonAnimation) target;

                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("▲ Send to Animation"))
                {
                    var animation = buttonAnimation.GetComponent<Animation>();
                    if (animation != null)
                    {
                        var animationClips = GetAnimationClips();
                        for (int i = 0; i < animationClips.Count; i++)
                        {
                            if (animationClips[i] != null)
                            {
                                animation.AddClip(animationClips[i], animationClips[i].name);
                            }
                        }
                        
                        EditorUtility.SetDirty(animation);
                    }
                }
                if (GUILayout.Button("✖ Clear from Animation"))
                {
                    var animation = buttonAnimation.GetComponent<Animation>();
                    if (animation != null)
                    {
                        var animationClips = GetAnimationClips();
                        for (int i = 0; i < animationClips.Count; i++)
                        {
                            if (animationClips[i] != null)
                            {
                                animation.RemoveClip(animationClips[i].name);
                            }
                        }
                        
                        EditorUtility.SetDirty(animation);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            private List<AnimationClip> GetAnimationClips()
            {
                var activeProperty = serializedObject.FindProperty("activeStateAnimation");
                var normalProperty = serializedObject.FindProperty("normalStateAnimation");
                var disabledProperty = serializedObject.FindProperty("disabledAnimation");
                var enabledProperty = serializedObject.FindProperty("enabledAnimation");

                var activeClip = (AnimationClip) activeProperty.objectReferenceValue;
                var normalClip = (AnimationClip) normalProperty.objectReferenceValue;
                var disabledClip = (AnimationClip) disabledProperty.objectReferenceValue;
                var enabledClip = (AnimationClip) enabledProperty.objectReferenceValue;

                return new List<AnimationClip>
                {
                    activeClip,
                    normalClip,
                    disabledClip,
                    enabledClip
                };
            }
        }
        #endif
    }
