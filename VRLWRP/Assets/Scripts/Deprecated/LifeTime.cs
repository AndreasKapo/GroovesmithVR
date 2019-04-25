using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public float lifeTime;
    float timer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > lifeTime)
        {
            Destroy(gameObject);
        }
	}
}
