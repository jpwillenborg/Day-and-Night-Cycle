using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
	private CanvasScaler screenInfoCanvas;
    [SerializeField]
	private TextMeshProUGUI display;
    private bool isFullScreen = false;


    void Awake ()
    {
        Screen.SetResolution(1280, 720, false);
        screenInfoCanvas.scaleFactor = 720f / 1080f;
        StartCoroutine(UpdateDisplay());
    }


    public void FullscreenSwitch(bool value)
    {
        isFullScreen = !isFullScreen;
        
        if (value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            screenInfoCanvas.scaleFactor = (float)Screen.currentResolution.height / 1080f;
        } else
        {
            Screen.SetResolution(1280, 720, false);
            screenInfoCanvas.scaleFactor = 720f / 1080f;
        }

        StartCoroutine(UpdateDisplay());
    }


    private IEnumerator UpdateDisplay()
    {
        yield return new WaitForSeconds(0.05f);
        display.SetText("Resolution" + "\n" + Screen.width + " x " + Screen.height);
    }
}