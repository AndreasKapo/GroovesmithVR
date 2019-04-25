using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlend : MonoBehaviour {

    public GameObject shape;
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    float blendOne = 0f;
    float blendTwo = 0f;
    float blendThree = 0f;
    float blendSpeed = 1f;
    bool blendOneFinished = false;

    void Awake()
    {
        skinnedMeshRenderer = shape.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = shape.GetComponent<SkinnedMeshRenderer>().sharedMesh;
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
        blendThree += blendAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);
        skinnedMeshRenderer.SetBlendShapeWeight(2, blendThree);
    }
}
