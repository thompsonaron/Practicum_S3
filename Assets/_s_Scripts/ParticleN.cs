using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleN : MonoBehaviour
{

    public ParticleSystem dash;
    public AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            dash.Play();
            audioManager.Play(ListAllAudio.W_Revolver);
        }
    }
}
