using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController charController;
    public float movement_speed =3f;
    public float gravity = 10f;
    public float rotation_Speed = 0.15f;
    public float rotateDegreesPerSeceond = 180f;
    public float fall_speed = 60f;

    void Awake()
    {
        charController = GetComponent < CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Vector3 move = Vector3.zero;
        move.y -= gravity * Time.deltaTime;
        charController.Move(move * fall_speed * Time.deltaTime);
    }
     void Move()
    {
        if(Input.GetAxis(Axis.VERTICAL_AXIS)>0)
        {
            Vector3 MoveDirection= this.transform.forward;
          //  MoveDirection.y -= gravity * Time.deltaTime;
            charController.Move(MoveDirection * movement_speed * Time.deltaTime);
        }
        if (Input.GetAxis(Axis.VERTICAL_AXIS) < 0)
        {
            Vector3 MoveDirection = -this.transform.forward;
          //  MoveDirection.y -= gravity * Time.deltaTime;
            charController.Move(MoveDirection * movement_speed * Time.deltaTime);
        }

        Rotate();
    }
    void Rotate()
    {
        Vector3 rotation_Direction = Vector3.zero;

        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.right);
        }

        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.left);
        }
        if (rotation_Direction != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_Direction), rotateDegreesPerSeceond * Time.deltaTime);
        }
    }

}
