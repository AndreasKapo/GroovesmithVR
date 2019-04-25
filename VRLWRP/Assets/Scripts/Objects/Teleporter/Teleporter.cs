using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public TeleporterHitbox hitbox;
    public Teleporter sisterTeleporter;

    public Vector3 spawnLocation;

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	public void Teleport()
    {
        if (hitbox.playerIsOnTeleporter)
        {

            GameObject player = GameObject.Find("LocalAvatarWithGrab");

            spawnLocation = player.transform.position + (sisterTeleporter.gameObject.transform.position - transform.position);

            player.transform.position = spawnLocation;
        }
    }
}
