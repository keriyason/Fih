using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //speed of the player
    public float mouseSens = 100f; //mouse sensitivity 

    public float jumpForce = 5f; //jump amount
    public LayerMask mapMask; //ground layer
    public float groundCheckDistance = 1.1f; // checks if the player is on the ground

    private Rigidbody rb;
   

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the center of the screen
        rb = GetComponent<Rigidbody>(); 
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        MovePlayer(); //Movement for player

      
    }

    private void Update()
    {
        RotatePlayer(); // camera rotation with mouse
        HandleJump();
    }
    void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal"); // moves left and right with A/D
        float z = Input.GetAxisRaw("Vertical"); // moves up and down with W/S

        Vector3 move = transform.right * x + transform.forward * z;

       rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void RotatePlayer()
    {
       float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
    }
    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }    
    }
   
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, mapMask);

    }    
        
}

