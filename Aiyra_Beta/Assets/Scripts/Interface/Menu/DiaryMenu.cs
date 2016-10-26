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

    public float buttontimer;
    #endregion

    #region Methods

    //Methods that check if the game is active or disable and alert the player
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
        LoadActorDiary();
        UploadDiaryPagesStatus();
    }

    #endregion

    #region Diary Fundamental Methods

    #region Upload CGs Methods
    //THIS METHOD UPLOAD THE STATUS OF THE Diary Page LIKE IT ID, IT NAME, IF IT IS UNLOCKED
    public void UploadDiaryPagesStatus()
    {
        //Inicialize a whip of the type for to run all page 0 diary
        for (int i = 0; i < actordiariespage0.Length; i++)
        {
            //change the diary id to it respective place in the whip
            actordiariespage0[i].cgid = i;
            //check what page is the player for than change the diary name to get it status from the collectiondata
            switch (currentpages)
            {
                case 0:
                    actordiariespage0[i].gameObject.name = "ENZODIARY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 1:
                    actordiariespage0[i].gameObject.name = "ISISDIARY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 2:
                    actordiariespage0[i].gameObject.name = "BENJAMINDIARY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 3:
                    actordiariespage0[i].gameObject.name = "MALIKADIARY" + actordiariespage0[i].cgid.ToString();
                    break;
            }
            //Check if player has unlock the diary page in collectiondata and load it from collectiondata
            collectiondata.SetActorCG(actordiariespage0[i]);
            collectiondata.LoadSpecficActorCGStatus();

            //check if the current cg is unlocked to provide it for the player
            if (actordiariespage0[i].isunlock)
            {
                actordiariespage0[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actordiariespage0[i].GetComponent<Button>().interactable = false;
            }

            //If diary page is unlocked update it internal information for the player
            actordiariespage0[i].UpdateCG();
        }
        //Same as before but to page 1 diary;
        for (int i = 0; i < actordiariespage1.Length; i++)
        {
            actordiariespage1[i].cgid = i;
            switch (currentpages)
            {
                case 0:
                    actordiariespage1[i].gameObject.name = "ISISDIARY" + actordiariespage1[i].cgid.ToString();
                    break;
                case 1:
                    actordiariespage1[i].gameObject.name = "BENJAMINDIARY" + actordiariespage1[i].cgid.ToString();
                    break;
                case 2:
                    actordiariespage1[i].gameObject.name = "MALIKADIARY" + actordiariespage1[i].cgid.ToString();
                    break;
                case 3:
                    actordiariespage1[i].gameObject.name = "ZAKIDIARY" + actordiariespage1[i].cgid.ToString();
                    break;
            }
            collectiondata.SetActorCG(actordiariespage1[i]);
            collectiondata.LoadSpecficActorCGStatus();

            if (actordiariespage1[i].isunlock)
            {
                actordiariespage1[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actordiariespage1[i].GetComponent<Button>().interactable = false;
            }

            actordiariespage1[i].UpdateCG();
        }
    }

    #endregion    

    #region Load Diary Methods

    public void LoadActorDiary()
    {
        Debug.Log("ActorDiaryLoaded");
    
        switch (currentpages)
        {
            case 0:
                for (int i = 0; i < actordiariespage0.Length; i++)
                {
                    actordiariespage0[i].SetCGPath("Backgrounds/Teste/");
                    switch (i)
                    {
                        case 0:
                            actordiariespage0[i].SetCGName("MainMenu");
                            break;
                        case 1:
                            actordiariespage0[i].SetCGName("CenarioSala");
                            break;
                        default:
                            actordiariespage0[i].SetCGPath("JardimTeste");
                            break;
                    }
                }
                for (int i = 0; i < actordiariespage1.Length; i++)
                {
                    actordiariespage1[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage1[i].SetCGName("JardimTeste");
                }
                break;
            case 1:
                for (int i = 0; i < actordiariespage0.Length; i++)
                {
                    actordiariespage0[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage0[i].SetCGName("JardimTeste");
                }
                for (int i = 0; i < actordiariespage1.Length; i++)
                {
                    actordiariespage1[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage1[i].SetCGName("CenarioSala");
                }
                break;
            case 2:
                for (int i = 0; i < actordiariespage0.Length; i++)
                {
                    actordiariespage0[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage0[i].SetCGName("CenarioSala");
                }
                for (int i = 0; i < actordiariespage1.Length; i++)
                {
                    actordiariespage1[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage1[i].SetCGName("Quarto1");
                }
                break;
            case 3:
                for (int i = 0; i < actordiariespage0.Length; i++)
                {
                    actordiariespage0[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage0[i].SetCGName("Quarto1");
                }
                for (int i = 0; i < actordiariespage1.Length; i++)
                {
                    actordiariespage1[i].SetCGPath("Backgrounds/Teste/");
                    actordiariespage1[i].SetCGName("SalaNormal.2");
                }
                break;
        }
    }

    #endregion

    #endregion

    #region Buttons Methods

    public void LoadNextPage()
    {
        if (buttontimer <= 0)
        {
            if (currentpages < 5)
            {
                gofowardbutton.interactable = true;
                gobackbutton.interactable = true;
                currentpages++;
                LoadActorDiary();
                UploadDiaryPagesStatus();
            }
            else
            {
                gofowardbutton.interactable = false;
                gobackbutton.interactable = true;
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.1f; }
    }
    public void LoadBackPage()
    {
        if (buttontimer <= 0)
        {
            if (currentpages > 0)
            {
                gobackbutton.interactable = true;
                gofowardbutton.interactable = true;
                currentpages--;
                LoadActorDiary();
                UploadDiaryPagesStatus();
            }
            else
            {
                gobackbutton.interactable = false;
                gofowardbutton.interactable = true;
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.1f; }
    }
    public void DisplayCGOnCGDisplayer(ActorCG ActorCG)
    {
        if (ActorCG.isunlock)
        {
            displaycgmenu.lastmenu = gameObject;
            displaycgmenu.gameObject.SetActive(true);
            displaycgmenu.cgdisplayer.cg = ActorCG.cgimage.sprite;
            displaycgmenu.SetCGDisplayerTitle(ActorCG.cgname);
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
