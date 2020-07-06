using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Singleton : MonoBehaviour
{
    public string tagger;
    public AudioClip[] music;
    public AudioSource audioSource;
    int i=0;
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagger);
        if (objects.Length > 1) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();

        

    }
    private void Start()
    {
        if (tagger == "Music")
        {
            i = Random.Range(0, music.Length);
            audioSource.clip = music[i];
        audioSource.Play(); }
            

    }

    private void Update()
    {
        if (tagger == "Music")
        {
            if (!audioSource.isPlaying)
            {
                if (i < music.Length-1)
                {
                    i++;
                    audioSource.clip = music[i];
                    audioSource.Play();
                    
                }
                else i = 0;
            }
        }

    }
}
