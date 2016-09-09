using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameData gamedata;
    public GameObject affinitybarbox;
    public Scrollbar affinityscrollbar;
    public GameObject helpbox;
    public Actor[] actors;

    void OnEnable()
    {
        Debug.Log("game paused");
        gamedata.SetLoadRequest(7);
        gamedata.SaveLoadRequest();
        gamedata.SetSaveRequest(7);
        gamedata.SaveSaveRequest();
    }
    void OnDisable()
    {
        Debug.Log("game running");
    }
	public void SaveButton()
    {
        Application.LoadLevel(3);
    }
    public void LoadButton()
    {
        Application.LoadLevel(3);
    }
    public void ParametersButtons()
    {
        if (!affinitybarbox.activeInHierarchy)
        {
            foreach (Actor actor in actors)
                actor.gameObject.SetActive(false);
            affinitybarbox.SetActive(true);
            if (gamedata.playercurrentactor == "Enzo")
                affinityscrollbar.value = gamedata.currentenzoaffinity;
        }
        else {
            foreach (Actor actor in actors)
                if (actor.hasdialog)
                    actor.gameObject.SetActive(true);
            affinitybarbox.SetActive(false); }
    }
    public void OptionsButton()
    {
        Application.LoadLevel(6);
    }
    public void HelpButton()
    {
        if (!helpbox.activeInHierarchy)
        {
            foreach (Actor actor in actors)
                actor.gameObject.SetActive(false);
            helpbox.SetActive(true);
        }
         else {
            foreach (Actor actor in actors)
                if (actor.hasdialog)
                    actor.gameObject.SetActive(true);
            helpbox.SetActive(false);
        }
    }
    public void MenuButton()
    {
        if(!gamedata.issaving || !gamedata.isloading)
            Application.LoadLevel(2);
    }
    public void ReturnButton()
    {
        foreach (Actor actor in actors)
            if(actor.hasdialog)
                actor.gameObject.SetActive(true);
        affinitybarbox.SetActive(false);
        helpbox.SetActive(false);
        gameObject.SetActive(false);
    }
}
