using UnityEngine;

/// <summary>
/// Responsible for the audio effects of the quiz.
/// </summary>
/// Attached to the _GameManager object.
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip RightAnswerAudio;
    [SerializeField] private AudioClip WrongAnswerAudio;
    [SerializeField] private AudioClip WinGameAudio;

    /// <summary>
    /// Plays a sound to indicate that the answer was right.
    /// </summary>
    public void PlayRightAnswerAudio() {
        AudioSource.clip = RightAnswerAudio;
        AudioSource.Play();
    }

    /// <summary>
    /// Plays a sound to indicate that the answer was wrong.
    /// </summary>
    public void PlayWrongAnswerAudio() {
        AudioSource.clip = WrongAnswerAudio;
        AudioSource.Play();
    }

    /// <summary>
    /// Plays a sound to indicate that the game is over.
    /// </summary>
    public void PlayWinAudio() {
        AudioSource.clip = WinGameAudio;
        AudioSource.Play();
    }
}
