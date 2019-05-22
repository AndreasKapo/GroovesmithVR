using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    Text text;
    public Image circleImage;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.worldState == WorldState.PlayingAnvilSong)
        {
            text.text = "" + GameManager.instance.GetTimeLeftInSong() + "s \n" + GameManager.instance.koreoGraphy.GetLatestSampleTime();
            circleImage.fillAmount = 1 - ((float)GameManager.instance.koreoGraphy.GetLatestSampleTime() / (float)GameManager.instance.endOfSongTime);
        }
    }
}
