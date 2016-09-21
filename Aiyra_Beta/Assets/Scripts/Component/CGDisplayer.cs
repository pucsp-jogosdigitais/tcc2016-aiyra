using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AudioSource))]
public class CGDisplayer : MonoBehaviour {

    #region Attributes

    public enum CGType { gameend, cg};
    public CGType cgtype;

    public GameObject movieplayerbuttonsbox;
    public Button movieplayerloopbutton;
    public RawImage displayer;
    public Sprite cg;
    public MovieTexture movie;
    public AudioSource movieaudio;

    public bool hasended;
    public bool islooped;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("CGDisplayer Active");
    }
    void OnDisable()
    {
        Debug.Log("CGDisplayer Desactive");
    }

    #endregion

    #region Awake And Start

    void Awake()
    {
        if (displayer == null)
            displayer = GetComponent<RawImage>();
        if (cg == null)
            Debug.LogWarning("CGDisplayer has no cg to display");
        if (movie == null)
            Debug.LogWarning("CGDisplayer has no movie to play");
        if (movieaudio == null)
            movieaudio = GetComponent<AudioSource>();
    }

    #endregion

    #region Update Methods

    void Update()
    {
        if(movie != null)
        {
            movieplayerbuttonsbox.SetActive(true);

            if (displayer.texture != movie as MovieTexture)
            {
                displayer.texture = movie as MovieTexture;
                movieaudio.clip = movie.audioClip;
            }

            hasended = CheckMovieEnd();
        }
        else
        {
            movieplayerbuttonsbox.SetActive(false);

            if (displayer.texture != cg.texture)
                displayer.texture = cg.texture;
        }

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

    #endregion

    #endregion

}
