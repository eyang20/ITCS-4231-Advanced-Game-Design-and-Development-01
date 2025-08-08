using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //If the script <PlayerStats>() is NOT null...
        if (PlayerStats.Instance != null)
        {
            //...set the vaule of speed to be equal to vaule baseSpeed in the <PlayerStats>() script.
            speed = PlayerStats.Instance.speed;
        }

        else
        {
            //...else set the vaule of speed to twelve.
            speed = 12f;
        }

        //Creates an invisable sphere at groundCheck transform's position and set isGrounded to true if groundMask colliding with the sphere.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If isGrounded is true and velocity.y is less than 0...
        if (isGrounded && velocity.y < 0)
        {
            //...set velocity.y to -2, making sure the player is on the ground.
            velocity.y = -2f;
        }

        //Defines x and y as inputs. X is Horizontal and Z is Vertical.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Takes the direction that the player is facing and moves the player according to the x or z axis.
        Vector3 move = transform.right * x + transform.forward * z;

        //Moves the player according to move by speed.
        controller.Move(move * speed * Time.deltaTime);

        //If Jump is pressed and isGrounded is true...
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //...velocity.y is equal to the square root of jumpHeight * -2f * gravity.
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Adds the value of gravity to velocity.
        velocity.y += gravity * Time.deltaTime;

        //The controller moves according the vaule of velocity.
        controller.Move(velocity * Time.deltaTime);

        //If the input button "Horizontal" is pressed or held down and isGrounded or  the input button "Vertical" is pressed or held down and isGrounded...
        if (Input.GetButton("Horizontal") && isGrounded || Input.GetButton("Vertical") && isGrounded)
        {
            //...call PlayAudio and get the component <MovementSound>().
            PlayAudio(GetComponent<MovementSound>());
        }
    }

    void PlayAudio(MovementSound movementSound)
    {
        //Calls PlayClip() from the script <MovementSound>().
        movementSound.PlayClip();
    }
}
