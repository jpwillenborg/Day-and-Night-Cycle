using UnityEngine;
using DistantLands.Cozy;
using DistantLands.Cozy.Data;
using UnityEditor;
using UnityEngine.UI;
using TMPro;


public class TimePause : MonoBehaviour
{
    [SerializeField]
    PerennialProfile perennialProfile;
    [SerializeField]
    private Slider slider;
    [HideInInspector]
    public static bool isPaused = true;
    private CozyTimeModule timeModule;


    void Start()
    {
        timeModule = CozyWeather.instance.timeModule;
        perennialProfile.pauseTime = isPaused;
    }


    void Update()
    {
        slider.SetValueWithoutNotify((float)timeModule.currentTime);
    }


    public void AdjustTime(float value)
    {
        timeModule.currentTime = (MeridiemTime)value;
    }


    public void PauseTime(bool value)
    {
        perennialProfile.pauseTime = !value;
        isPaused = !value;
    }
}