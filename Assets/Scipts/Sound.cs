using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private static AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public static void playLanding ()
    {
        source.Play();
    }

}

