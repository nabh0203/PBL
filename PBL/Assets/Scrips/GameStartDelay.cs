using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartDelay : MonoBehaviour
{
    public float startDelay = 3f;
    public GameObject gameElements;
    public Text countdownText;

    void OnEnable()
    {
        gameElements.SetActive(false);
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay()
    {
        float currentTime = startDelay;
        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1);
            currentTime -= 1;
        }

        countdownText.text = "";
        gameElements.SetActive(true);
    }
}
