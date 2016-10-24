using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour {

    #region Attributes

    public enum videoType { intro, gameend };
    public videoType videotype;

    public MovieTexture movie;
    private AudioSource movieaudio;

    public int nextlevel;

    public bool playautomatic;
    public bool replayable;
    public bool hasended;

    #endregion

    #region Methods

    #region Awake And Start
    //Method is run only one and when the gameobject associed with the script awake
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

    #endregion

    #region Update Methods

    void Update()
    {
        if(!replayable)
            hasended = CheckMovieEnd();
        if (hasended)
            if (videotype == videoType.intro)
                Application.LoadLevel(nextlevel);
        PlayerInput();
    }

    #endregion

    #region Video Player Fundamental Methods

    #region Check Methods

    bool CheckMovieEnd()
    {
        if (!movie.isPlaying)
            return true;

        return false;
    }

    #endregion

    #region Player Comands Methods

    void PlayerInput()
    {
        if (Input.GetButtonDown("Confirm"))
            if (videotype == videoType.intro)
                Application.LoadLevel(nextlevel);
    }

    #endregion

    #endregion

    #endregion

}
