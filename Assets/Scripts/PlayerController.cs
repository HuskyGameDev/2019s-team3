﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody item;
    public float baseMoveSpeed;
    private float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float gravityScale = 5f;
    public float knockBackForce;
    public float knockBackTime;
    public float knockBackCounter;
    private Vector3 moveDirection;
    private bool hasDoubleJumped = false;
    private bool isCarrying = false;
    private long lastFootStepSoundTime = -1;

    public int FootStepFrequency = 100;

    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            // set movement
            //NOTE: Use GetAxisRaw to remove "sliding" after movement
            float prevY = moveDirection.y; // store y value temp
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) +
                    (transform.right * Input.GetAxis("Horizontal"));

            // let player run
            bool running = Input.GetButton("Run");
            if (running)
            {
                moveSpeed = 2 * baseMoveSpeed;
            }
            else
            {
                moveSpeed = baseMoveSpeed;
            }

            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = prevY;

            // jump logic
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                hasDoubleJumped = false;
            }
            if (Input.GetButtonDown("Jump") && (controller.isGrounded || !hasDoubleJumped))
            {
                if (!controller.isGrounded)
                {
                    hasDoubleJumped = true;
                }
                moveDirection.y = jumpForce;
                AkSoundEngine.PostEvent("Jump", this.gameObject);
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        // apply gravity
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        // apply movement
        controller.Move(moveDirection * Time.deltaTime);

        // move player in different directions based on camera look directions
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        FootStepSoundTick();
    }

    private void FootStepSoundTick()
    {
        // make running step sounds 
        float calculatedStepFreq = FootStepFrequency / (Math.Abs(moveDirection.x) + Math.Abs(moveDirection.z) / 2);
        if ((Math.Abs(moveDirection.x) > 0.1 || Math.Abs(moveDirection.z) > 0.1)
            && controller.isGrounded
            && Environment.TickCount - lastFootStepSoundTime > calculatedStepFreq)
        {
            lastFootStepSoundTime = Environment.TickCount;
            AkSoundEngine.PostEvent("Footstep", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Pick Up"))
        //{
        //    other.gameObject.SetActive(false);
        //}
    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
    }
}
