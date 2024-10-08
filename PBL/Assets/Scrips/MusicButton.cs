using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip Music;
    private void OnMouseDown()
    {
        Audio.PlayOneShot(Music);
    }
}
