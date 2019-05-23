using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public int beatLeewayTime = 2000;
    public int beatLeewayDebugWarningTime = 20000;
    int previousSampleTime;

    public GameEvent beatHit;
    public GameEvent endOfSongEvent;
    SimpleMusicPlayer smp;

    public List<KoreographyEvent> beatEvents;
    public int beatEventIndex;
    public int endOfSongTime;

    public List<IndicatorManager> indicatorManagers;

    public Koreography koreoGraphy;
    public SwordBlend swordBlend;

    public int spawnEarlyInSeconds = 1;
    public bool isPlayingSong;


    public KoreographyEvent currentFreeHitEvent;
    public bool isFreeHit = false;
    public bool isFreeHitTransitioning = false;
    public int freeHitEndSampleTime;
    public int freehitTransitonSampleTime;
    public FloatVariable freeHitTransitionTime;


    public WorldState worldState;


    public int numBeats;
    public int numGoodHits;
    public int numBadHits;
    public int numMisses;

    public int multiplier = 1;
    public int multiplierCounter;
    public int score;
    

    public int scorePerHit = 150;
    public int freeHitScorePerHit;

    public bool playParticles;

    
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    

    public int SampleRate
    {
        get
        {
            return koreoGraphy.SampleRate;
        }
    }

    public void SetWorldState(WorldState worldState)
    {
        this.worldState = worldState;
    }



    private void Start()
    {
        
        GameObject localAvatar = GameObject.Find("LocalAvatarWithGrab");
        localAvatar.SetActive(false);
        localAvatar.SetActive(true);

        smp = GetComponent<SimpleMusicPlayer>();
        AudioManager.instance.PlayLobbyMusic();
    }

    public void SetKoreography(AudioClip clip, Koreography koreography, string songTitle)
    {
        numBeats = 0;
        numGoodHits = 0;
        numBadHits = 0;
        numMisses = 0;
        beatEventIndex = 0;
        multiplierCounter = 0;
        score = 0;
        multiplier = 1;

        Koreographer.Instance.ClearEventRegister();

        //Koreographer.Instance.ClearEventRegister();
        this.koreoGraphy = koreography;
        GetComponent<AudioSource>().clip = clip;

        smp.LoadSong(koreography, 0, false);

        Koreographer.Instance.LoadKoreography(koreography);

        
        
        for(int i=0; i<indicatorManagers.Count; i++)
        {
            indicatorManagers[i].SetLaneEvents(koreography.GetTrackAtIndex(i));
            numBeats += koreography.GetTrackAtIndex(i).GetAllEvents().Count;
        }
        for(int i =0; i < koreography.GetNumTracks(); i++)
        {
            if (koreography.GetTrackAtIndex(i).EventID == "Beats")
            {
                beatEvents = koreography.GetTrackAtIndex(i).GetAllEvents();
            } else if(koreography.GetTrackAtIndex(i).EventID == "EndOfSong")
            {
                endOfSongTime = koreography.GetTrackAtIndex(i).GetAllEvents()[0].StartSample;
            }
        }

        //string[] eventIds = koreography.GetEventIDs();
        /* foreach(string eventId in eventIds)
         {
             if(eventId != "Beats")
             {
             Koreographer.Instance.RegisterForEvents(eventId, OnMusicalHit);
             }
         }*/
        Koreographer.Instance.RegisterForEvents("Beats", OnBeatHit);

        Koreographer.Instance.RegisterForEvents("EndOfSong", EndOfSong);

        Koreographer.Instance.RegisterForEvents("FreeHit", StartFreeHit);

    }

    public void SetSwordBlend(SwordBlend swordBlend)
    {
        this.swordBlend = swordBlend;
    }

    //Reads through the beat events, checks four beats ahead to see if the next beat should be spawned
    public int GetNextBeatHitTime()
    {
        if(beatEvents.Count >= (beatEventIndex + 2))
        {
            return beatEvents[beatEventIndex + 2].StartSample;
        } else
        {
            return beatEvents[beatEvents.Count-1].StartSample;
        }

    }

    public void OnMusicalHit(KoreographyEvent evt)
    {
        Debug.Log(evt.ToString());
    }

    public void OnBeatHit(KoreographyEvent evt)
    {
        if(previousSampleTime == 0 || (koreoGraphy.GetLatestSampleTime() > (previousSampleTime + beatLeewayTime)))
        {
            //if (previousSampleTime != 0 && koreoGraphy.GetLatestSampleTime() < (previousSampleTime + beatLeewayDebugWarningTime)) {
            //    Debug.LogWarning("Possible double beat warning! Current sample time: " + koreoGraphy.GetLatestSampleTime() + " Previous sample time: " + previousSampleTime);
            //}
            previousSampleTime = koreoGraphy.GetLatestSampleTime();
            beatHit.Raise();
            beatEventIndex++;
            foreach (IndicatorManager manager in indicatorManagers)
            {
                manager.BeatIterate();
            }
        } else
        {
            Debug.LogWarning("Double beat event found! Current sample time: " + koreoGraphy.GetLatestSampleTime() + " Previous sample time: " + previousSampleTime);
        }
        
    }

    public void StartSong()
    {
        worldState = WorldState.PlayingAnvilSong;
        this.isPlayingSong = true;

        previousSampleTime = 0;
    }

    public void StartFreeHit(KoreographyEvent evt)
    {
        if (!isFreeHit)
        {
            freeHitEndSampleTime = evt.EndSample;
            koreoGraphy.GetLatestSampleTime();
            freehitTransitonSampleTime = freeHitEndSampleTime - (int)(freeHitTransitionTime.Value * koreoGraphy.SampleRate);
            isFreeHit = true;
            isFreeHitTransitioning = false;
            foreach (IndicatorManager manager in indicatorManagers)
            {
                manager.StartFreeHit();
            }
        }
    }

    public void StartFreeHitTransition()
    {
        isFreeHitTransitioning = true;
        foreach (IndicatorManager manager in indicatorManagers)
        {
            manager.StartFreeHitTransition();
        }
    }

    public void StopFreeHit()
    {
        isFreeHit = false;
        isFreeHitTransitioning = false;
        foreach (IndicatorManager manager in indicatorManagers)
        {
            manager.StopFreeHit();
        }
    }

    public void EndSong()
    {
        
        foreach (IndicatorManager manager in indicatorManagers)
        {
            manager.DeactivateAllIndicators();
        }
        previousSampleTime = 0;
        smp.StopAllCoroutines();
        smp.Stop();
        this.isPlayingSong = false;
        endOfSongEvent.Raise();
    }

    public void EndOfSong(KoreographyEvent evt)
    {
        EndSong();
        //endOfSongEvent.Raise();
    }

    public int GetCurrentTime()
    {
        return koreoGraphy.GetLatestSampleTime();
    }

    public void AddGoodHit()
    {
        this.numGoodHits++;
        swordBlend.GoodHit(100f / numBeats);

        multiplierCounter++;
        CalculateMultiplier();

        score += scorePerHit * multiplier;
    }

    public void AddBadHit()
    {
        this.numBadHits++;
        swordBlend.BadHit(100f / numBeats);

        multiplierCounter = 0;
        CalculateMultiplier();

    }

    public void AddMiss()
    {
        this.numMisses++;
        swordBlend.MissHit(100f / numBeats);

        multiplierCounter = 0;
        CalculateMultiplier();
    }

    public void AddFreeHit()
    {
        score += freeHitScorePerHit;
    }

    void CalculateMultiplier()
    {

        if (multiplierCounter > 30)
        {
            multiplier = 4;
        }
        else if (multiplierCounter > 20)
        {
            multiplier = 3;
        }
        else if (multiplierCounter > 10)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
    }

    public int GetTimeLeftInSong()
    {
        int timeLeftInSong = (endOfSongTime - koreoGraphy.GetLatestSampleTime())/(koreoGraphy.SampleRate);
        return timeLeftInSong;
    }


}
