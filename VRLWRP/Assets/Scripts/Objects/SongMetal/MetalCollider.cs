using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCollider : MonoBehaviour {

    public Collider shortCollider;
    public Collider longCollider;

    private void Awake()
    {
        shortCollider.enabled = true;
        longCollider.enabled = false;
    }
    // Use this for initialization
    

    public void EnableLongCollider()
    {
        shortCollider.enabled = false;
        longCollider.enabled = true;
    }

    public void EnableGrabbable(bool grabbable)
    {
        GetComponent<OVRGrabbable>().enabled = grabbable;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
