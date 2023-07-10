using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    private List<UIScreen> screenStack = new List<UIScreen>();
    private UIScreen activeScreen;

    public void Awake()
    {
        if (ReferenceEquals(Instance, null)) {
            Instance = this;
        }
    }

    public void Push(UIScreen prefab)
    {
        activeScreen = Instantiate(prefab, transform);
        
        screenStack.Add(activeScreen);

        activeScreen.OnVisibilityChanged += OnScreenVisibilityChanged;

        activeScreen.Open();
    }

    private void OnScreenVisibilityChanged(bool isVisible)
    {
        if (!isVisible) {
            activeScreen.OnVisibilityChanged -= OnScreenVisibilityChanged;

            screenStack.Remove(activeScreen);
            Destroy(activeScreen.gameObject);
            
            if (screenStack.Count == 0) {
                activeScreen = null;
                return;
            }
        
            activeScreen = screenStack.Last();
        }
    }
}
