using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 direction;
    public float forwardSpeed;
    public float originalForwardSpeed;

    public float jumpForce;

    public float dashTry;
    public float Gravity = -20;
    public int flag = 0;

    public float dashSpeed;

    public float dashTime;

    public int isSprinting = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalForwardSpeed = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        direction.y += Gravity * Time.deltaTime;


    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    public void JumpButton()
    {
        if(controller.isGrounded)
        {
            //flag = 1;
            Jump();   
        }
    }

    private void Jump()
    {
        //if(isSprinting == 0)
        //{
        direction.y = jumpForce;
        //}
        //else if (isSprinting == 1)
        //{
            //direction.y = jumpForce * 2;
        //}
    }

    public void Sprint()
    {
        if(controller.isGrounded)
        {
            forwardSpeed = forwardSpeed * 2;
            isSprinting = 1;
        }
        
    }
    public void SprintStop()
    {
        if(controller.isGrounded)
        {
            forwardSpeed = originalForwardSpeed;
        }
        
    }

    public void DashButton()
    {
        StartCoroutine(Dash());

    }
    IEnumerator Dash()
    {
        float startTime = Time.time;

        if(controller.isGrounded)
        {
            while(Time.time < startTime + dashTime)
            {
                controller.Move(direction * dashSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
