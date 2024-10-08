using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip Music;
    // Start is called before the first frame update
    void Start()
    {
        Audio.PlayOneShot(Music);
    }

}
