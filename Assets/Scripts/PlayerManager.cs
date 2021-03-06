﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Sprite[] dashboardDamageSprites = null;

    public CameraFX camFX;
    private SubmarineMovement movement;
    private PlayerLightManager playerLightManager;
    private PlayerSoundManager playerSoundManager;
    private SpriteRenderer dashboardDamageSprite = null;

    private void Awake()
    {
        // Ensure that there is only one instance of the PlayerManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        movement = GetComponent<SubmarineMovement>();
        camFX = GetComponentInChildren<CameraFX>();

        playerLightManager = GetComponentInChildren<PlayerLightManager>();
        playerSoundManager = GetComponentInChildren<PlayerSoundManager>();
        dashboardDamageSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage = 1)
    {
        if (health <= 0)
            return;
        health -= damage;
        // change dashboard sprite


        if (health <= 0)
            DeathSequence();

        if (dashboardDamageSprites.Length > health)
        {
            dashboardDamageSprite.sprite = dashboardDamageSprites[health];
        }
    }

    // handle damage from fast collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Harpoon>())
        {
            return;
        }

        if (collision.relativeVelocity.magnitude > 5f)
        {
            camFX.HighShake();
            playerSoundManager.HeavyImpact();
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
            playerSoundManager.MedImpact();
        }
        else if (collision.relativeVelocity.magnitude > 1f)
        {
            // Very minor hit
            camFX.LowShake();
            playerSoundManager.LowImpact();
        }
    }

    public void Moving()
    {
        playerSoundManager.Moving();
    }

    public void MovingFast()
    {
        playerSoundManager.MovingFast();
    }

    public void NotMoving()
    {
        playerSoundManager.NotMoving();
    }


    public void BossSawPlayer()
    {
        playerLightManager.BossSawPlayer();
        camFX.ambientRumbling = true;
        playerSoundManager.ActivateAmbientBossRumble();
    }

    public void BossLostPlayer()
    {
        playerLightManager.BossLostPlayer();
        camFX.ambientRumbling = false;
        playerSoundManager.DeactivateAmbientBossRumble();
    }

    public void BossSeenPlayerTooLong(Vector3 bossPos)
    {
        StartCoroutine(BossSeenPlayerTooLongEnum(bossPos));
    }

    private IEnumerator BossSeenPlayerTooLongEnum(Vector3 bossPos)
    {
        // big red light, play sound
        playerLightManager.BossSeenPlayerTooLong();
        playerSoundManager.BossCaughtPlayer();
        movement.canMove = false;

        // lerp to look at boss
        float elapsedTime = 0f;
        float duration = 1f;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation((bossPos - transform.position).normalized, Vector3.up);
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / duration);
            yield return null;
        }

        transform.rotation = targetRot;

        // zoom in
        elapsedTime = 0f;
        duration = .3f;
        Camera cam = camFX.GetComponent<Camera>();
        float startFOV = cam.fieldOfView;
        float targetFOV = 30f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cam.fieldOfView = Mathf.SmoothStep(startFOV, targetFOV, elapsedTime / duration);
            yield return null;
        }
        cam.fieldOfView = targetFOV;

        yield return new WaitForSeconds(.5f);

        TakeDamage(1);

        yield return new WaitForSeconds(.5f);
        // take damage, shake, heavy impact
        camFX.HighShake();
        playerSoundManager.HeavyImpact();
        playerSoundManager.BossAttackPlayer();
        cam.fieldOfView = startFOV;
        movement.BossPush(bossPos);
        movement.canMove = true;
    }

    public void CaughtInExplosion(Vector3 explosionPos)
    {
        TakeDamage(1);
        movement.CaughtInExplosion(explosionPos);
    }

    private void DeathSequence()
    {
        StartCoroutine(DeathSequenceEnum());
    }

    private IEnumerator DeathSequenceEnum()
    {
        // lose control
        movement.canMove = false;
        // start playing sounds
        playerSoundManager.DeathSounds();
        playerLightManager.DeathSequence();
        movement.DeathSequence();
        // start rotating on z and on x, move down slowly, fade to black

        yield return new WaitForSeconds(3f);
        GameManager.instance.DeathSequence();
    }
}
