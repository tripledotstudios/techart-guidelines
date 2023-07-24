using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    
    private UIScreen activeScreen;

    public void Awake()
    {
        if (ReferenceEquals(Instance, null)) {
            Instance = this;
        }
    }

    public UIScreen Push(UIScreen prefab)
    {
        if (!ReferenceEquals(activeScreen, null)) {
            activeScreen.Close();
            activeScreen = null;
        }
        
        activeScreen = Instantiate(prefab, transform);

        activeScreen.OnOpened += OnScreenOpened;
        activeScreen.OnClosed += OnScreenClosed;

        activeScreen.Open();

        return activeScreen;
    }

    private void OnScreenOpened(UIScreen screen)
    {
        
    }
    
    private void OnScreenClosed(UIScreen screen)
    {
        if (screen == activeScreen) {
            activeScreen = null;
        }
        
        Destroy(screen.gameObject);
    }
}
