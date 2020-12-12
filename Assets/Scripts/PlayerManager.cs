﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Main manager for player. Contains references to movement.
 */

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance = null;

    public int health = 5;

    private float lastCollisionTime = 0f;
    [SerializeField] private float collisionCooldown = 2f;

    private SubmarineMovement movement;
    public CameraFX camFX;

    private void Awake()
    {
        // Ensure that there is only one instance of the PlayerManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        movement = GetComponent<SubmarineMovement>();
        camFX = GetComponentInChildren<CameraFX>();
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
    }

    // handle damage from fast collisions
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 5f)
        {
            camFX.HighShake();
            // Major hit
            if (lastCollisionTime + collisionCooldown < Time.time)
            {
                lastCollisionTime = Time.time;
                TakeDamage(1);
                // damage animation
                // collision sound
            }
        }
        else if (collision.relativeVelocity.magnitude > 3f)
        {
            // Minor hit
            camFX.MedShake();
        }
        else if (collision.relativeVelocity.magnitude > 1f)
        {
            // Very minor hit
            camFX.LowShake();
        }
    }

    public void BossSeenTooLong()
    {
        TakeDamage(1);
    }

}
