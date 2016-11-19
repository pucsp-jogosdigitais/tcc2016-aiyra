using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public Button gobackbutton;
    public Button gofowardbutton;
    public CollectionData collectiondata;
    public DisplayCGMenu displaycgmenu;
    public ActorCG[] actorcgspage0;
    public ActorCG[] actorcgspage1;

    public int currentpages;
    public int numberofpages;

    public float buttontimer;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Gallery Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("Gallery Menu Desactive");
    }

    #endregion

    #region Awake And Start Methods
    
    //method that will take care the inicialize of the gallery with methods that need to be execute when it start
    void Start()
    {
        Debug.Log("Gallery Start and Set CGs");
        LoadActorGallery();
        UploadCGSStatus();
    }

    #endregion

    #region Gallery Fundamental Methods

    #region Upload CGs Methods

    //THIS METHOD UPLOAD THE STATUS OF THE CG LIKE IT ID, IT NAME, IF IT IS UNLOCKED
    public void UploadCGSStatus()
    {
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
    }

    #endregion

    #region Load Gallery Methods

    public void LoadActorGallery()
    {
        Debug.Log("ActorGalleryLoaded");

        switch (currentpages)
        {
            case 0:
                for (int i = 0; i < actorcgspage0.Length; i++)
                {
                    //Enzo
                    actorcgspage0[i].SetCGPath("CGs/");
                    switch (i)
                    {
                        case 0:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                        case 1:
                            actorcgspage0[i].SetCGName("Enzo e protagonista 1");
                            break;
                        default:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                    }
                }
                for (int i = 0; i < actorcgspage1.Length; i++)
                {
                    //Isis
                    actorcgspage1[i].SetCGPath("CGs/");
                    switch (i)
                    {
                        case 0:
                            actorcgspage1[i].SetCGName("PuzzlePatio1.2");
                            break;
                        case 1:
                            actorcgspage1[i].SetCGName("Isis e protagonista 1");
                            break;
                        default:
                            actorcgspage1[i].SetCGName("PuzzlePatio1.2");
                            break;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < actorcgspage0.Length; i++)
                {
                    //Isis
                    actorcgspage0[i].SetCGPath("CGs/");
                    switch(i)
                    {
                        case 0:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                        case 1:
                            actorcgspage0[i].SetCGName("Isis e protagonista 1");
                            break;
                        default:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                    }
                }
                for (int i = 0; i < actorcgspage1.Length; i++)
                {
                    //Ben
                    actorcgspage1[i].SetCGPath("CGs/");
                    switch (i)
                    {
                        case 0:
                            actorcgspage1[i].SetCGName("PuzzlePatio1.2");
                            break;
                        case 1:
                            actorcgspage0[i].SetCGName("Benjamin e protagonista 1");
                            break;
                        default:
                            actorcgspage1[i].SetCGName("PuzzlePatio1.2");
                            break;
                    }
                    
                }
                break;
            case 2:
                for (int i = 0; i < actorcgspage0.Length; i++)
                {
                    //Ben
                    actorcgspage0[i].SetCGPath("CGs/");
                    switch (i)
                    {
                        case 0:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                        case 1:
                            actorcgspage0[i].SetCGName("Benjamin e protagonista 1");
                            break;
                        default:
                            actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                            break;
                    }
                }
                for (int i = 0; i < actorcgspage1.Length; i++)
                {
                    actorcgspage1[i].SetCGPath("CGs/");
                    actorcgspage1[i].SetCGName("PuzzlePatio1.2");
                }
                break;
            case 3:
                for (int i = 0; i < actorcgspage0.Length; i++)
                {
                    actorcgspage0[i].SetCGPath("CGs/");
                    actorcgspage0[i].SetCGName("PuzzlePatio1.2");
                }
                for (int i = 0; i < actorcgspage1.Length; i++)
                {
                    actorcgspage1[i].SetCGPath("CGs/");
                    actorcgspage1[i].SetCGName("PuzzlePatio1.2");
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
            if (currentpages < numberofpages)
            {
                gofowardbutton.interactable = true;
                gobackbutton.interactable = true;
                currentpages++;
                LoadActorGallery();
                UploadCGSStatus();
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
                LoadActorGallery();
                UploadCGSStatus();
            }
            else
            {
                gobackbutton.interactable = false;
                gofowardbutton.interactable = true;
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.2f; }
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
