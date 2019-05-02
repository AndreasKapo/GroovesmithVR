using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilMetalTrigger : MonoBehaviour {

    public Transform metalSongLocation;
    public GameEvent beginAnvilTransition;

    SongMetal songMetal;
    bool hasMetal;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "SongMetal" && !other.GetComponent<OVRGrabbable>().isGrabbed && !hasMetal)
        {
            other.transform.position = metalSongLocation.position;
            other.transform.rotation = metalSongLocation.rotation;
            other.GetComponent<MetalCollider>().PutOnAnvil();

            songMetal = other.GetComponent<SongMetal>();
            GameManager.instance.SetKoreography(songMetal.clip, songMetal.koreography, songMetal.songTitle);
            GameManager.instance.SetSwordBlend(other.GetComponent<SwordBlend>());

            hasMetal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SongMetal" && other.GetComponent<SongMetal>().songTitle == songMetal.songTitle)
        {
            hasMetal = false;
            GameManager.instance.EndSong();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HammerHead" && hasMetal && !GameManager.instance.isPlayingSong)
        {
            beginAnvilTransition.Raise();
        }
    }
}
