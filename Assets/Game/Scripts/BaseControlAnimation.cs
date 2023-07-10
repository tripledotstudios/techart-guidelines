using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animation))]
public abstract class BaseControlAnimation : UIBehaviour
{
    [SerializeField, Header("Animation Clips"), Tooltip("Toggle: On, Button: Pressed")]
    protected AnimationClip activeStateAnimation;
    [SerializeField, Tooltip("Toggle: Off, Button: Idle")]
    protected AnimationClip normalStateAnimation;
    [SerializeField, Tooltip("Toggle & Button: Transition from either Active or Normal, to Disabled.")]
    protected AnimationClip disabledAnimation;
    [SerializeField, Tooltip("Toggle & Button: Transition from disabled, to either Active or Normal.")]
    protected AnimationClip enabledAnimation;
    
    private Animation animationComponent;
    private bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
        animationComponent = GetComponent<Animation>();

        // Add animations to Animation component's list.
        if (animationComponent.GetClip(activeStateAnimation.name) == null)
            animationComponent.AddClip(activeStateAnimation, activeStateAnimation.name);

        if (animationComponent.GetClip(normalStateAnimation.name) == null)
            animationComponent.AddClip(normalStateAnimation, normalStateAnimation.name);

        if (animationComponent.GetClip(disabledAnimation.name) == null)
            animationComponent.AddClip(disabledAnimation, disabledAnimation.name);

        if (animationComponent.GetClip(enabledAnimation.name) == null)
            animationComponent.AddClip(enabledAnimation, enabledAnimation.name);
    }

    protected override void Start()
    {
        base.Start();

        SetupInitialState();
        initialized = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (initialized) {
            SetupInitialState();
        }
    }

    protected abstract void SetupInitialState();

    protected void PlayClip(AnimationClip clip)
    {
            animationComponent.Play(clip.name);
        }

        protected void SampleAnimation(string clipName, float normalizedTime)
        {
            var state = animationComponent[clipName];
            state.weight = 1;
            state.normalizedTime = normalizedTime;
            state.enabled = true;
            animationComponent.Sample();
            state.enabled = false;
        }
    }
