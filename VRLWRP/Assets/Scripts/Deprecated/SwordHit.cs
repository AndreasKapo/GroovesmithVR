using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour {


    public GameObject hitParticle;
    public PointTracker tracker;
    public SongGenerator generator;
    public SwordHitboxType hitboxIndex;

    bool isHittable = true;
    public FloatVariable hitCooldownTime;
    float hitCoolTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isHittable)
        {
            hitCoolTimer += Time.deltaTime;
            if(hitCoolTimer > hitCooldownTime.Value)
            {
                isHittable = true;
                hitCoolTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HammerHead")
        {
            if (isHittable)
            {
           
                string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
                if(handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
                {
                    ControllerManager.CM.StartVibration(1, 1, 0.1f, OVRInput.Controller.RTouch);
                } else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
                {
                    ControllerManager.CM.StartVibration(1, 1, 0.1f, OVRInput.Controller.LTouch);
                }

                isHittable = false;

            
                tracker.TakeHit();
                generator.TakeHit(hitboxIndex);
                GameObject hitInstance = GameObject.Instantiate(hitParticle, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
                hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            }

        }
    }
}
