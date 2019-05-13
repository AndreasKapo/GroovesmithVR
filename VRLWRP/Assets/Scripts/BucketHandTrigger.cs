using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketHandTrigger : MonoBehaviour
{

    public GameObject textToDisplay;
    public string handString;
    // Start is called before the first frame update
    void Start()
    {
        textToDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == handString)
        {
            textToDisplay.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == handString)
        {
            textToDisplay.SetActive(false);
        }
    }
}
