using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlickerText : MonoBehaviour
{
    public Text flickeringText;
    public float flickerInterval = 0.5f;
    public int flickerCount = 3;

    private IEnumerator Flicker()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            flickeringText.enabled = !flickeringText.enabled;
            yield return new WaitForSeconds(flickerInterval);
            flickeringText.enabled = !flickeringText.enabled;
            yield return new WaitForSeconds(flickerInterval);
        }
    }

    private void Start()
    {
        StartCoroutine(Flicker());
    }
}
