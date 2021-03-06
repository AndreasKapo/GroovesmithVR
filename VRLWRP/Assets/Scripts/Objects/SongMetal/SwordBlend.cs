﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlend : MonoBehaviour {
    
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    public float blendOne = 0f;
    public float blendTwo = 0f;
    public float blendThree = 0f;
    float blendSpeed = 1f;
    bool blendOneFinished = false;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    void Update()
    {
        /*
        if (blendShapeCount > 2)
        {

            if (blendOne < 100f)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
                blendOne += blendSpeed;
            }
            else
            {
                blendOneFinished = true;
            }

            if (blendOneFinished == true && blendTwo < 100f)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);
                blendTwo += blendSpeed;
            }

        }
        */
    }

    public void GoodHit(float blendAmount)
    {
        blendOne += blendAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
    }

    public void BadHit(float blendAmount)
    {
        blendTwo += blendAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);
    }

    public void MissHit(float blendAmount)
    {
        blendThree += blendAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(2, blendThree);
    }
}
