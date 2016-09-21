using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public DisplayCGMenu displaycgmenu;
    public ActorButton[] actorbuttons;
    public ActorCG[] actorcgs;

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
    
    #region Buttons Methods

    public void LoadActorCGs(ActorButton ActorButton)
    {
        if (ActorButton.actor.actorname == "Enzo")
        {
            for (int i = 0; i < actorcgs.Length; i++)
            {
                actorcgs[i].SetCGID(i);
                actorcgs[i].SetCGPath("Backgrounds/Teste/");
                if (i == 0)
                    actorcgs[i].isunlock = false;
                //actorcgs[i].SetCGName("MainMenu");
                else
                {
                    actorcgs[i].SetCGName("JardimTeste");
                }
            }
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
    public void ReturnButton()
    {
        gameObject.SetActive(false);
        albummenu.SetActive(true);
    }

    #endregion

    #endregion
    
}
