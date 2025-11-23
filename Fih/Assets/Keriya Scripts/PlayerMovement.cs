using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //speed of the player
    public float mouseSens = 100f; //mouse sensitivity 

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
        RotatePlayer(); // Camera rotation with mouse
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
}
