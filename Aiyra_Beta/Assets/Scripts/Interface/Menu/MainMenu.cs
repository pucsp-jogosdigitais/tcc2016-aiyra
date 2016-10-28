using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public LoadingInterface loadinginterface;
    public Animator cameraanimator;

    public float lastclicktime;
    public float clicktime;

    public bool canclick;

    #endregion

    #region Methods

    #region Awake And Start
    //Method that run when the gameobject is awake get eny component that is necessary to execute of the script
    void Awake()
    {
        if (cameraanimator == null)
        {
            cameraanimator = GameObject.Find("Main Menu Main Camera").GetComponent<Animator>();
        }
        canclick = true;
    }
    //Method that run when the first frame with gameobject active 
    void Start()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();
    }

    #endregion

    #region Update Methods
    //Method that update the main menu related scripts
    void Update()
    {
        if(loadinginterface != null)
            loadinginterface.UploadLoadingMessage();
    }

    #endregion

    #region Check Methods

    bool CheckTimeOfAnimator()
    {
        clicktime = Time.time;
        if (clicktime > lastclicktime + 1.5f)
            return true;

        return false;
    }

    #endregion

    //Methods for buttons that is related with the Main Menu
    #region Buttons Methods

    public void NewGameButton()
    {
        if (canclick)
        {
            Debug.Log("StartNewGame");
            gamedata.SetLoadRequest(-1);
            gamedata.SaveLoadRequest();
            gamedata.SetSaveRequest(-1);
            gamedata.SaveSaveRequest();

            gamedata.SetPlayerName("");
            gamedata.SetPlayerCurrentActor("");
            gamedata.ResetAffinitys();
            gamedata.ResetInventoryObjects();
            gamedata.SetPlayTime(gamedata.playtime);
            gamedata.SaveAllPlayerData();

            gamedata.playercurrentscene = 0;
            gamedata.playercurrenttextfile = 0;
            gamedata.playercurrentdialogline = 0;
            gamedata.currentscenestate = Scene.state.dialog.ToString();
            gamedata.SaveAllGameData();

            loadinginterface.gameObject.SetActive(true);

            Application.LoadLevel(7);
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void LoadGameButton()
    {
        if (canclick)
        {
            Debug.Log("LoadGame");
            gamedata.SetLoadRequest(-1);
            gamedata.SaveLoadRequest();
            gamedata.SetSaveRequest(-1);
            gamedata.SaveSaveRequest();
            Application.LoadLevel(3);
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void AlbumButton()
    {
        Debug.Log("Album CLicked");
        if (canclick)
        {
            Application.LoadLevel(4);
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void MemoirsButton()
    {
        if (canclick)
        {
            Application.LoadLevel(5);
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void OptionsButton()
    {
        if (canclick)
        {
            gamedata.SetLoadRequest(-1);
            gamedata.SaveLoadRequest();
            gamedata.SetSaveRequest(-1);
            gamedata.SaveSaveRequest();
            Application.LoadLevel(6);
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void CreditsButton()
    {
        if (canclick)
        {
            cameraanimator.SetBool("Credits", true);
            canclick = false;
            lastclicktime = Time.time;
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    public void CreditsReturnButton()
    {
        if (canclick)
        {
            cameraanimator.SetBool("Credits", false);
            canclick = false;
            lastclicktime = Time.time;
        }
        else
        {
            canclick = CheckTimeOfAnimator();
        }
    }
    #endregion

    #endregion
}
