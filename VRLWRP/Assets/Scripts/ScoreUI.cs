using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    int numBeats;
    int numGoodHits;
    int numBadHits;
    int numMisses;

    float blendOne;
    float blendTwo;
    float blendThree;

    int multiplierCounter;
    int multiplier;
    int score;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        numBeats = GameManager.instance.numBeats;
        numGoodHits = GameManager.instance.numGoodHits;
        numBadHits = GameManager.instance.numBadHits;
        numMisses = GameManager.instance.numMisses;

        /*
        string textString = "numBeats: " + numBeats;
        textString += "\n numGoodHits: " + numGoodHits;
        textString += "\n numBadHits: " + numBadHits;
        textString += "\n numMisses: " + numMisses;
        */
        /*
        blendOne = GameManager.instance.swordBlend.blendOne;
        blendTwo = GameManager.instance.swordBlend.blendTwo;
        blendThree = GameManager.instance.swordBlend.blendThree;

        textString += "\n";
        textString += "\n blendOne: " + blendOne;
        textString += "\n blendTwo: " + blendTwo;
        textString += "\n blendThree: " + blendThree;*/
        /*
        multiplierCounter = GameManager.instance.multiplierCounter;
        multiplier = GameManager.instance.multiplier;
        score = GameManager.instance.score;

        textString += "\n";
        textString += "\n multiplierCounter: " + multiplierCounter;
        textString += "\n multiplier: " + multiplier;
        textString += "\n score: " + score; 
        */
        string textString = "" + GameManager.instance.score;
         text.text = textString;
    }


}
