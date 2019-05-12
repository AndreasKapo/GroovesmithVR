using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<OVRGrabbable>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
