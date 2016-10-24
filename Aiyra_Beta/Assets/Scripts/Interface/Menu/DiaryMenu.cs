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
        UploadDiaryPagesStatus();
    }

    #endregion

    #region Diary Fundamental Methods
    //THIS METHOD UPLOAD THE STATUS OF THE Diary Page LIKE IT ID, IT NAME, IF IT IS UNLOCKED
    public void UploadDiaryPagesStatus()
    {
        //Inicialize a whip of the type for to run all page 0 diary
        for (int i = 0; i < actordiariespage0.Length; i++)
        {
            //change the diary id to it respective place in the whip
            actordiariespage0[i].cgid = i;
            //check what page is the player for than change the cg name to get it status from the collectiondata
            switch (currentpages)
            {
                case 0:
                    actordiariespage0[i].gameObject.name = "ENZOGALLERY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 1:
                    actordiariespage0[i].gameObject.name = "ISISGALLERY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 2:
                    actordiariespage0[i].gameObject.name = "BENJAMINGALLERY" + actordiariespage0[i].cgid.ToString();
                    break;
                case 3:
                    actordiariespage0[i].gameObject.name = "MALIKAGALLERY" + actordiariespage0[i].cgid.ToString();
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
        /*
        //Inicialize a whip of the type for to run all page 0 cg
        for (int i = 0; i < actorcgspage0.Length; i++)
        {
            //change the cg id to it respective place in the whip
            actorcgspage0[i].cgid = i;
            //check what page is the player for than change the cg name to get it status from the collectiondata
            switch(currentpages)
            {
                case 0:
                    actorcgspage0[i].gameObject.name = "ENZOGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 1:
                    actorcgspage0[i].gameObject.name = "ISISGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 2:
                    actorcgspage0[i].gameObject.name = "BENJAMINGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 3:
                    actorcgspage0[i].gameObject.name = "MALIKAGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
            }

            //Check if player has unlock the cg in collectiondata and load it from collectiondata
            collectiondata.SetActorCG(actorcgspage0[i]);
            collectiondata.LoadSpecficActorCGStatus();
            
            //check if the current cg is unlocked to provide it for the player
            if (actorcgspage0[i].isunlock)
            {
                actorcgspage0[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actorcgspage0[i].GetComponent<Button>().interactable = false;
            }

            //If cg is unlocked update it internal information for the player
            actorcgspage0[i].UpdateCG();
        }
        //Same thing has the last whip but for page 1
        for (int i = 0; i < actorcgspage1.Length; i++)
        {
            actorcgspage1[i].cgid = i;
            switch (currentpages)
            {
                case 0:
                    actorcgspage1[i].gameObject.name = "ISISGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 1:
                    actorcgspage1[i].gameObject.name = "BENJAMINGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 2:
                    actorcgspage1[i].gameObject.name = "MALIKAGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
                case 3:
                    actorcgspage1[i].gameObject.name = "ZAKIGALLERY" + actorcgspage0[i].cgid.ToString();
                    break;
            }

            collectiondata.SetActorCG(actorcgspage1[i]);
            collectiondata.LoadSpecficActorCGStatus();

            if (actorcgspage1[i].isunlock)
            {
                actorcgspage1[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                actorcgspage1[i].GetComponent<Button>().interactable = false;
            }

            actorcgspage1[0].UpdateCG();

        }
         */
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
        if (buttontimer <= 0)
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
            buttontimer = 0.2f;
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
            }
            else
            {
                gobackbutton.interactable = false;
                gofowardbutton.interactable = true;
            }
            buttontimer = 0.2f;
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
