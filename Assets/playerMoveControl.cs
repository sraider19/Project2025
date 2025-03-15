using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMoveControl : MonoBehaviour
{
    private Vector3 playerVelocity;
    public float gravityValue = -9.81f;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    public float jumpHeight = 2f;

    public Animator anim;

    private CharacterController characterController;
    private Vector3 velocity;
    public bool isGrounded;
    private float inputX;
    private bool canJump;
    private bool jumpBool = false;
    private bool animGrounded = true;
    public GameObject camFollowScript;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        anim.SetFloat("InputX", inputX);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotateSpeed, 0);
        }
            if (Input.GetKey(KeyCode.UpArrow))
            {
            camFollowScript.GetComponent<SimpleCameraFollow>().enabled = true;
        }
            else
            {
            camFollowScript.GetComponent<SimpleCameraFollow>().enabled = false;
        }

        // Check for ground contact
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);

        // Get input
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(-verticalInput, 0, 0).normalized;

        // Apply gravity
        // Move the character
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * Input.GetAxis("Vertical");
        characterController.SimpleMove(forward * curSpeed);
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            {
                    jumpBool = true;
                    anim.SetBool("animJumpB", true);
                    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                }
                else if (!Input.GetKeyDown(KeyCode.Space) && !characterController.isGrounded)
            {
                    anim.SetBool("animJumpB", false);
                }
            }
        // Gravity
        if (characterController.isGrounded)
        {
            anim.SetBool("animGrounded", true);
        }
        if (velocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        if (!characterController.isGrounded)
        {
            anim.SetBool("animGrounded", false);
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);

      playerVelocity.y += gravityValue * Time.deltaTime;
      characterController.Move(playerVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpZone"))
        {
            canJump = true;
            Debug.Log("Entered Jump Zone. Jump enabled.");
        }
    }

    // When the player exits a trigger collider with the tag "JumpZone", disable jumping.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpZone"))
        {
            canJump = false;
            Debug.Log("Exited Jump Zone. Jump disabled.");
        }
    }
}