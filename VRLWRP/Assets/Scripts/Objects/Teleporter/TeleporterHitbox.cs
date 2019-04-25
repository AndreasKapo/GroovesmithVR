using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterHitbox : MonoBehaviour {

    public bool playerIsOnTeleporter = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "PlayerHeadHitbox")
        {
            playerIsOnTeleporter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerHeadHitbox")
        {
            playerIsOnTeleporter = false;
        }
    }


}
