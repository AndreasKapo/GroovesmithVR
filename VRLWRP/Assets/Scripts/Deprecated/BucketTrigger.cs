using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketTrigger : MonoBehaviour {

    public GameObject hammerPrefab;
    public Transform hammerPosition;

    public float hammerCheckRadius;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "HammerHead")
        {
            Collider[] results = Physics.OverlapSphere(transform.position, hammerCheckRadius);
            int numHammerHead = 0;
            foreach(Collider result in results)
            {
                if(result.tag == "HammerHead")
                {
                    numHammerHead++;
                }
            }
            if(numHammerHead < 5)
            {
                GameObject hammerInstance = GameObject.Instantiate(hammerPrefab, hammerPosition.position, hammerPosition.rotation);
                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, hammerCheckRadius);
    }
}
