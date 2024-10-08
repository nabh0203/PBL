using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalagaGM : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip BGM;
    public Text timerText;
    public string Scenename;
    private float timeLeft = 50f;

    void Start()
    {
        StartCoroutine(WaitForSceneSwitch());
        Audio.PlayOneShot(BGM);
        
    }

    void Update()
    {
        UpdateTimer();
    }
    IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(50f);
        SceneManager.LoadScene(Scenename);
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }

        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

}
