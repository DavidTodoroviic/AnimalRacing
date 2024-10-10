//ChatGPT Generated Code
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public Animator animator;       // Reference to the Animator component
    public string nextSceneName;    // The name of the next scene to load
    public string animationName = "TVMove"; // Name of the animation to play

    void Start()
    {
        if (videoPlayer == null || animator == null)
        {
            Debug.LogError("VideoPlayer or Animator is not assigned.");
            return;
        }

        // Disable the Animator to prevent automatic playback
        animator.enabled = false;

        // Prepare the VideoPlayer
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnVideoError;
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video prepared. Starting playback.");
        vp.Play();
        StartCoroutine(PlayVideoThenAnimation());
    }

    private void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError($"VideoPlayer error: {message}");
    }

    private IEnumerator PlayVideoThenAnimation()
    {
        // Wait until the video has finished
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Destroy the video player GameObject after the video finishes
        Debug.Log("Video finished. Destroying VideoPlayer GameObject.");
        Destroy(videoPlayer.gameObject);

        // Enable the Animator and play the animation
        animator.enabled = true;
        animator.Play(animationName);

        // Wait until the animation finishes
        AnimationClip clip = GetAnimationClip(animationName);
        if (clip != null)
        {
            Debug.Log($"Playing animation '{animationName}' with duration {clip.length} seconds.");
            yield return new WaitForSeconds(clip.length);
        }
        else
        {
            Debug.LogError("Animation clip not found.");
        }

        // Load the next scene
        Debug.Log($"Loading next scene: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }

    private AnimationClip GetAnimationClip(string clipName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }
}
