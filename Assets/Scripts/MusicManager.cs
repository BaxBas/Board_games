using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;

    AudioSource audioSource;
    List<int> playedSongs = new List<int>();
    int currentIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextSong();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    public void PlayNextSong()
    {
        if (songs.Length == 0) return;

        // Everything has been played, so start a new cycle
        if (playedSongs.Count >= songs.Length)
        {
            playedSongs.Clear();
        }

        int next;
        do
        {
            next = Random.Range(0, songs.Length);
        }
        // skip songs already played this cycle, and never repeat the current song back to back
        while (playedSongs.Contains(next) || (songs.Length > 1 && next == currentIndex));

        currentIndex = next;
        playedSongs.Add(next);
        audioSource.clip = songs[next];
        audioSource.Play();
    }
}