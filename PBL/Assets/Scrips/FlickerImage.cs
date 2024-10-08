using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlickerImage : MonoBehaviour
{
    public Image flickeringImage;
    public float flickerInterval = 0.5f;
    public int flickerCount = 3;

    private IEnumerator Flicker()
    {
        Color originalColor = flickeringImage.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        for (int i = 0; i < flickerCount; i++)
        {
            flickeringImage.color = transparentColor;
            yield return new WaitForSeconds(flickerInterval);
            flickeringImage.color = originalColor;
            yield return new WaitForSeconds(flickerInterval);
        }
    }

    private void Start()
    {
        StartCoroutine(Flicker());
    }
}
