using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControl : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip Dot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Audio.PlayOneShot(Dot);
        }
    }
}
