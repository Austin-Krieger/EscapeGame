using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhysics : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 1f;

    private Rigidbody rbody;
    private int JumpCount;

    //Raycasting
    private RaycastHit vision; // Used for detecting Raycast collision
    public float rayLength = 0.2f; // Used for assigning a length to the raycast

    private void Start()
    {
        //Jump Count
        JumpCount = 0;
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red, 0.5f);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rbody.AddForce(movement * speed);

        //This statement is called when the Raycast is hitting a collider in the scene
        if (Physics.Raycast(transform.position - new Vector3(0, -0.2f, 0), Vector3.down, out vision, rayLength))
        {
            JumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && JumpCount < 1)
        {
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
            JumpCount++;
        }
    }
}
