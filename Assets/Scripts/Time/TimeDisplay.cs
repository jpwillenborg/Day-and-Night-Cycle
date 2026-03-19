using UnityEngine;
using DistantLands.Cozy;
using UnityEngine.UI;
using TMPro;


public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    private CozyWeather sphere;
    [SerializeField]
    private CozyTimeModule timeModule;
    [SerializeField]
    TextMeshProUGUI display01;
    [SerializeField]
    TextMeshProUGUI display02;
    [SerializeField]
    private Image fuzzyTimeIcon, fuzzyTimeIconDrop;
    [SerializeField]
    private Sprite sunriseIcon, sunsetIcon, dayIcon, nightIcon;


    public void Update()
    {
        display01.text = $"{(timeModule.currentTime.hours % 12 == 0 ? 12 : timeModule.currentTime.hours % 12):D2}:{timeModule.currentTime.minutes:D2}";
        display02.text = $"{(timeModule.currentTime.hours >= 12 ? "PM" : "AM")}";

        if (sphere.dayPercentage < new MeridiemTime(6, 00))
        {
            fuzzyTimeIcon.sprite = nightIcon;
            fuzzyTimeIconDrop.sprite = nightIcon;
        }
        else if (sphere.dayPercentage < new MeridiemTime(7, 30))
        {
            fuzzyTimeIcon.sprite = sunriseIcon;
            fuzzyTimeIconDrop.sprite = sunriseIcon;
        }
        else if (sphere.dayPercentage < new MeridiemTime(17, 00))
        {
            fuzzyTimeIcon.sprite = dayIcon;
            fuzzyTimeIconDrop.sprite = dayIcon;
        }
        else if (sphere.dayPercentage < new MeridiemTime(19, 00))
        {
            fuzzyTimeIcon.sprite = sunsetIcon;
            fuzzyTimeIconDrop.sprite = sunsetIcon;
        }
        else
        {
            fuzzyTimeIcon.sprite = nightIcon;
            fuzzyTimeIconDrop.sprite = nightIcon;
        }
    }
}