using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{

    public ParticleSystem[] particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnParticles()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }

    public void TurnOffParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
}

