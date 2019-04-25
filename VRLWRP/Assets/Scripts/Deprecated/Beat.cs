using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;

public class Beat : MonoBehaviour {

    public float beatsPerMinute;
    float timeBetweenBeats;
    public float delayBeforeSound;

    public Material red;
    public Material green;

    Renderer renderer;
    

    bool isOn;
    AudioSource sound;

    public float leeWay = 0.1f;
    bool playedSound = false;
    
    public GameObject expandingCirclePrefab;
    public Transform targetCircle;

    
    public PointTracker tracker;
    public bool isEnabled;

    public string koreographerEventID;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        sound = GetComponent<AudioSource>();

        Koreographer.Instance.RegisterForEvents(koreographerEventID, OnMusicalHit);
    }
    

    public void StartSong()
    {
        isOn = true;
    }


    private void OnMusicalHit(KoreographyEvent evt)
    {
        Debug.Log("koreographerEventID: " + koreographerEventID);
        MakeExpander();
    }

    // Update is called once per frame
    void Update () {

	}

    public void MakeExpander()
    {
        Expander expander = GameObject.Instantiate(expandingCirclePrefab, targetCircle, false).GetComponent<Expander>();
        expander.gameObject.transform.position = targetCircle.position;
        expander.targetCircle = transform;
        expander.leeWay = this.leeWay;
        expander.tracker = tracker;

        expander.SetEnabled(this.isEnabled);
    }

    //Deprecated code, keeping for sentimental value <3
    void BeatRhythm()
    {
        //timeBetweenBeats = 1 / (beatsPerMinute / 60f);
        //timer += Time.deltaTime;
        //if (timer > timeBetweenBeats)
        //{
        //    timer = timeBetweenBeats;
        //}

        //expandingCircle.localScale = initialScale + ((targetScale - initialScale) * (timer / timeBetweenBeats));
        //Debug.Log("BPM : :" + beatsPerMinute + " expandingCircle.localScale: " + expandingCircle.localScale.x + "," + expandingCircle.localScale.y + "," + expandingCircle.localScale.z);
        //if (timer >= (timeBetweenBeats - delayBeforeSound) && !playedSound)
        //{
        //    sound.Play();
        //    playedSound = true;
        //}

        //if (timer >= timeBetweenBeats)
        //{
        //    timer = 0f;
        //    isOff = false;
        //    playedSound = false;
            // renderer.material = green;
        //}

        //if (!isOff)
        //{
        //    if (timer > timeToLight)
        //    {
        //        isOff = true;
                // renderer.material = red;
        //    }
        //}
    }
}
