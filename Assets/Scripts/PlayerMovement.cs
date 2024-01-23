using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : Agent
{
    public PlayerDataExample myPlayerData = null;
    public static PlayerDataExample GlobalPlayerData = null;

    public CharacterController characterController;

    public float speed = 12.0f;
    public float gravity = -9.81f;

    public Transform Groundcheck = null;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 velocity = Vector3.zero;
    bool isGrounded = false;

    public void Awake()
    {
        GlobalPlayerData = myPlayerData;
    }
    public void Update()
    {
        isGrounded = Physics.CheckSphere(Groundcheck.position, GroundDistance, GroundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
       
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }
    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }


}