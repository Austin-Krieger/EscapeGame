using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Text txtGameObjectInSight;
    
    public float speed = 2f;
    public float runSpeed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float playerHealth = 100f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float pushPower = 4f;

    private float tempSpeed = 2f;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), Vector3.forward * 2f, Color.red, 0.5f);
        //Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), transform.forward, Color.red, 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.forward
        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f);
            txtGameObjectInSight.text = hit.transform.gameObject.name.ToString();
            //Debug.Log(hit.transform.gameObject.name);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tempSpeed = runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            tempSpeed = speed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Time.deltaTime makes this framerate independent
        controller.Move(move * tempSpeed * Time.deltaTime);

        // Jump is by default mapped to the space bar
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Add gravity, without a ground check the velocity will constantly increase.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
