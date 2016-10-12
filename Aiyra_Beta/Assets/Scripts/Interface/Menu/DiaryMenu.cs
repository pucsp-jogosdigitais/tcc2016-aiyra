using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiaryMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public Button gobackbutton;
    public Button gofowardbutton;
    public CollectionData collectiondata;
    public DisplayCGMenu displaycgmenu;
    public ActorCG[] actordiariespage0;
    public ActorCG[] actordiariespage1;

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

    #region Awake And Start Methods

    void Start()
    {
        Debug.Log("Diary Start and Set CGs");
        UploadDiaryPagesStatus();
    }

    #endregion

    #region Diary Fundamental Methods

    public void UploadDiaryPagesStatus()
    {
        for (int i = 0; i < actordiariespage0.Length; i++)
        {
            actordiariespage0[i].cgid = i;
            if (actordiariespage0[i].isunlock)
            {
                actordiariespage0[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actordiariespage0[i].GetComponent<Button>().interactable = false;
            }
        }
        for (int i = 0; i < actordiariespage1.Length; i++)
        {
            actordiariespage1[i].cgid = i;
            if (actordiariespage1[i].isunlock)
            {
                actordiariespage1[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actordiariespage1[i].GetComponent<Button>().interactable = false;
            }
        }
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
