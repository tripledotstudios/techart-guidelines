using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBar : MonoBehaviour
{
    [SerializeField]
    private TripledotToggle homeToggle;
    [SerializeField]
    private TripledotToggle settingsToggle;
    [SerializeField]
    private TripledotToggle hamburgerToggle;

    [SerializeField]
    private UIScreen homeScreenPrefab;
    [SerializeField]
    private UIScreen settingsScreenPrefab;
    [SerializeField]
    private UIScreen hamburgerScreenPrefab;
    
    private UIScreen activeScreen;

    public void OnHomeToggleChanged(bool value)
    {
        if (!value && !ReferenceEquals(activeScreen, null)) {
            CloseScreen();
            return;
        }
        
        ShowScreen(homeScreenPrefab);
    }
    
    public void OnSettingsToggleChanged(bool value)
    {
        if (!value) {
            CloseScreen();
            return;
        }
        
        ShowScreen(settingsScreenPrefab);
    }
    
    public void OnHamburgerToggleChanged(bool value)
    {
        if (!value) {
            CloseScreen();
            return;
        }
        
        ShowScreen(hamburgerScreenPrefab);
    }

    private void ShowScreen(UIScreen prefab)
    {
        CloseScreen();
        activeScreen = ScreenManager.Instance.Push(prefab);
    }

    private void CloseScreen()
    {
        if (ReferenceEquals(activeScreen, null)) {
            return;
        }
        
        activeScreen.Close();
        activeScreen = null;
    }
}
