using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Prevents the comptuer mouse from going off screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //Prevents the player from looking behind themselves when looking up or down.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotates the Camera position allowing it rotate on the X axis and Z axis.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotates the Camera position allowing it rotate on the Y axis.
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
