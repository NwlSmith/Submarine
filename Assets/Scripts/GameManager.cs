using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private Image blackOverlay = null;

    public int numPillars = 5;

    private void Awake()
    {
        // Ensure that there is only one instance of the GameManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        StartCoroutine(BlackOverlayFadeOut());
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private IEnumerator BlackOverlayFadeOut()
    {
        blackOverlay.color = new Color(0f, 0f, 0f, 1f);
        float duration = 2f;
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 1f);
        Color finalColor = new Color(0f, 0f, 0f, 0f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackOverlay.color = finalColor;
    }

    private IEnumerator BlackOverlayFadeIn()
    {
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        float duration = 2f;
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackOverlay.color = finalColor;
    }

    public void ExplodedPillar()
    {
        numPillars--;
    }
}
