using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectScreen : MonoBehaviour
{
    [SerializeField] private List<UIPanel> lstUIPanel;
    private bool isPortrait;

    private void Start()
    {
        if (Screen.width < Screen.height)
        {
            isPortrait = true;
            SetAllPortrait();
        }
        else
        {
            isPortrait = false;
            SetAllLandscape();
        }
    }
    private void FixedUpdate()
    {
        if (Screen.width < Screen.height)
        {
            if (isPortrait == false)
            {
                isPortrait = true;
                SetAllPortrait();
            }
        }
        else
        {
            if (isPortrait)
            {
                isPortrait = false;
                SetAllLandscape();
            }
        }
    }
    void SetAllPortrait()
    {
        for (int i = 0; i < lstUIPanel.Count; i++)
        {
            lstUIPanel[i].PortraitUI();
        }
    }
    void SetAllLandscape()
    {
        for (int i = 0; i < lstUIPanel.Count; i++)
        {
            lstUIPanel[i].LandscapeUI();
        }
    }

}
