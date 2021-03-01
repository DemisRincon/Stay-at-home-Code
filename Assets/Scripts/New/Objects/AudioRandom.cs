using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioRandom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private AudioClip[] audioList;
  

    [SerializeField] private VideoClip[] videoList;
   


    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip GetAudioSource()
    {
        return audioList[GetRandomNumber(audioList.Length)];
    }
    public VideoClip GetVideoSource()
    {
        return videoList[GetRandomNumber(videoList.Length)];
    }
    private int GetRandomNumber(int lenght) {
        return Random.Range(0, lenght);
    }



}
