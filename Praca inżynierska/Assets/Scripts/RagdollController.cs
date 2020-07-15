using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public List<GameObject> ragdollJoints;

    private void Awake()
    {
        foreach(GameObject go in ragdollJoints)
        {
            go.GetComponent<Rigidbody>().detectCollisions = false;
        }
        this.enabled = false;
    }

    private void OnEnable()
    {
        foreach(GameObject go in ragdollJoints)
        {
            CapsuleCollider cap = go.GetComponent<CapsuleCollider>();
            BoxCollider box = go.GetComponent<BoxCollider>();
            SphereCollider cir = go.GetComponent<SphereCollider>();
            if (cap != null)
            {
                cap.isTrigger = false;
            }
            else if(box != null)
            {
                box.isTrigger = false;
            }
            else
            {
                cir.isTrigger = false;
            }
            go.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }
}
