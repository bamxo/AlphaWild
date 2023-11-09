using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 12f;
    public float sprintSpeed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Interactable focus;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // move physics & animations
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftControl) ? sprintSpeed : walkSpeed; // Check if Left Control is held down

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        //jump physics & animations

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // left click
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }

        // right click
        if (Input.GetMouseButtonDown(1))
        {
            
        }

        // keybind "e" for use
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray;
            RaycastHit hit;

            // Get the first-person camera
            Camera fpsCamera = GetComponentInChildren<Camera>();

            // Calculate the ray using the camera's viewport
            ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    interactable.Interact();
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void RemoveFocus()
    {
        focus = null;
    }

    // left click
    void HandleLeftClick()
    {
        Ray ray;
        RaycastHit hit;

        // Get the first-person camera
        Camera fpsCamera = GetComponentInChildren<Camera>();

        // Calculate the ray using the camera's viewport
        ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable is Enemy)
                {
                    // Interact only if the object is an enemy
                    SetFocus(interactable);
                    interactable.Interact();
                }
            }
        }
    }
}