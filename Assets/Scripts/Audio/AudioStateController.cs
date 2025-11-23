using UnityEngine;
using UnityEngine.Audio;

public class AudioStateController : MonoBehaviour
{
    [Header("Snapshots")]
    public AudioMixerSnapshot menuSnapshot;
    public AudioMixerSnapshot gameplaySnapshot;

    [Header("Transition times (seconds)")]
    public float menuTransitionTime = 0.5f;
    public float gameplayTransitionTime = 1.0f;

    public void GoToMenu()
    {
        if (menuSnapshot != null)
            menuSnapshot.TransitionTo(menuTransitionTime);
    }

    public void StartGame()
    {
        if (gameplaySnapshot != null)
            gameplaySnapshot.TransitionTo(gameplayTransitionTime);
    }
}
