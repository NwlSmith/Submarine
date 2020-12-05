using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Alerts other code if the player is seen or lost.
 */

public class BossSight : MonoBehaviour
{
    public bool seesPlayer = false;
    [SerializeField] private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void FixedUpdate()
    {
        Ray bossToPlayer = new Ray(playerManager.transform.position, (transform.position - playerManager.transform.position).normalized);
        if(Physics.Raycast(bossToPlayer, out RaycastHit hit, 50f))
        {
            // if the boss has uninterrupted line of sight to player
            if (hit.collider.CompareTag("Player"))
            {
                // if the boss couldn't previously see the player
                if (!seesPlayer)
                {
                    // trigger boss seeing player
                    CaughtPlayer();
                }
            }
            else if (seesPlayer)
            {
                LostPlayer();
            }
        }
    }

    /*
     * Triggers when the boss spots the player after not seeing them.
     */
    private void CaughtPlayer()
    {
        seesPlayer = true;
        Debug.Log("Boss has seen player.");
    }

    /*
     * Triggers when the boss loses the player after seeing them.
     */
    private void LostPlayer()
    {
        seesPlayer = false;
        Debug.Log("Boss has lost player.");
    }
}
