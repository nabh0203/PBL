using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip BGM;
    public Text timerText;
    public List<GameObject> Dots = new List<GameObject>();
    public string Scenename;
    public GameObject Oil1;
    public GameObject Oil2;
    public GameObject Oil3;
    public GameObject Oil4;
    private float timeLeft = 50f;
    private bool isPaused = false;

    void Start()
    {
        StartCoroutine(WaitForSceneSwitch());
        Audio.PlayOneShot(BGM);
    }

    void Update()
    {
        UpdateTimer();

        if (!isPaused) 
        {
            CheckDotsRemaining();
        }
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

    void CheckDotsRemaining()
    {
        GameObject[] remainingDots = GameObject.FindGameObjectsWithTag("dot");
        if (remainingDots.Length == 64)
        {
            Oil1.SetActive(true);
        }
        else if (remainingDots.Length == 60)
        {
            Oil2.SetActive(true);
        }
        else if (remainingDots.Length == 40)
        {
            Oil3.SetActive(true);
        }
        else if (remainingDots.Length == 20)
        {
            Oil4.SetActive(true);
        }
        else if (remainingDots.Length == 0)
        {
            SceneManager.LoadScene(Scenename);
        }
    }

}
