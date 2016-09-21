using UnityEngine;
using System.Collections;

public class DiaryMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public DisplayCGMenu displaycgmenu;
    public ActorButton[] actorbuttons;
    public ActorCG[] actordiaries;

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
    public void LoadActorDiary(ActorButton ActorButton)
    {
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
