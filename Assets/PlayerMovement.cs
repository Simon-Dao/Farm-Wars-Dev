using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float sprintSpeed = 10f;
    public float walkSpeed = 5f;
    private float moveSpeed = 0;
    private float horizontalInput;
    private float verticalInput;
    private string currentState;
    // Start is called before the first frame update
    const string IDLE_LEFT = "Idle_Left";
    const string IDLE_RIGHT = "Idle_Right";
    const string RUN_LEFT = "Run_Left";
    const string RUN_RIGHT = "Run_Right";
    const string WALK_LEFT = "Walk_Left";
    const string WALK_RIGHT = "Walk_Right";
    bool isRunning = false;
    bool isFacingRight = true;
    void Start()
    {
        animator.Play(IDLE_RIGHT);
        horizontalInput = 0;
        verticalInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {
            isRunning = true;
        } else {
            isRunning = false;
        }

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        CheckDirection();
        if(verticalInput == 0 && horizontalInput == 0) {
            if(isFacingRight) {
                ChangeAnimationState(IDLE_RIGHT);
            } else {
                ChangeAnimationState(IDLE_LEFT);
            }
        } else if(horizontalInput > 0 && !isRunning) {
            if(isFacingRight) {
                ChangeAnimationState(WALK_RIGHT);
            } else {
                ChangeAnimationState(WALK_LEFT);
            }
        } else {
            if(isFacingRight) {
                ChangeAnimationState(RUN_RIGHT);
            } else {
                ChangeAnimationState(RUN_LEFT);
            }
        }


        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
        if(isRunning) {
            moveSpeed = sprintSpeed;
        } else {
            moveSpeed = walkSpeed;
        }
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void CheckDirection() {
        if(horizontalInput > 0) {
            isFacingRight = true;
        } else if(horizontalInput < 0) {
            isFacingRight = false;
        }
    }

    void ChangeAnimationState(string newState) {
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
