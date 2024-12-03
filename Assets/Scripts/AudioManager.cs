using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------AUDIO SOURCE---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("--------AUDIO CLIPS---------")]
    //public AudioClip background;
    public AudioClip jump;
    public AudioClip coinCollected;

    private void Start()
    {
       // musicSource.clip = background;
        //musicSource.Play();
    }

    public void SFXSound(AudioClip clip)

    {
        SFXSource.PlayOneShot(clip);    
    }
}
