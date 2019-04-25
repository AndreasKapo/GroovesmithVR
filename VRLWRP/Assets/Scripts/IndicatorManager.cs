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
        if ((laneEvents[laneEventIndex].StartSample) <= nextBeatSample)
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
                    indicators[i].DeActivate();
                    indicators[i + 1].Activate();
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
        if (hitSuccess)
        {
            GameManager.instance.AddGoodHit();
        } else if (badHit)
        {
            GameManager.instance.AddBadHit();
        } else
        {
            GameManager.instance.AddMiss();
        }
    }
}
