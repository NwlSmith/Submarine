﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Primary player movement manager. Most action will occur in Update and FixedUpdate.
 */

public class SubmarineMovement : MonoBehaviour
{
    public bool canMove = true;

    // Movement parameters
    [SerializeField] private float forwardMovePower = 10f;
    [SerializeField] private float backwardMoveMultiplier = .5f;
    [SerializeField] private float horizontalMovePower = 5f;
    [SerializeField] private float sprintMultiplier = 2f;

    // Rotation parameters
    [SerializeField] private float vertMouseSensitivity = 100f;
    [SerializeField] private float horiMouseSensitivity = 100f;
    [SerializeField] private float mouseMovementSmoothing = 10f;

    private Quaternion intendedRot = Quaternion.identity;

    private Rigidbody rb;

    private float moveZ; // Forward and backward movement
    private float moveX; // Horizontal movement
    private float rotH; // Horizontal rotation
    private float rotV; // Vertical rotation
    private float shift; // Sprint?

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        rotH = Input.GetAxis("Mouse X"); // moving the mouse left/right should rotate player around y axis
        rotV = Input.GetAxis("Mouse Y"); // moving the mouse up/down should rotate player around x axis
        shift = Input.GetAxis("Sprint");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        /*
        if (transform.eulerAngles.x > 180f)
        {
            Debug.Log("Rot too high");
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);
        }
        else if (transform.eulerAngles.x < 0f)
        {
            Debug.Log("Rot too low");
            transform.eulerAngles = new Vector3(-90f, transform.eulerAngles.y, 0f);
        }*/

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Handle spacial movement
            Vector3 forwardForce;
            forwardForce = Vector3.forward * forwardMovePower * moveZ;
            if (moveZ > 0 && shift > 0)
            {
                forwardForce *= sprintMultiplier;
            }
            else if (moveZ < 0)
            {
                forwardForce *= backwardMoveMultiplier;
            }

            Vector3 horizontalForce;
            horizontalForce = Vector3.right * horizontalMovePower * moveX;
            if (moveZ > 0 && shift > 0)
            {
                forwardForce *= sprintMultiplier;
            }
            else if (moveZ < 0)
            {
                forwardForce *= backwardMoveMultiplier;
            }

            Vector3 totalForce = forwardForce + horizontalForce;

            rb.AddRelativeForce(totalForce, ForceMode.Force);

            // Handle rotational movement

            // Retrieve mouse input.
            float intendedRotChangeH = rotH * horiMouseSensitivity;
            float intendedRotChangeV = rotV * vertMouseSensitivity;

            

            float intendedRotH = transform.localEulerAngles.y;
            float intendedRotV = transform.localEulerAngles.x;

            intendedRotH += intendedRotChangeH;
            intendedRotV -= intendedRotChangeV;

            intendedRotV = Mathf.Clamp(intendedRotV, -90, 90);

            intendedRot = Quaternion.Euler(new Vector3(intendedRotV, intendedRotH, 0f));

            Quaternion finalRot = Quaternion.Lerp(transform.localRotation, intendedRot, Time.fixedDeltaTime * mouseMovementSmoothing);

            //rb.MoveRotation(finalRot);
            
            Vector3 change = new Vector3(-intendedRotChangeV, intendedRotChangeH, 0f);
            rb.AddRelativeTorque(change, ForceMode.Force);



            //transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -90, 90), transform.localEulerAngles.y, 0f);
        }
    }
}
