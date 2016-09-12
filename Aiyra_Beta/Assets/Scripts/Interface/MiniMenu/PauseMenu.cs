using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameData gamedata;
    public GameController gamecontroller;
    public GameObject affinitybarbox;
    public Slider affinityslider;
    public RectTransform filltransform;
    public RectTransform handletransform;
    public GameObject helpbox;


    public bool hideactors;
    void Awake()
    {

    }
    void Start()
    {
        if (affinitybarbox.activeInHierarchy || helpbox.activeInHierarchy)
        {
            affinitybarbox.SetActive(false);
            helpbox.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        Debug.Log("game paused");
        if (gamedata.loadrequest < 0)
        {
            gamedata.SetLoadRequest(7);
            gamedata.SaveLoadRequest();
        }
        if (gamedata.saverequest < 0)
        {
            gamedata.SetSaveRequest(7);
            gamedata.SaveSaveRequest();
        }
    }
    void OnDisable()
    {
        Debug.Log("game running");
    }
    #region PauseButton
    public void OnClickPauseGame()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
    #endregion
    public void SaveButton()
    {
        gamedata.SetSaveRequest(7);
        gamedata.SaveSaveRequest();
        gamedata.SetLoadRequest(-1);
        gamedata.SaveLoadRequest();
        Application.LoadLevel(3);
    }
    public void LoadButton()
    {
        gamedata.SetLoadRequest(7);
        gamedata.SaveLoadRequest();
        gamedata.SetSaveRequest(-1);
        gamedata.SaveSaveRequest();
        Application.LoadLevel(3);
    }
    public void ParametersButtons()
    {
        if (!affinitybarbox.activeInHierarchy)
        {
            hideactors = true;
            affinitybarbox.SetActive(true);
            if (gamedata.playercurrentactor == "Enzo")
                affinityslider.value = gamedata.currentenzoaffinity;
        }
        else {
            hideactors = false;
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
            hideactors = true;
            helpbox.SetActive(true);
        }
         else {
            hideactors = false;
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
        hideactors = false;
        affinitybarbox.SetActive(false);
        helpbox.SetActive(false);
        gameObject.SetActive(false);
    }
}
