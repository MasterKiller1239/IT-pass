using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ModelCharacterController : MonoBehaviour
{
    private Vector3 move = Vector3.zero;

    protected float moving;

    private bool canDoAction = true;
    private bool canRotate = true;
    private bool canDash = true;

    protected bool CanDoAction { get => canDoAction; }
    protected bool CanRotate { get => canRotate; }
    public bool CanDash { get => canDash; }

    [SerializeField]
    protected float movementSpeed = 1.0f;

    protected Animator modelAnimator = null;
    protected CharacterController characterController = null;

    protected virtual void Awake()
    {
        modelAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void Move(float moving)
    {
        if(canDoAction)
        {
            move = Vector3.zero;

            if (moving > 0)
            {
                //if (modelAnimator.GetBool("isWalking") == false)
                //    modelAnimator.SetTrigger("walkingTrigger");

                modelAnimator.SetBool("isRunning", true);

                //modelAnimator.SetBool("isGrounded", characterController.isGrounded);

                //modelAnimator.SetFloat("velocity", moving, 1f, Time.deltaTime * 10f);
                move = (this.transform.forward * moving) * movementSpeed * Time.deltaTime;
            }
            else
            {
                modelAnimator.SetBool("isRunning", false);
                //modelAnimator.SetBool("isWalking", false);
                //modelAnimator.SetFloat("velocity", 0.0f, 1f, Time.deltaTime * 10f);
            }

            /*if (isGoingUp)
            {
                move.y += jumpSpeed * Time.deltaTime;
            }
            else if (gravityActive)
                move.y -= gravity * Time.deltaTime;*/

            characterController.Move(move);
        }
    }

    protected void Attack()
    {
        if (canDoAction)
        {
          //  modelAnimator.SetTrigger("attackTrigger");
        }
    }

    protected void Kick()
    {
        if (canDoAction)
        {
            //modelAnimator.SetTrigger("kickTrigger");
        }
    }

    protected void Dash()
    {
        if (canDash)
        {
           // modelAnimator.SetTrigger("dashTrigger");
        }
    }

    #region AnimEvents

    public void LockAction()
    {
        canDoAction = false;
    }

    public void UnlockAction()
    {
        canDoAction = true;
    }

    public void LockRotation()
    { 
        canRotate = false;
    }

    public void UnlockRotation()
    {
        canRotate = true;
    }

    public void LockDash()
    {
        canDash = false;
    }

    public void UnlockDash()
    {
        canDash = true;
    }

    public void DisableRootMotion()
    {
        modelAnimator.applyRootMotion = false;
    }

    public void EnableRootMotion()
    {
        modelAnimator.applyRootMotion = true;
    }

    //public void EnableBonusAS()
    //{
    //    modelAnimator.speed += GetComponent<PlayerHealth>().bonusAttackSpeed / 100.0f;
    //}

    //public void DisableBonusAS()
    //{
    //    //modelAnimator.speed -= GetComponent<PlayerHealth>().bonusAttackSpeed / 100.0f;
    //    modelAnimator.speed = 1.0f;
    //}

    #endregion
}
