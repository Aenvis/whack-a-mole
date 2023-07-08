using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private const float volumeDelta = 0.05f;
    [SerializeField] private AudioData playlist;
    [SerializeField] private AudioSource source;
    private bool _isPaused;

    private void Start()
    {
        if (source.clip is null)
            PlayRandomTrack();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) PlayRandomTrack();

        if (Input.GetKeyDown(KeyCode.P)) PlayPauseMusic();

        if (Input.GetKeyDown(KeyCode.DownArrow)) source.volume -= volumeDelta;

        if (Input.GetKeyDown(KeyCode.UpArrow)) source.volume += volumeDelta;

        if (!source.isPlaying && !_isPaused)
            PlayRandomTrack();
    }

    private void PlayRandomTrack()
    {
        var clip = playlist.GetRandom();

        while (clip.name == source.clip?.name) clip = playlist.GetRandom();

        source.clip = clip;
        source.Play();
        GameManager.Instance.OnNewTrack?.Invoke(clip.name);
    }

    private void PlayPauseMusic()
    {
        if (_isPaused) source.Play();
        else source.Pause();

        _isPaused = !_isPaused;
    }
}