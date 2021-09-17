using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource AD;
    public AudioClip[] audio;
    
    private void Awake()
    {
        AD = this.gameObject.GetComponent<AudioSource>();
    }
    public void PlayAudio(int num)
    {
        AD.clip = audio[num];
        AD.Play();
    }
}
