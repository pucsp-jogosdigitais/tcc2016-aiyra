using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCGMenu : MonoBehaviour {

    #region Attributes

    public CGDisplayer cgdisplayer;
    public GameObject lastmenu;
    public Text cgdisplayertitle;

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

    #region CGDisplayer Fundamental Methods

    #region Set Values Methods

    public void SetCGDisplayerTitle(string NewTitle)
    {
        if(NewTitle.Length > 0 && NewTitle != "")
            cgdisplayertitle.text = NewTitle;
        else
        {
            cgdisplayertitle.text = "CGDisplayer";
            Debug.Log("Haven�t pass any value to cgdisplayer so it can change it title");
        }
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

    #endregion
}
