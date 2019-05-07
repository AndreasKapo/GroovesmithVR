using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class IndicatorManager : MonoBehaviour
{
    public bool autoActivate;
    public Indicator[] indicators;
    List<KoreographyEvent> laneEvents = new List<KoreographyEvent>();
    int laneEventIndex = 0;
    int timeBeforeSpawn;

    private void Update()
    {
        
    }

    void CheckSpawnNext()
    {
        //
        int nextBeatSample = GameManager.instance.GetNextBeatHitTime();
        if (laneEventIndex < laneEvents.Count && (laneEvents[laneEventIndex].StartSample) <= nextBeatSample)
        {
            ActivateInitialIndicator();
            laneEventIndex++;
        }
    }

    public void BeatIterate()
    {
        

        //Debug.Log("BeatIterate " + GameManager.instance.koreoGraphy.GetLatestSampleTime());
        bool foundActivated = false;
        for(int i= indicators.Length-1; i>=0; i--)
        {
            if (i != indicators.Length - 1)
            {
                if (indicators[i].activated)
                {
                    foundActivated = true;
                    if (!indicators[i].inert)
                    {
                        indicators[i + 1].Activate();
                    } else if (indicators[i].inert)
                    {
                        indicators[i + 1].ActivateInert();
                    }
                    indicators[i].DeActivate();
                    //break;
                }
            }
            else if (i == indicators.Length - 1)
            {
                if (indicators[i].activated)
                {
                    
                    IndicatorDone(indicators[i].hitSuccess, indicators[i].badHit);
                    foundActivated = true;
                    indicators[i].DeActivate();
                    indicators[i].hitSuccess = false;
                    indicators[i].badHit = false;
                    
                    
                }
            }
        }
        if (!foundActivated && autoActivate)
        {
            indicators[0].Activate();
        }

        CheckSpawnNext();
    }

    public void ActivateInitialIndicator()
    {
        indicators[0].Activate();
    }

    public void SetLaneEvents(KoreographyTrackBase track)
    {

        timeBeforeSpawn = GameManager.instance.spawnEarlyInSeconds * GameManager.instance.SampleRate; 
        this.laneEvents = track.GetAllEvents();
        laneEventIndex = 0;
    }

    void IndicatorDone(bool hitSuccess, bool badHit)
    {
        if (badHit)
        {
            GameManager.instance.AddBadHit();
        }
        else if (hitSuccess)
        {
            GameManager.instance.AddGoodHit();
        }
        else
        {
            GameManager.instance.AddMiss();
        }
    }

    public void DeactivateAllIndicators()
    {
        for(int i=0; i<indicators.Length; i++)
        {
            indicators[i].DeActivate();
        }
    }

    public void StartFreeHit()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].EnableFreeHit();
        }
    }

    public void StartFreeHitTransition()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].BeginFreeHitTransition();
        }
    }

    public void StopFreeHit()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].DisableFreeHit();
        }
    }
}
