using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInTime : MonoBehaviour
{
    [SerializeField]
    private Vector3 value;

    private void FixedUpdate()
    {
        this.transform.Rotate(value);
    }
}
