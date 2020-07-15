using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToFollow = null;

    [SerializeField]
    private float delaySpeed = 0.0f;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private void FixedUpdate()
    {
        if (objectToFollow)
            this.transform.position = Vector3.Lerp(this.transform.position, objectToFollow.transform.position + offset, delaySpeed * Time.deltaTime);
    }
}
