using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoTransition : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;        
    }
    void LoadScene(VideoPlayer vp)
    {               
        SceneManager.LoadScene(SceneName);
    }
}