using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ���� �÷��̾� ����
    public string sceneName; // �̵��� ���� �̸�

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
