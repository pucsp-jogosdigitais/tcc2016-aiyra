using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour {

    public enum videoType { intro };
    public videoType videotype;

    public MovieTexture movie;
    private AudioSource movieaudio;

    public int nextlevel;

    public bool playautomatic;
    public bool replayable;
    public bool hasended;

    void Awake()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        movieaudio = GetComponent<AudioSource>();
        movieaudio.clip = movie.audioClip;
    }
    void Start()
    {
        if(replayable)
        {
            movie.loop = true;
        }
        else
        {
            movie.loop = false;
        }
        if (playautomatic)
        {
            movie.Play();
            movieaudio.Play();
        }
    }
    void Update()
    {
        if(!replayable)
            hasended = CheckMovieEnd();
        if (hasended)
            if (videotype == videoType.intro)
                Application.LoadLevel(nextlevel);
        PlayerInput();
    }
    bool CheckMovieEnd()
    {
        if (!movie.isPlaying)
            return true;

        return false;
    }
    void PlayerInput()
    {
        if (Input.GetButtonDown("Confirm"))
            if (videotype == videoType.intro)
                Application.LoadLevel(nextlevel);
    }
}
