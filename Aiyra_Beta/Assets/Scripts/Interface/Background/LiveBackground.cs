using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AudioSource))]
public class LiveBackground : MonoBehaviour {

    #region Attributes
    public enum LiveBackgroundBehaviour { random, inorder, justone }
    public LiveBackgroundBehaviour livebackgroundbehaviour;

    public MovieTexture[] movies;
    private AudioSource movieaudio;

    public int currentmovie;
    public int nextmovie;

    public bool replayable;
    public bool playautomatic;
    public bool hasended;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Live Background active");
    }
    void OnDisable()
    {
        Debug.Log("Live Background desactive");
    }

    #endregion

    #region Awake And Start
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        movieaudio = GetComponent<AudioSource>();
        UploadLiveBackground();
    }
    void Start()
    {
        if (movies.Length > 0)
        {
            if (replayable)
            {
                movies[currentmovie].loop = true;
                movieaudio.loop = true;
            }
            else
            {
                movies[currentmovie].loop = false;
                movieaudio.loop = false;
            }
            if(livebackgroundbehaviour == LiveBackgroundBehaviour.justone)
            {
                if (playautomatic)
                {
                    movies[currentmovie].Play();
                    movieaudio.Play();
                }
            }
            if(livebackgroundbehaviour == LiveBackgroundBehaviour.inorder)
            {
                if(playautomatic)
                {
                    movies[currentmovie].Play();
                    movieaudio.Play();
                }
            }
            
        }
    }

    #endregion

    #region Update Method

    public void UpdateLiveBackground()
    {
        hasended = CheckMovieEnd();
        if (hasended)
            UploadLiveBackground();
    }


    #endregion

    #region Live Background Fundamental Methods
    
    #region Check Methods

    bool CheckMovieEnd()
    {
        if (!movies[currentmovie].isPlaying)
            return true;

        return false;
    }

    #endregion

    #region Upload LiveBackground Methods

    public void UploadLiveBackground()
    {
        if (movies.Length > 0)
        {
            if (livebackgroundbehaviour == LiveBackgroundBehaviour.justone)
            {
                GetComponent<RawImage>().texture = movies[currentmovie] as MovieTexture;
                movieaudio.clip = movies[currentmovie].audioClip;

                movies[currentmovie].Play();
                movieaudio.Play();
            }
            if (livebackgroundbehaviour == LiveBackgroundBehaviour.random)
            {
                nextmovie = Random.Range(0, movies.Length);
                currentmovie = nextmovie;
                GetComponent<RawImage>().texture = movies[currentmovie] as MovieTexture;
                movieaudio.clip = movies[currentmovie].audioClip;

                movies[currentmovie].Play();
                movieaudio.Play();
            }
            if(livebackgroundbehaviour == LiveBackgroundBehaviour.inorder)
            {
                if (nextmovie < movies.Length - 1)
                {
                    nextmovie++;
                    currentmovie = nextmovie;
                }
                else
                {
                    nextmovie = 0;
                }

                GetComponent<RawImage>().texture = movies[currentmovie] as MovieTexture;
                movieaudio.clip = movies[currentmovie].audioClip;

                movies[currentmovie].Play();
                movieaudio.Play();
            }
        }
        else
        {
            Debug.Log("No movies to upload");
        }
    }

    #endregion

    #endregion

    #endregion

}
