using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightManager : MonoBehaviour
{
    [SerializeField] private Light bossSightLightLow = null;
    [SerializeField] private Light bossSightLightHigh = null;

    [SerializeField] private Light flashlightDim = null;
    [SerializeField] private Light flashlightBright = null;

    // Start is called before the first frame update
    void Start()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;

        flashlightDim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightDim.enabled = true;
            }
            else if (flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightBright.enabled = true;
                flashlightDim.enabled = false;
            }
            else
            {
                flashlightBright.enabled = false;
                flashlightDim.enabled = false;
            }
        }
    }

    public void BossSawPlayer()
    {
        bossSightLightLow.enabled = true;
        bossSightLightHigh.enabled = false;
    }

    public void BossLostPlayer()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;

    }

    public void BossSeenPlayerTooLong()
    {
        StartCoroutine(BossSeenPlayerTooLongEnum());
    }

    private IEnumerator BossSeenPlayerTooLongEnum()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = true;
        yield return new WaitForSeconds(1.5f);
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;
    }
}
