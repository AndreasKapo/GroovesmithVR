using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {


    public static ControllerManager CM;

    void Awake()
    {
        //Singleton pattern
        if (CM == null)
        {
            DontDestroyOnLoad(gameObject);
            CM = this;
        }
        else if (CM != this)
        {
            Destroy(gameObject);
        }
    }

    bool leftControllerVibrating;
    bool rightControllerVibrating;

    float leftControllerVibrationTime;
    float rightControllerVibrationTime;

    float leftControllerTimer;
    float rightControllerTimer;

    float leftControllerFrequency;
    float rightControllerFrequency;

    float leftControllerAmplitude;
    float rightControllerAmplitude;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (leftControllerVibrating)
        {
            OVRInput.SetControllerVibration(leftControllerFrequency, leftControllerAmplitude, OVRInput.Controller.LTouch);
            leftControllerTimer += Time.deltaTime;
            if(leftControllerTimer >= leftControllerVibrationTime)
            {
                EndVibration(OVRInput.Controller.LTouch);
            }
        }

        if (rightControllerVibrating)
        {
            OVRInput.SetControllerVibration(rightControllerFrequency, rightControllerAmplitude, OVRInput.Controller.RTouch);
            rightControllerTimer += Time.deltaTime;
            if (rightControllerTimer >= rightControllerVibrationTime)
            {
                EndVibration(OVRInput.Controller.RTouch);
            }
        }
    }

    public void StartVibration(float frequency, float amplitude, float duration, OVRInput.Controller controller)
    {


        if (controller == OVRInput.Controller.LTouch)
        {
            leftControllerVibrating = true;
            leftControllerFrequency = frequency;
            leftControllerAmplitude = amplitude;
            leftControllerVibrationTime = duration;
            leftControllerTimer = 0f;
        }

        if (controller == OVRInput.Controller.RTouch)
        {
            rightControllerVibrating = true;
            rightControllerFrequency = frequency;
            rightControllerAmplitude = amplitude;
            rightControllerVibrationTime = duration;
            rightControllerTimer = 0f;
        }
    }

    public void EndVibration(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(0, 0, controller);
        if (controller == OVRInput.Controller.LTouch)
        {
            leftControllerVibrating = false;
            leftControllerFrequency = 0f;
            leftControllerAmplitude = 0f;
            leftControllerVibrationTime = 0f;
            leftControllerTimer = 0f;
        }

        if (controller == OVRInput.Controller.RTouch)
        {
            rightControllerVibrating = false;
            rightControllerFrequency = 0f;
            rightControllerAmplitude = 0f;
            rightControllerVibrationTime = 0f;
            rightControllerTimer = 0f;
        }
    }
}
