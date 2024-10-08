using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 비디오 플레이어 참조
    public string sceneName; // 이동할 씬의 이름

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Configure the script to be called when the video finishes playback
        videoPlayer.loopPointReached += LoadSceneAfterVideo;
    }

    // This method will be called when the video ends
    public void LoadSceneAfterVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);
    }
}
