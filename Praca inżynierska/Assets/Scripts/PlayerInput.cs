//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerInput : MonoBehaviour
//{
//    #region Variables       

//    [Header("Controller Input")]
//    public string horizontalInput = "Horizontal";
//    public string verticallInput = "Vertical";
//    public KeyCode jumpInput = KeyCode.Space;
//    public KeyCode strafeInput = KeyCode.Tab;
//    public KeyCode sprintInput = KeyCode.LeftShift;

//    [Header("Camera Input")]
//    public string rotateCameraXInput = "Mouse X";
//    public string rotateCameraYInput = "Mouse Y";

//    [HideInInspector] public vThirdPersonController cc;
//    [HideInInspector] public vThirdPersonCamera tpCamera;
//    [HideInInspector] public Camera cameraMain;

//    #endregion

//    protected virtual void Start()
//    {
//        InitilizeController();
//        InitializeTpCamera();
//    }

//    protected virtual void FixedUpdate()
//    {
//        cc.UpdateMotor();               // updates the ThirdPersonMotor methods
//        cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
//        cc.ControlRotationType();       // handle the controller rotation type
//    }

//    protected virtual void Update()
//    {
//        InputHandle();                  // update the input methods
//        cc.UpdateAnimator();            // updates the Animator Parameters
//    }

//    public virtual void OnAnimatorMove()
//    {
//        cc.ControlAnimatorRootMotion(); // handle root motion animations 
//    }
//}
