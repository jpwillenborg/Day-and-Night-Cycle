using UnityEngine;
using System;
using System.Collections.Generic;
using DistantLands.Cozy;
using DistantLands.Cozy.Data;


[Serializable]
public struct LampData
{
    public MeshRenderer renderer;
    public int materialIndex;
    public Material onMaterial;
    public Material offMaterial;
}


public class SwitchLights : MonoBehaviour
{
    [SerializeField]
    private GameObject lightGroup;
    [SerializeField]
    private LampData[] lampData;
    private Material[] lampMaterials;

    private CozyTimeModule timeModule;
    private float morning = 0.2916667f;
    private float evening = 0.7083333f;
    private bool nightTime;

    private List<Light> lights;
    private List<float> maxIntensities;

    public float duration = 2f;
    private float timer = 0f;
    private bool isRampingOn = false;
    private bool isRampingOff = false;


    void Awake()
    {
        lights = new List<Light>();

        // maxIntensities = new List<float>();
        maxIntensities = new List<float> {1.34f, 1.34f, 1.34f, 1.34f, 2.5f, 2.0f, 2.0f, 2.0f, 0.86f, 0.5f, 0.5f};


        foreach (Transform childTransform in lightGroup.transform)
        {
            GameObject childGameObject = childTransform.gameObject;
            lights.Add(childGameObject.GetComponent<Light>());
            // maxIntensities.Add(childGameObject.GetComponent<Light>().intensity);
        }


        timeModule = CozyWeather.instance.timeModule;


        if ((float)timeModule.currentTime > morning && (float)timeModule.currentTime < evening)
        {
            nightTime = false;
            TurnLightsOff();
        } else
        {
            nightTime = true;
            TurnLightsOn();
        }


        if (!nightTime)
        {
            foreach (Light light in lights)
            {
                light.intensity = 0;
            }
        }
    }


    void Update()
    {
        if (nightTime)
        {
            if ((float)timeModule.currentTime > morning && (float)timeModule.currentTime < evening)
            {
                nightTime = false;

                if (TimePause.isPaused)
                {
                    isRampingOn = false;
                    isRampingOff = false;
                    TurnLightsOff();
                } else
                {
                    RampLightsOff();
                }
            }
        } else
        {
            if ((float)timeModule.currentTime > evening || (float)timeModule.currentTime < morning)
            {
                nightTime = true;
                
                if (TimePause.isPaused)
                {
                    isRampingOn = false;
                    isRampingOff = false;
                    TurnLightsOn();
                } else
                {
                    RampLightsOn();
                }
            }
        }


        if (isRampingOn)
        {
            timer += Time.deltaTime / duration;

            for (var i = 0; i < lights.Count; i++)
            {
                float currentIntensity = Mathf.Lerp(0, maxIntensities[i], timer);
                lights[i].intensity = currentIntensity;
            }

            if (timer >= 1f) isRampingOn = false;

            SwitchMaterials();
        }


        if (isRampingOff)
        {
            timer += Time.deltaTime / duration;

            for (var i = 0; i < lights.Count; i++)
            {
                float currentIntensity = Mathf.Lerp(maxIntensities[i], 0, timer);
                lights[i].intensity = currentIntensity;
            }

            if (timer >= 1f) isRampingOff = false;

            SwitchMaterials();
        }
    }


    void TurnLightsOn()
    {
        SwitchMaterials();

        for (var i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = maxIntensities[i];
        }       
    }


    public void RampLightsOn()
    {
        timer = 0f;
        isRampingOn = true;
    }


    void TurnLightsOff()
    {
        SwitchMaterials();

        for (var i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = 0;
        }   
    }


    public void RampLightsOff()
    {
        timer = 0f;
        isRampingOff = true;
    }


    void SwitchMaterials()
    {
        for (int i = 0; i < lampData.Length; i++)
        {
            lampMaterials = lampData[i].renderer.materials;
            if (nightTime)
            {
                lampMaterials[lampData[i].materialIndex] = lampData[i].onMaterial;
            } else
            {
                lampMaterials[lampData[i].materialIndex] = lampData[i].offMaterial;
            }
            lampData[i].renderer.materials = lampMaterials;
        }
    }
}