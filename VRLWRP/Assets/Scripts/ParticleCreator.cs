using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCreator : MonoBehaviour
{

    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HammerHead")
        {
            GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
