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
        None,
        DimDirectionalLight,
        BrightenSpotLight
    }

    TransitionToAnvilStates transitionToAnvilState;

    enum TransitionFromAnvilToDefaultStates
    {
        None,
        DimSpotLight,
        BrightenDirectionalLight
    }
    TransitionFromAnvilToDefaultStates transitionFromAnvilToDefaultState;

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
        TransitionFromAnvilToDefault();
        PlayAnvil();
    }

    public void StartTransitionToAnvil()
    {
        GameManager.instance.worldState = WorldState.TransitioningToAnvil;
        timer = 0f;

        directionalLightIntensity = directionalLight.GetComponent<Light>().intensity;
        spotLightIntensity = spotLight.GetComponent<Light>().intensity;

        transitionToAnvilState = TransitionToAnvilStates.DimDirectionalLight;
    }
    public void StartTransitionFromAnvilToDefault()
    {
        GameManager.instance.worldState = WorldState.TransitionFromAnvilToDefault;
        timer = 0f;

        directionalLightIntensity = directionalLight.GetComponent<Light>().intensity;
        spotLightIntensity = spotLight.GetComponent<Light>().intensity;

        transitionFromAnvilToDefaultState = TransitionFromAnvilToDefaultStates.DimSpotLight;
    }

    void TransitionFromAnvilToDefault()
    {
        if (GameManager.instance.worldState == WorldState.TransitionFromAnvilToDefault)
        {
            if (transitionFromAnvilToDefaultState == TransitionFromAnvilToDefaultStates.DimSpotLight)
            {
                timer += Time.deltaTime;

                if (timer <= spotLightTransitionTime)
                {
                    spotLightIntensity = targetSpotLightIntensity - (targetSpotLightIntensity * (timer / spotLightTransitionTime));
                    spotLight.GetComponent<Light>().intensity = spotLightIntensity;
                }
                else
                {
                    transitionFromAnvilToDefaultState = TransitionFromAnvilToDefaultStates.BrightenDirectionalLight;
                    timer = 0f;
                }
            }
            else if (transitionFromAnvilToDefaultState == TransitionFromAnvilToDefaultStates.BrightenDirectionalLight)
            {
                timer += Time.deltaTime;

                if (timer <= directionalLightTransitionTime)
                {
                    directionalLightIntensity = targetDirectionalLightIntensity * (timer / directionalLightTransitionTime);
                    directionalLight.GetComponent<Light>().intensity = directionalLightIntensity;
                }
                else
                {
                    GameManager.instance.worldState = WorldState.Default;
                    timer = 0f;
                }
            }
        }
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
                    Debug.Log("Start Song event Risen");
                    startSongEvent.Raise();

                }
            }

        }
        
    }

    void PlayAnvil()
    {
        if(GameManager.instance.worldState == WorldState.PlayingAnvilSong)
        {
            if(GameManager.instance.isFreeHit) {

                Debug.Log("GameManager.instance.freehitTransitonSampleTime: " + GameManager.instance.freehitTransitonSampleTime);
                Debug.Log("GameManager.instance.koreoGraphy.GetLatestSampleTime(): " + GameManager.instance.koreoGraphy.GetLatestSampleTime());
                if (GameManager.instance.freeHitEndSampleTime < GameManager.instance.koreoGraphy.GetLatestSampleTime())
                {
                    GameManager.instance.StopFreeHit();
                } else if (GameManager.instance.freehitTransitonSampleTime < GameManager.instance.koreoGraphy.GetLatestSampleTime() && !GameManager.instance.isFreeHitTransitioning)
                {
                    GameManager.instance.StartFreeHitTransition();
                }
            }
        }
    }

   
}
