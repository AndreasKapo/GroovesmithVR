using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHitbox : MonoBehaviour
{

    public Indicator yellowIndicator;
    public Indicator greenIndicator;
    public GameObject freeHitParticle;
    public GameObject hitParticle;
    public GameObject badHitParticle;

    Transform playerCamera;

    [SerializeField]
    FloatVariable particleHeightDiff;
    

    bool isHittable = true;
    public FloatVariable hitCooldownTime;
    float hitCoolTimer = 0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("OVRCameraRig").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHittable)
        {
            hitCoolTimer += Time.deltaTime;
            if (hitCoolTimer > hitCooldownTime.Value)
            {
                isHittable = true;
                hitCoolTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isPlayingSong)
        {
            if(other.tag == "HammerHead" && isHittable){
                isHittable = false;
                if (greenIndicator.freeHit)
                {
                    FreeHitReaction(other);
                } else if (greenIndicator.activated)
                {
                    if (!greenIndicator.inert)
                    {
                        GoodHitReaction(other);
                    }
                }
                else if (yellowIndicator.activated)
                {
                    EarlyHitReaction(other);
                }
                else
                {
                    BadHitReaction(other);
                }
            }
            
        }
       
    }

    void FreeHitReaction(Collider other)
    {
        try
        {
            string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
            if (handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.RTouch);
            }
            else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.LTouch);
            }
        }
        catch (System.NullReferenceException e) { }
        if (GameManager.instance.playParticles)
        {
            //GameObject hitInstance = GameObject.Instantiate(freeHitParticle, other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0), Quaternion.identity);
            GameObject hitInstance = ParticleManager.instance.GetFreeHitParticle();
            hitInstance.transform.position = other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0);
            hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            hitInstance.GetComponent<ParticleSystem>().Play();
        }
        //hitInstance.transform.LookAt(playerCamera);
        greenIndicator.hitSuccess = true;
        AudioManager.instance.PlayGoodAnvilHit();
        GameManager.instance.AddFreeHit();
    }

        void GoodHitReaction(Collider other)
    {
        //Debug.Log("GoodHitReaction");
        try
        {

            string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
            if (handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.RTouch);
            }
            else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.LTouch);
            }
        }
        catch (System.NullReferenceException e) { }
        if (GameManager.instance.playParticles)
        {
            //GameObject hitInstance = GameObject.Instantiate(freeHitParticle, other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0), Quaternion.identity);
            GameObject hitInstance = ParticleManager.instance.GetGoodHitParticle();
            hitInstance.transform.position = other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0);
            hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            hitInstance.GetComponent<ParticleSystem>().Play();
        }
        //hitInstance.transform.LookAt(playerCamera);
        greenIndicator.HitSuccess();
        AudioManager.instance.PlayGoodAnvilHit();
    }

    void BadHitReaction(Collider other)
    {
        //Debug.Log("BadHitReaction");
        try
        {

            string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
            if (handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.RTouch);
            }
            else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.LTouch);
            }
        }
        catch (System.NullReferenceException e) { }
        //GameManager.instance.AddBadHit();
        AudioManager.instance.PlayBadAnvilHit();
        greenIndicator.badHit = true;
        if (GameManager.instance.playParticles)
        {
            //GameObject hitInstance = GameObject.Instantiate(freeHitParticle, other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0), Quaternion.identity);
            GameObject hitInstance = ParticleManager.instance.GetBadHitParticle();
            hitInstance.transform.position = other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0);
            hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            hitInstance.GetComponent<ParticleSystem>().Play();
        }
    }

    void EarlyHitReaction(Collider other)
    {
        //Debug.Log("Ear;yHitReaction");
        try
        {
            string handHoldingHammer = other.transform.parent.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ToString();
            if (handHoldingHammer == "AvatarGrabberRight (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.RTouch);
            }
            else if (handHoldingHammer == "AvatarGrabberLeft (OVRGrabber)")
            {
                ControllerManager.CM.StartVibration(0.5f, 0.5f, 0.1f, OVRInput.Controller.LTouch);
            }
        }
        catch (System.NullReferenceException e) { }
        //GameManager.instance.AddBadHit();
        greenIndicator.badHit = true;
        AudioManager.instance.PlayBadAnvilHit();
        if (GameManager.instance.playParticles)
        {
            //GameObject hitInstance = GameObject.Instantiate(freeHitParticle, other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0), Quaternion.identity);
            GameObject hitInstance = ParticleManager.instance.GetBadHitParticle();
            hitInstance.transform.position = other.ClosestPointOnBounds(transform.position) + new Vector3(0, particleHeightDiff.Value, 0);
            hitInstance.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            hitInstance.GetComponent<ParticleSystem>().Play();
        }
        yellowIndicator.RenderInert();
    }
}
