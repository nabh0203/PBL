using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeIn : MonoBehaviour
{
    public Text text;
    public float fadeInDuration = 2f;

    private void Start()
    {
        StartCoroutine(FadeInText(fadeInDuration, text));
    }

    private IEnumerator FadeInText(float duration, Text targetText)
    {
        Color originalColor = targetText.color;
        float counter = -0.5f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);

            targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
