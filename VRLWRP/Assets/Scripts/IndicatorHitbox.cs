using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHitbox : MonoBehaviour
{
    public Indicator yellowIndicator;
    public Indicator greenIndicator;
    public GameObject hitParticle;

    Transform playerCamera;

    [SerializeField]
    FloatVariable particleHeightDiff;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("OVRCameraRig").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isPlayingSong)
        {
            if (other.tag == "HammerHead" && greenIndicator.activated)
            {
                GoodHitReaction(other);
            } else if(other.tag == "HammerHead" && yellowIndicator.activated)
            {
                EarlyHitReaction(other);
            }
            else if (other.tag == "HammerHead" && !greenIndicator.activated)
            {
                MissHitReaction(other);
            }
        }
       
    }

    void GoodHitReaction(Collider other)
    {
        string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
        if (handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
        {
            ControllerManager.CM.StartVibration(1, 1, 0.1f, OVRInput.Controller.RTouch);
        }
        else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
        {
            ControllerManager.CM.StartVibration(1, 1, 0.1f, OVRInput.Controller.LTouch);
        }
        GameObject hitInstance = GameObject.Instantiate(hitParticle, other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0), Quaternion.identity);
        hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
        //hitInstance.transform.LookAt(playerCamera);
        greenIndicator.hitSuccess = true;
    }

    void MissHitReaction(Collider other)
    {
        GameManager.instance.AddBadHit();
    }

    void EarlyHitReaction(Collider other)
    {

    }
}
