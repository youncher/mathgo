﻿using UnityEngine;

public class OverlayCloseButton : MonoBehaviour
{
    private GameObject overObject;
    
    void Start()
    {
        overObject = GameObject.Find("TutorialOverlay");
    }

    public void HandleClickDeactivateOverlay()
    {
        overObject.SetActive(false);
    }
}
