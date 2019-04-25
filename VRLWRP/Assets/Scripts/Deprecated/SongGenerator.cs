using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SonicBloom.Koreo;

[System.Serializable]
struct EditableNode
{
    public SongNodes beat;
    public bool edit;
}
public class SongGenerator : MonoBehaviour {

    [SerializeField]
    KoreographyTrack track1;
    [SerializeField]
    KoreographyTrack track2;
    [SerializeField]
    KoreographyTrack track3;

    [SerializeField]
    Koreographer koreographer;
    public MusicPlayer player;
    public PointManager manager;


    bool isPlayingSong;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayingSong)
        {
            if (!player.isPlayingSong)
            {
                isPlayingSong = false;
            }
            
        }
	}

    public void TakeHit(SwordHitboxType hitboxIndex)
    {
        if (isPlayingSong)
        {
            KoreographyTrack track = null;
            switch (hitboxIndex)
            {
                case SwordHitboxType.Box1:
                    track = track1;
                    break;
                case SwordHitboxType.Box2:
                    track = track2;
                    break;
                case SwordHitboxType.Box3:
                    track = track3;
                    break;
            }

            KoreographyEvent koreographyEvent = new KoreographyEvent();
            koreographyEvent.StartSample = koreographer.GetMusicSampleTime("m_envir_anvil");

            track.AddEvent(koreographyEvent);
        }
    }

    public void CreateSong()
    {
        manager.EnableAll();

        /*
        if (beat1.edit)
        {
            EditorUtility.SetDirty(beat1.beat);
            beat1.beat.nodes = new List<SongNode>();
        }

        if (beat2.edit)
        {
            EditorUtility.SetDirty(beat2.beat);
            beat2.beat.nodes = new List<SongNode>();
        }

        if (beat3.edit)
        {
            EditorUtility.SetDirty(beat3.beat);
            beat3.beat.nodes = new List<SongNode>();
        }
        */

        track1.RemoveAllEvents();
        track2.RemoveAllEvents();
        track3.RemoveAllEvents();

        //ScriptableObject.CreateInstance("SongNodes");
        player.StartPlayingMusic();
        isPlayingSong = true;



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HammerHead")
        {
            CreateSong();
        }
    }
}
