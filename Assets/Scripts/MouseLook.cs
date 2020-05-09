using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float MaxLookDown = 45f;
    public float MaxLookUp = 45f;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.smoothDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.smoothDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -MaxLookUp, MaxLookDown);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Access player body and specify what axis we want to rotate around
        // Vector3.up is our Y-axis
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
