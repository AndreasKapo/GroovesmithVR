using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    float directionalLightIntensity;
    float spotLightIntensity;

    float targetSpotLightIntensity;
    float targetDirectionalLightIntensity;
    public GameObject spotLight;
    public GameObject directionalLight;
    public float directionalLightTransitionTime;
    public float spotLightTransitionTime;

    public GameEvent startSongEvent;

    float timer = 0f;

    enum TransitionToAnvilStates
    {
        DimDirectionalLight,
        BrightenSpotLight
    }

    TransitionToAnvilStates transitionToAnvilState;

    // Start is called before the first frame update
    void Start()
    {
        targetSpotLightIntensity = spotLight.GetComponent<Light>().intensity;
        targetDirectionalLightIntensity = directionalLight.GetComponent<Light>().intensity;
        spotLight.GetComponent<Light>().intensity = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

        TransitionToAnvil();
    }

    public void StartTransitionToAnvil()
    {
        GameManager.instance.worldState = WorldState.TransitioningToAnvil;
        timer = 0f;

        directionalLightIntensity = directionalLight.GetComponent<Light>().intensity; 
        spotLightIntensity = spotLight.GetComponent<Light>().intensity;

        transitionToAnvilState = TransitionToAnvilStates.DimDirectionalLight;
    }

    public void TransitionToAnvil()
    {
        if (GameManager.instance.worldState == WorldState.TransitioningToAnvil)
        {
            if (transitionToAnvilState == TransitionToAnvilStates.DimDirectionalLight)
            {

                timer += Time.deltaTime;

                if (timer <= directionalLightTransitionTime)
                {
                    directionalLightIntensity = targetDirectionalLightIntensity - (targetDirectionalLightIntensity * (timer / directionalLightTransitionTime));
                    directionalLight.GetComponent<Light>().intensity = directionalLightIntensity;
                }
                else
                {
                    transitionToAnvilState = TransitionToAnvilStates.BrightenSpotLight;
                    timer = 0f;
                }
            }
            else if (transitionToAnvilState == TransitionToAnvilStates.BrightenSpotLight)
            {

                timer += Time.deltaTime;

                if (timer <= spotLightTransitionTime)
                {
                    spotLightIntensity = targetSpotLightIntensity * (timer / spotLightTransitionTime);
                    spotLight.GetComponent<Light>().intensity = spotLightIntensity;
                }
                else
                {
                    startSongEvent.Raise();
                }
            }

        }
        
    }
}
