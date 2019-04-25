using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class MusicPlayer : MonoBehaviour {

    public AudioSource audioSource;
    public bool isPlayingSong;

    SimpleMusicPlayer smp;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        smp = GetComponent<SimpleMusicPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayingSong)
        {
            if (!audioSource.isPlaying)
            {
                isPlayingSong = false;
            }
        }
	}

    public void StartPlayingMusic()
    {
        GameManager.instance.isPlayingSong = true;
        smp.Play();
        isPlayingSong = true;
    }
}
