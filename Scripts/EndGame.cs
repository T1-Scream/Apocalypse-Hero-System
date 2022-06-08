using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Player.Movement;

public class EndGame : MonoBehaviour
{

    [SerializeField] private RawImage videoImage;
    [SerializeField] private VideoPlayer video;
    public string url;

    private bool isPlaying;

    //private void Start()
    //{
    //    video.url = url;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndVideo();
    }

    private void Update()
    {
        if (!isPlaying) return;

        LastFrame();
    }

    private void LastFrame()
    {
        long curFrame = video.frame;
        long frameCount = Convert.ToInt64(video.frameCount);

        if (curFrame == frameCount - 1)
            SceneManager.LoadSceneAsync("Menu");
    }

    public void EndVideo()
    {
        FindObjectOfType<PlayerMovement>().canMove = false;
        videoImage.gameObject.SetActive(true);
        video.gameObject.SetActive(true);
        video.Play();
        isPlaying = true;
    }
}
