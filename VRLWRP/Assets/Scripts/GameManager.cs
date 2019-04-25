using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameEvent beatHit;
    public GameEvent endOfSongEvent;
    SimpleMusicPlayer smp;

    public List<KoreographyEvent> beatEvents;
    public int beatEventIndex;

    public List<IndicatorManager> indicatorManagers;
    public Koreography koreoGraphy;

    public int spawnEarlyInSeconds = 1;
    public bool isPlayingSong;

    


    public WorldState worldState;

    public int numBeats;
    public int numGoodHits;
    public int numBadHits;
    public int numMisses;
    
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
        smp = GetComponent<SimpleMusicPlayer>();
    }

    public void SetKoreography(AudioClip clip, Koreography koreography)
    {
        numBeats = 0;
        numGoodHits = 0;
        numBadHits = 0;
        numMisses = 0;

        //Koreographer.Instance.ClearEventRegister();
        this.koreoGraphy = koreography;
        GetComponent<AudioSource>().clip = clip;

        smp.LoadSong(koreography, 0, false);
        Koreographer.Instance.LoadKoreography(koreography);

        
        
        for(int i=0; i<indicatorManagers.Count; i++)
        {
            if(koreography.GetTrackAtIndex(i).EventID != "Beats" && koreography.GetTrackAtIndex(i).EventID != "EndOfSong") { 
                indicatorManagers[i].SetLaneEvents(koreography.GetTrackAtIndex(i));
                beatEventIndex = 0;
            }
           
        }
        for(int i =0; i < koreography.GetNumTracks(); i++)
        {
            if (koreography.GetTrackAtIndex(i).EventID == "Beats")
            {
                beatEvents = koreography.GetTrackAtIndex(i).GetAllEvents();
                numBeats += beatEvents.Count;
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



    }

    //Reads through the beat events, checks four beats ahead to see if the next beat should be spawned
    public int GetNextBeatHitTime()
    {
        if(beatEvents.Count >= (beatEventIndex + 3))
        {
            return beatEvents[beatEventIndex + 3].StartSample;
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
        beatHit.Raise();
        beatEventIndex++;
        foreach (IndicatorManager manager in indicatorManagers)
        {
            manager.BeatIterate();
        }
    }

    public void EndOfSong(KoreographyEvent evt)
    {
        endOfSongEvent.Raise();
    }

    public int GetCurrentTime()
    {
        return koreoGraphy.GetLatestSampleTime();
    }

    public void AddGoodHit()
    {
        this.numGoodHits++;
    }

    public void AddBadHit()
    {
        this.numBadHits++;
    }

    public void AddMiss()
    {
        this.numMisses++;
    }
}
