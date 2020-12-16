using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] private Image blackOverlay;
    [SerializeField] private float textSpeed = .5f;
    private Text text;
    private AudioSource audioSource;
    private bool fading = false;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !fading)
        {
            StartCoroutine(FadeOut());
        }
        text.rectTransform.position = text.rectTransform.position + new Vector3(0f, textSpeed, 0f);
    }

    private IEnumerator FadeOut()
    {
        fading = true;
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        float elapsedTime = 0f;
        float duration = 2f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackOverlay.color = finalColor;
        SceneManager.LoadScene(1);

    }
}
