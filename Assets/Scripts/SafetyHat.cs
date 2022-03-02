using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyHat : GrabInteractable
{
    [SerializeField] private GameObject safetyLight;
    [SerializeField] private Material lightOn;
    [SerializeField] private Material lightOff;
    [SerializeField] private MeshRenderer lightRenderer;

    private bool isLightOn;

    public override void Start()
    {
        base.Start();
        LightOFF();
    }

    void LightON()
    {
        isLightOn = true;
        lightRenderer.material = lightOn;
        safetyLight.SetActive(true);
    }

    void LightOFF()
    {
        isLightOn = false;
        lightRenderer.material = lightOff;
        safetyLight.SetActive(false);
    }

    public override void OnTriggerStart()
    {

        LightON();
    }

    public override void OnTriggerEnd()
    {
        LightOFF();
    }
}
