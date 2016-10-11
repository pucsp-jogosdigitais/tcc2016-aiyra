using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndsMenu : MonoBehaviour {

    #region Movies Paths and names

    private string enzoendspath = "Videos/Intro/Teste/";
    private string enzoperfectendname = "TesteGameIntro";

    #endregion

    #region Attributes

    public GameObject gameendsmenu;
    public DisplayCGMenu displaycgmenu;
    public Text especficendtext;
    public ActorButton[] actorbuttons;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Ends Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("Ends Menu Desactive");
    }

    #endregion

    #region Buttons Methods

    public void LoadActorCGsAndPlay(ActorButton ActorButton)
    {
        /*
        if (ActorButton.actor.actorname == "Enzo")
        {
            if (especficendtext.text == "Finais Perfeitos")
            {
                displaycgmenu.gameObject.SetActive(true);
                displaycgmenu.cgdisplayer.movie = Resources.Load<MovieTexture>(enzoendspath+enzoperfectendname);
            }
            else if(especficendtext.text == "Finais Normais")
            {

            }
            else if(especficendtext.text == "Finais Ruins")
            {

            }
            else
            {
                Debug.LogError("No end type found");
            }
        }
        */
        displaycgmenu.lastmenu = gameObject;
    }
    
    public void ReturnButton()
    {
        gameObject.SetActive(false);
        gameendsmenu.SetActive(true);
    }
    

    #endregion

    #endregion
}
