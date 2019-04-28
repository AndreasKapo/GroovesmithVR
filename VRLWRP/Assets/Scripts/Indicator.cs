using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour

{
    

    public bool activated;
    public bool inert;
    public bool hitSuccess = false;
    public bool badHit = false;

    public Material activatedMaterial;
    public Material inactiveMaterial;
    public Material inertMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Activate()
    {
        GetComponent<Renderer>().material = activatedMaterial;
        activated = true;
        inert = false;
        
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
        GetComponent<Renderer>().material = inertMaterial;
        activated = true;
        inert = true;
    }

    public void RenderInert()
    {
        GetComponent<Renderer>().material = inertMaterial;
        activated = true;
        inert = true;
    }
}
