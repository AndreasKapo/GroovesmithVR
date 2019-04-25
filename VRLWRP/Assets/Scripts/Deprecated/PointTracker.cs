using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTracker : MonoBehaviour {

    public int score;
    public int misses;
    public int falseHits;

    bool isValid;
    bool tookHit;
    public Text pointTrackerText;

    public int totalBeats;
    public PointManager PM;
    public bool isEnabled;
    public GameObject whiteCircle;

	void Start() {
        score = 0;
        misses = 0;
        falseHits = 0;
        UpdateUI();
    }

    void AddPoint()
    {
        score++;
        PM.AddPoint();
    }

    void AddMiss()
    {
        misses++;
    }

    void AddFalseHit()
    {
        falseHits++;
        PM.AddFalseHit();
    }


    public void BeginHitTime()
    {
        isValid = true;
        tookHit = false;
    }

    public void EndHitTime()
    {
        isValid = false;
        if (!tookHit)
        {
            AddMiss();
        }

        tookHit = false;
        UpdateUI();
    }

    public void TakeHit()
    {
        if (isEnabled)
        {
            if (isValid)
            {
                AddPoint();
            } else
            {
                AddFalseHit();
            }
            tookHit = true;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        pointTrackerText.text = "Hits: " + score + "\n" + "False hits: " + falseHits + "\n" + "Misses: " + misses;
    }

    public void Enable()
    {
        this.isEnabled = true;
        whiteCircle.SetActive(true);
    }

    public void Disable()
    {
        this.isEnabled = false;
        whiteCircle.SetActive(false);
    }
}
