﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterButton : MonoBehaviour {

    public Teleporter teleporter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GrabVolumeBig")
        {
            teleporter.Teleport();
        }
    }
}
