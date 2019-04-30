using System.Collections;
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
    public Color freeHitColor1;
    public Color freeHitColor2;
    bool pulsingUp;

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
            
            /*if(freeHitPulseTimer > freeHitPulseTransitionTime.Value)
            {
                freeHitPulseTimer = 0;
                pulsingUp = !pulsingUp;
            }*/

            Pulse();
        }
    }

    void Pulse()
    {

        freeHitMaterial.color = Color.Lerp(freeHitColor1, freeHitColor2, Mathf.PingPong(Time.time, freeHitPulseTransitionTime.Value));
        
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
