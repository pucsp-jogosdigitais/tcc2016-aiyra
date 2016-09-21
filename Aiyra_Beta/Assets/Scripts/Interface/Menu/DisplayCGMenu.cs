using UnityEngine;
using System.Collections;

public class DisplayCGMenu : MonoBehaviour {

    #region Attributes

    public CGDisplayer cgdisplayer;
    public GameObject lastmenu;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Display CG Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("Display CG Menu Desactive");
    }

    #endregion

    #region Buttons Methods
    public void PlayButton()
    {
        if (cgdisplayer.movie != null)
        {
            if (!cgdisplayer.movie.isPlaying)
            {
                cgdisplayer.movie.Play();
                cgdisplayer.movieaudio.Play();
            }
        }
    }
    public void PlayAgainButton()
    {
        if(cgdisplayer.movie != null)
        {
            cgdisplayer.movie.Stop();
            cgdisplayer.movieaudio.Stop();
            cgdisplayer.movie.Play();
            cgdisplayer.movieaudio.Play();
        }
    }
    public void LoopButton()
    {
        if(cgdisplayer.movie != null)
        {
            if (!cgdisplayer.movie.loop)
            {
                cgdisplayer.movie.loop = true;
                cgdisplayer.movieaudio.loop = true;
            }
            else
            {
                cgdisplayer.movie.loop = true;
                cgdisplayer.movieaudio.loop = true;
            }
        }
    }
    public void ReturnButton()
    {
        if (cgdisplayer.movie != null)
        {
            cgdisplayer.movie.Stop();
            cgdisplayer.movieaudio.Stop();
        }
        gameObject.SetActive(false);
        lastmenu.SetActive(true);
    }

    #endregion

    #endregion
}
