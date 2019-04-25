using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilMetalTrigger : MonoBehaviour {

    public Transform metalSongLocation;
    public GameEvent beginAnvilTransition;

    bool hasMetal;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "SongMetal" && !other.GetComponent<OVRGrabbable>().isGrabbed && !hasMetal)
        {
            other.transform.position = metalSongLocation.position;
            other.transform.rotation = metalSongLocation.rotation;

            SongMetal songMetal = other.GetComponent<SongMetal>();
            GameManager.instance.SetKoreography(songMetal.clip, songMetal.koreography);

            hasMetal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SongMetal")
        {
            hasMetal = false;
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
