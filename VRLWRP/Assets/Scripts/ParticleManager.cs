using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance = null;

    public GameObject[] goodHitParticles;
    int goodHitParticlesIndex;

    public GameObject[] badHitParticles;
    int badHitParticlesIndex;

    public GameObject[] freeHitParticles;
    int freeHitParticlesIndex;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


    public GameObject GetGoodHitParticle()
    {
        goodHitParticlesIndex++;
        if (goodHitParticlesIndex >= goodHitParticles.Length)
        {
            goodHitParticlesIndex = 0;
        }
        return goodHitParticles[goodHitParticlesIndex];
    }

    public GameObject GetBadHitParticle()
    {
        badHitParticlesIndex++;
        if (badHitParticlesIndex >= badHitParticles.Length)
        {
            badHitParticlesIndex = 0;
        }
        return badHitParticles[badHitParticlesIndex];
    }

    public GameObject GetFreeHitParticle()
    {
        freeHitParticlesIndex++;
        if (freeHitParticlesIndex >= freeHitParticles.Length)
        {
            freeHitParticlesIndex = 0;
        }
        return freeHitParticles[freeHitParticlesIndex];
    }
}
