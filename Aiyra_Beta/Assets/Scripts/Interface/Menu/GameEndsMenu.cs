using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEndsMenu : MonoBehaviour {

    #region Movies Paths and names

    private string enzoendspath = "Videos/Intro/Teste/";
    private string enzoperfectendname = "TesteGameIntro";

    #endregion

    #region Attributes

    public GameObject albummenu;
    public CollectionData collectiondata;
    public DisplayCGMenu displaycgmenu;
    public ActorButton[] actorbuttons;

    public string endstype;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("GameEnds Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("GameEnds Menu Desactive");
    }

    #endregion

    #region Buttons Methods
    public void EndButton(ActorButton ActorButton)
    {
        if (endstype == "Perfeito")
        {
            if (ActorButton.actor.actorname == "Enzo")
            {
                displaycgmenu.gameObject.SetActive(true);
                displaycgmenu.cgdisplayer.movie = Resources.Load<MovieTexture>(enzoendspath + enzoperfectendname);
            }
        }
        displaycgmenu.SetCGDisplayerTitle("Final " + endstype + " com " + ActorButton.actor.actorname);
        displaycgmenu.lastmenu = gameObject;
    }
    public void PerfectEndsButton()
    {
        Debug.Log("Loaded perfect ends");
        endstype = "Perfeito";
        for(int i = 0;i< actorbuttons.Length;i++)
        {
            switch(i)
            {
                case 0:
                    actorbuttons[i].actornamebuttontext.text = "Enzo em um /n romance serio ... !!! kkkkkkk";
                    break;
                case 1:
                    actorbuttons[i].actornamebuttontext.text = "Isis vira Lesbica kkkkkkkkk";
                    break;
                case 2:
                    actorbuttons[i].actornamebuttontext.text = "blbla";
                    break;
                case 3:
                    actorbuttons[i].actornamebuttontext.text = "Zaki domina o mundo humano";
                    break;
                case 4:
                    actorbuttons[i].actornamebuttontext.text = "BLa bla bal";
                    break;
            }
        }
    }
    public void NormalEndsButton()
    {
        Debug.Log("Loaded normal ends");
    }
    public void BadEndsButton()
    {
        Debug.Log("Loaded bad ends");
    }
    public void GalleryButton()
    {
        albummenu.GetComponent<AlbumMenu>().gallerybox.SetActive(true);
        gameObject.SetActive(false);
    }
    public void DiaryButton()
    {
        albummenu.GetComponent<AlbumMenu>().diarybox.SetActive(true);
        gameObject.SetActive(false);
    }
    public void GameEndsButton()
    {
        albummenu.GetComponent<AlbumMenu>().gameendsbox.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ReturnButton()
    {
        gameObject.SetActive(false);
        albummenu.SetActive(true);
    }

    #endregion

    #endregion

}
