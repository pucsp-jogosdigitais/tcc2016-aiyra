using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiaryMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public Button gobackbutton;
    public Button gofowardbutton;
    public DisplayCGMenu displaycgmenu;
    public ActorCG[] actordiaries;

    public int currentpages;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Diary Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("Diary Menu Desactive");
    }

    #endregion

    #region Buttons Methods
    public void LoadActorDiary()
    {
        Debug.Log("ActorDiaryLoaded");
        /*switch (CurrentPage)
        {
            case 0: for(int)
        }
        if (ActorButton.actor.actorname == "Enzo")
        {
            for (int i = 0; i < actordiaries.Length; i++)
            {
                actordiaries[i].SetCGID(i);
                actordiaries[i].SetCGPath("Backgrounds/Teste/");
                if (i == 0)
                    actordiaries[i].isunlock = false;
                //actorcgs[i].SetCGName("MainMenu");
                else
                {
                    actordiaries[i].SetCGName("JardimTeste");
                }
            }
        }
        */
    }
    public void LoadNextPage()
    {
        if (currentpages < 5)
        {
            gofowardbutton.interactable = true;
            gobackbutton.interactable = true;
            currentpages++;
            LoadActorDiary();
        }
        else
        {
            gofowardbutton.interactable = false;
            gobackbutton.interactable = true;
        }
    }
    public void LoadBackPage()
    {
        if(currentpages > 0)
        {
            gobackbutton.interactable = true;
            gofowardbutton.interactable = true;
            currentpages--;
            LoadActorDiary();
        }
        else
        {
            gobackbutton.interactable = false;
            gofowardbutton.interactable = true;
        }
    }
    public void DisplayCGOnCGDisplayer(ActorCG ActorCG)
    {
        if (ActorCG.isunlock)
        {
            displaycgmenu.lastmenu = gameObject;
            displaycgmenu.gameObject.SetActive(true);
            displaycgmenu.cgdisplayer.cg = ActorCG.cgimage.sprite;
        }
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
