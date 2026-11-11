using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    // add this script to an empty GameObject
    // adjust array size (aka your amount of songs) in the inspector and select or drag & drop your songs

    public AudioClip[] songs;
    AudioSource audioSource;
    int randomSongIndex;
    int amountOfSongs;
    List<int> playedSongs = new List<int>();
    int songsPlayed = 0;
    int lastPlayed; // for avoiding playing the same song twice even after list clear

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        amountOfSongs = songs.Length;
        randomSongIndex = (int)Mathf.Round(Random.Range(0, amountOfSongs));
        playedSongs.Add(randomSongIndex);
        songsPlayed++;
        audioSource.clip = songs[randomSongIndex];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (songsPlayed == amountOfSongs)
            {
                lastPlayed = randomSongIndex;
                playedSongs.Clear();
                while (randomSongIndex == lastPlayed)
                {
                    randomSongIndex = (int)Mathf.Round(Random.Range(0, amountOfSongs));
                }
            }
            else
            {
                while (playedSongs.Contains(randomSongIndex))
                {
                    randomSongIndex = (int)Mathf.Round(Random.Range(0, amountOfSongs));
                }
            }
            playedSongs.Add(randomSongIndex);
            songsPlayed++;
            audioSource.clip = songs[randomSongIndex];
            audioSource.Play();
        }
    }
}