using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCollider : MonoBehaviour {

    public Collider shortCollider;
    public Collider longCollider;
    Rigidbody rb;
    public bool isOnAnvil;
    public string transformName;

    private void Awake()
    {
        EnableShortCollider();
        rb = GetComponent<Rigidbody>();
    }
    // Use this for initialization


    public void EnableLongCollider()
    {
        shortCollider.enabled = false;
        longCollider.enabled = true;
    }
    public void EnableShortCollider()
    {
        shortCollider.enabled = true;
        longCollider.enabled = false;
    }

    public void EnableGrabbable(bool grabbable)
    {
        GetComponent<OVRGrabbable>().enabled = grabbable;
    }

    public void SetKinematic(bool kinematic)
    {
        rb.isKinematic = kinematic;
    }

    public void PutOnAnvil()
    {
        SetKinematic(true);
        isOnAnvil = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(!isOnAnvil && (GetComponent<OVRGrabbable>().enabled && GetComponent<OVRGrabbable>().grabbedBy == null))
        {
            SetKinematic(false);
        }
	}
}
