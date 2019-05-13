using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyCreation : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI breakdownText1;
    public TextMeshProUGUI breakdownText2;

    public GameObject targetRenderPlane;
    public Text renderTextScore;

    public bool useTextMesh;
    public bool useTargetRender;

    // Start is called before the first frame update
    void Start()
    {
        if (useTextMesh)
        {
            WriteScoreTextMesh();
            WriteBreakdown();
        } else
        {
            MakeTextMeshEmpty();
        }

        if (useTargetRender)
        {
            WriteScoreTargetRender();
        }
        else
        {
            DestroyTargetRender();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyTargetRender()
    {
        Destroy(targetRenderPlane);
    }

    void MakeTextMeshEmpty()
    {
        Destroy(scoreText.gameObject);
        Destroy(breakdownText1.gameObject);
        Destroy(breakdownText2.gameObject);
    }

    void WriteScoreTextMesh()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    void WriteScoreTargetRender()
    {
        GameObject.Find("TargetRenderScoreText").GetComponent<Text>().text = "" + GameManager.instance.score;
    }

    void WriteBreakdown()
    {
        int numBeats = GameManager.instance.numBeats;
        int numGoodHits = GameManager.instance.numGoodHits;
        int numBadHits = GameManager.instance.numBadHits;
        int numMisses = GameManager.instance.numMisses;


        int score = GameManager.instance.score;

        string textString = "Number of proper hits: " + numGoodHits;
        textString += "\n Number of Bad Hits: " + numBadHits;
        textString += "\n Number of Misses: " + numMisses;

        breakdownText1.text = textString;

        textString = "Final: " + numGoodHits + "\\" + numBeats;
        textString += "\n Percentage: " + ((float)numGoodHits/(float)numBeats) + "%";
        textString += "\n Score: " + score;

        breakdownText2.text = textString;
    }
}
