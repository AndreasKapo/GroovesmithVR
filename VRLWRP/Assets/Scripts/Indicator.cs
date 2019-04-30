﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour

{
    

    public bool activated;
    public bool freeHit;
    public bool inert;
    public bool hitSuccess = false;
    public bool badHit = false;

    bool freeHitCalledFromManager = false;

    public Material activatedMaterial;
    public Material inactiveMaterial;
    public Material inertMaterial;

    public Material freeHitMaterial;
    public Material freeHitColorMaterial1;
    public Material freeHitColorMaterial2;
    bool pulsingUp;
    float freeHitPulseTimer;

    public FloatVariable freeHitPulseTransitionTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freeHit)
        {
            Pulse();
        }
    }

    void Pulse()
    {

        if (pulsingUp)
        {
            freeHitPulseTimer += Time.deltaTime * freeHitPulseTransitionTime.Value;
            if (freeHitPulseTimer >= 1)
            {
                freeHitPulseTimer = 1;
                pulsingUp = false;
            }
        }
        else
        {
            freeHitPulseTimer -= Time.deltaTime * freeHitPulseTransitionTime.Value;
            if (freeHitPulseTimer <= 0)
            {
                freeHitPulseTimer = 0;
                pulsingUp = true;
            }
        }

        GetComponent<Renderer>().material.Lerp(activatedMaterial, inertMaterial, freeHitPulseTimer);

    }


    public void Activate()
    {
        if (!freeHit)
        {

            GetComponent<Renderer>().material = activatedMaterial;
            activated = true;
            inert = false;

        }
        //Debug.Log(gameObject.name + "  Activated   " + GameManager.instance.koreoGraphy.GetLatestSampleTime());
    }

    public void DeActivate()
    {
       
        GetComponent<Renderer>().material = inactiveMaterial;
        activated = false;
        inert = false;
        // Debug.Log(gameObject.name + "  DeActivated   " + GameManager.instance.koreoGraphy.GetLatestSampleTime());

    }

    public void ActivateInert()
    {
        if (!freeHit)
        {
            GetComponent<Renderer>().material = inertMaterial;
            activated = true;
            inert = true;
        }
    }

    public void RenderInert()
    {
        if (!freeHit)
        {
            GetComponent<Renderer>().material = inertMaterial;
            activated = true;
            inert = true;
        }
    }

    public void EnableFreeHit()
    {
        
        freeHit = true;
        pulsingUp = true;
        GetComponent<Renderer>().material = freeHitMaterial;
        
    }

    public void DisableFreeHit()
    {
        freeHit = false;
        DeActivate();
    }

    private void LateUpdate()
    {
        
    }
}
