﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Invector.vCharacterController
{


    public class vThirdPersonController : vThirdPersonAnimator
    {
        // private Animator animator;
        // private string currentAnimation = "";
        //public List<Collider> RagdollParts = new List<Collider>();
        public Rigidbody rb;

        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }
        // private void Start()
        // {
        //     animator = GetComponent<Animator>();
        //     //ChangeAnimation("Rac_Attack Paws");
        // }
        // private void Update()
        // {
        //     CheckAnimation();
        // }

        // private void ChangeAnimation(string animation, float crossfade = 0.2f)

        // {
        //     if(currentAnimation != animation)
        //     {
        //         currentAnimation = animation;
        //         animator.CrossFade(animation, crossfade);
        //     }
        
        // } 
        // private void CheckAnimation()
        // {
        //     if(moveDirection.y == 1)
        //     {
        //         ChangeAnimation("Rac_Trot Forward");
        //     }
        //     else if(moveDirection.y == -1)
        //     {
        //         ChangeAnimation("Rac_WalkBack Forward");
        //     }
        //     else if(moveDirection.x == 1)
        //     {
        //         ChangeAnimation("Rac_Trot Right");
        //     }
        //     else if(moveDirection.x == -1)
        //     {
        //         ChangeAnimation("Rac_Trot Left");
        //     }
        //     else
        //     {
        //         ChangeAnimation("Rac_Idle01");
        //     }
        // }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "car" )
            {
                Debug.Log("Collision with car detected!");
                rb.AddForce(500f * Vector3.up, ForceMode.Impulse);

            }
        }

        // private void Awake()
        // {
        //     SetRagdollParts();
        // }


        // private void SetRagdollParts()
        // {
        //     Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        //     foreach(Collider c in colliders)
        //     {
        //         if(c.gameObject != this.gameObject) 
        //         {
        //             c.isTrigger = true;
        //             RagdollParts.Add(c);
        //         }
        //     }
        // }
    }
}