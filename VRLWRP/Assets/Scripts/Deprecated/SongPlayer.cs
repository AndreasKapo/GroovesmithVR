using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongPlayer : MonoBehaviour {
    
    public MusicPlayer player;
    public PointManager PM;

    public Beat beat1;
    public Beat beat2;
    public Beat beat3;
    bool isPlayingSong;

    float timer;
    public Text timeLeftText;

    public bool enableAll;

    AudioSource metronome;

    public float bpm;
    float timeBetweenMetronomeHits;

    public int numTimesToTick = 4;
    int tickCounter = 0;
    bool isPlayingMetronome = false;

	// Use this for initialization
	void Start () {
        timeBetweenMetronomeHits = (1 / bpm) * 60;
        metronome = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayingSong)
        {
            if (!player.isPlayingSong)
            {
                isPlayingSong = false;
            } else
            {
               int timeleft = (int)((player.audioSource.clip.length - player.audioSource.time));
                timeLeftText.text = "" + timeleft + "s";
            }

            timer += Time.deltaTime;

            
        } else if (isPlayingMetronome)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenMetronomeHits)
            {
                metronome.Play();
                timer = 0f;
                tickCounter++;
            }

            if(tickCounter >= 4)
            {
                isPlayingMetronome = false;
                PlaySong();
            }
        }
	}

    public void PlaySong()
    {
        //ScriptableObject.CreateInstance("SongNodes");

        timer = 0f;

        player.StartPlayingMusic();
        isPlayingSong = true;

        beat1.StartSong();
        beat2.StartSong();
        beat3.StartSong();

        PM.StartSong();

        if (enableAll)
        {
            PM.EnableAll();
        }
    }

    private void PlayMetronome()
    {
        timer = 0f;
        tickCounter = 0;
        isPlayingMetronome = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HammerHead")
        {
            PlayMetronome();
        }
    }
}
