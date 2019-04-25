using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

    public Material mat;
	// Use this for initialization
	void Start () {
        mat.SetVector("_RipplePoint", new Vector2(0.25f, 0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
