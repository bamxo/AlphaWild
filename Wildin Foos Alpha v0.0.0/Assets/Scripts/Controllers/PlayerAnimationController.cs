using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isLeftWalkingHash;
    int isRightWalkingHash;
    int isBackWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isAttackingHash;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isLeftWalkingHash = Animator.StringToHash("isLeftWalking");
        isRightWalkingHash = Animator.StringToHash("isRightWalking");
        isBackWalkingHash = Animator.StringToHash("isBackWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isLeftWalking = animator.GetBool(isLeftWalkingHash);
        bool isRightWalking = animator.GetBool(isRightWalkingHash);
        bool isBackWalking = animator.GetBool(isBackWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isAttacking = animator.GetBool(isAttackingHash);
        bool grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool backwardPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey(KeyCode.LeftControl);
        bool jumpPressed = Input.GetKey(KeyCode.Space);
        bool leftClickPressed = Input.GetMouseButton(0);

        // Walking

        // Forwards
        // when W is pressed, character will execute walking animation
        if (!isWalking && forwardPressed)
        {
            // set isWalking boolean to true
            animator.SetBool(isWalkingHash, true);
        }
        // when W is not pressed, character will revert to previous animation
        if (isWalking && !forwardPressed)
        {
            // set isWalking boolean to false
            animator.SetBool(isWalkingHash, false);
        }

        // Left
        // when A is pressed, character will execute left animation
        if (!isLeftWalking && leftPressed)
        {
            // set isLeftWalking boolean to true
            animator.SetBool(isLeftWalkingHash, true);
        }
        // when A is not pressed, character will revert to previous animation
        if (isLeftWalking && !leftPressed)
        {
            // set isLeftWalking boolean to false
            animator.SetBool(isLeftWalkingHash, false);
        }

        // Right
        // when D is pressed, character will execute right animation
        if (!isRightWalking && rightPressed)
        {
            // set isRightWalking boolean to true
            animator.SetBool(isRightWalkingHash, true);
        }
        // when D is not pressed, character will revert to previous animation
        if (isRightWalking && !rightPressed)
        {
            // set isRightWalking boolean to false
            animator.SetBool(isRightWalkingHash, false);
        }

        // Backwards
        // when S is pressed, character will execute backwards animation
        if (!isBackWalking && backwardPressed)
        {
            // set isWalking boolean to true
            animator.SetBool(isBackWalkingHash, true);
        }
        // when S is not pressed, character will revert to previous animation
        if (isBackWalking && !backwardPressed)
        {
            // set isBackWalking boolean to false
            animator.SetBool(isBackWalkingHash, false);
        }

        // Sprinting

        // if player is walking and not running and presses LCTRL, character will execute sprinting animation
        if (!isRunning && (forwardPressed && runPressed))
        {
            // set isRunning boolean to true
            animator.SetBool(isRunningHash, true);
        }
        // if player is running LCTRL is not pressed, character will revert to previous animation
        if (isRunning && (!forwardPressed || !runPressed))
        {
            // set isRunning boolean to false
            animator.SetBool(isRunningHash, false);
        }

        // Jumping

        // if player presses space, character will executre full jump animation
        if (!isJumping && jumpPressed)
        {
            // set isJumping boolean to true
            animator.SetBool(isJumpingHash, true);
        }   
        if (grounded)
        {
            // set isJumping boolean to false
            animator.SetBool(isJumpingHash, false);
        }

        // Attacking
        if (!isAttacking && leftClickPressed)
        {
            // set isAttacking boolean to true
            animator.SetBool(isAttackingHash, true);
        }
        if (isAttacking && !leftClickPressed)
        {
            //set isAttacking boolean to false
            animator.SetBool(isAttackingHash, false);
        }
    }
}
