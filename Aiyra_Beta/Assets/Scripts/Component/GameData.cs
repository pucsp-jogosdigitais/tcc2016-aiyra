using UnityEngine;
using System.Collections;
using System;

public class GameData : MonoBehaviour {

    #region Keys

    private const string loadrequestsavekey = "LOADREQUEST";
    private const string saverequestsavekey = "SAVEREQUEST";

    private const string playernamesavekey = "PLAYERNAME";
    private const string playercurrentinventorylengthsavekey = "PLAYERCURRENTINVENTORYLENGTH";
    public string playercurrentinventoryobjectsavekey;


    private const string playercurrentactorsavekey = "PLAYERCURRENTACTOR";
    private const string enzoaffinitysavekey = "ENZOAFFINITY";
    private const string isisaffinitysavekey = "ISISAFFINITY";
    private const string benjaminaffinitysavekey = "BENJAMINAFFINITY";
    private const string malikaaffinitysavekey = "MALIKAAFFINITY";
    private const string zakiaffinitysavekey = "ZAKIAFFINITY";

    private const string playercurrentscenesavekey = "PLAYERCURRENTSCENE";
    private const string playerlasttextfilesavekey = "PLAYERLASTTEXTFILE";
    private const string playercurrenttextfilesavekey = "PLAYERCURRENTTEXTFILE";
    private const string playercurrentdialoglinesavekey = "PLAYERCURRENTDIALOGLINE";
    private const string currentscenestatesavekey = "CURRENTSCENESTATE";
    private const string currentdialoganswerstatesavekey = "CURRENTDIALOGANSWERSTATE";

    private const string gameprogresssavekey = "CURRENTGAMEPROGRESS";
    private const string currentgameendsavekey = "CURRENTGAMEEND";
    private const string savedatasavekey = "DATA";
    private const string playtimesavekey = "PLAYTIME";

    #endregion

    #region Attributes
    public DateTime systemdata;

    public int loadrequest;
    public int saverequest;

    public bool isloading;
    public bool issaving;

    public string playername;
    public string[] playercurrentinventoryobjects;

    public string playercurrentactor;

    public int currentenzoaffinity;
    public int currentisisaffinity;
    public int currentbenjaminaffinity;
    public int currentmalikaaffinity;
    public int currentzakiaffinity;

    public int playercurrentscene;
    public int playerlasttextfile;
    public int playercurrenttextfile;
    public int playercurrentdialogline;
    public string currentscenestate;
    public string currentdialoganswerstate;

    public int currentgameend;
    public int gameprogress;

    public string data;
    public float playtime;

    #endregion

    #region Methods

    #region Set Values Methods

    #region Reset Methods

    public void ResetAffinitys()
    {
        currentenzoaffinity = 0;
        currentisisaffinity = 0;
        currentbenjaminaffinity = 0;
        currentmalikaaffinity = 0;
        currentzakiaffinity = 0;
    }
    public void ResetInventoryObjects()
    {
        playercurrentinventoryobjects = new string[0];
    }
    public void ResetDialogAnswerState()
    {
        currentdialoganswerstate = "False";
    }
    public void ResetPlaytime()
    {
        playtime = 0;
    }

    #endregion

    #region Set Methods

    public void SetLoadRequest(int value)
    {
        loadrequest = value;
    }
    public void SetSaveRequest(int value)
    {
        saverequest = value;
    }
    public void SetPlayerName(string PlayerName)
    {
        playername = PlayerName;
    }
    public void SetPlayerCurrentActor(string CurrentActor)
    {
        playercurrentactor = CurrentActor;
    }
    public void SetAffinityPoints(int CurrentEnzoAffinity, int CurrentIsisAffinity, int CurrentBenjaminAffinity, int CurrentMalikaAffinity, int CurrentZakiAffinity)
    {
        currentenzoaffinity = CurrentEnzoAffinity;
        currentisisaffinity = CurrentIsisAffinity;
        currentbenjaminaffinity = CurrentBenjaminAffinity;
        currentmalikaaffinity = CurrentMalikaAffinity;
        currentzakiaffinity = CurrentZakiAffinity;
    }
    public void SetLastDialog(int CurrentlastDialog)
    {
        playerlasttextfile = CurrentlastDialog;
    }
    public void SetGameData(int CurrentScene,int CurrentTextFile, int CurrentDialogLine)
    {
        playercurrentscene = CurrentScene;
        playercurrenttextfile = CurrentTextFile;
        playercurrentdialogline = CurrentDialogLine;
    }
    public void SetCurrentSceneState(string CurrentSceneState)
    {
        currentscenestate = CurrentSceneState;
    }
    public void SetCurrentDialogAnswerState(string CurrentDialogAnswerState)
    {
        currentdialoganswerstate = CurrentDialogAnswerState;
    }
    public void SetPlayerInventoryLength(int CurrentInventoryLength)
    {
        if (CurrentInventoryLength > playercurrentinventoryobjects.Length)
        {
            string[] tempinventory = playercurrentinventoryobjects;

            playercurrentinventoryobjects = new string[CurrentInventoryLength];
            for (int i = 0; i < tempinventory.Length; i++)
            {
                playercurrentinventoryobjects[i] = tempinventory[i];
            }
        }
        else { Debug.LogWarning("You set a value of length lower than the current player intentory, \n for you not loss any object reference that is in the inventory no method has not let you make the change"); }
    }
    public void SetGameEnd(int CurrentGameEnd)
    {
        currentgameend = CurrentGameEnd;
    }
    public void SetGameProgress(int CurrentGameProgress)
    {
        gameprogress = CurrentGameProgress;
    }
    public void SetData()
    {
        systemdata = DateTime.Now;
        data = systemdata.ToShortDateString();
    }
    public void SetData(DateTime CurrentData)
    {
        data = CurrentData.ToShortDateString();
    }
    public void SetPlayTime(float CurrentTime)
    {
        playtime = CurrentTime;
    }

    #endregion

    #endregion

    #region Save And Load Methods

    //Save Methods

    #region Save Methods
        //Method that save current load request
    public void SaveLoadRequest()
    {
        issaving = true;
        PlayerPrefs.SetInt(loadrequestsavekey, loadrequest);
        issaving = false;
    }
    //Method that save current save request
    public void SaveSaveRequest()
    {
        issaving = true;
        PlayerPrefs.SetInt(saverequestsavekey, saverequest);
        issaving = false;
    }
    //Method that save all player information like name, current actor or patner, affinity with wich actor, data and playtime.
    public void SaveAllPlayerData()
    {
        issaving = true;
        PlayerPrefs.SetString(playernamesavekey, playername);
        PlayerPrefs.SetInt(playercurrentinventorylengthsavekey, playercurrentinventoryobjects.Length);
        for (int i = 0; i < playercurrentinventoryobjects.Length; i++)
        {
            playercurrentinventoryobjectsavekey = "INVENTORYOBJECT" + i.ToString();
            PlayerPrefs.SetString(playercurrentinventoryobjectsavekey, playercurrentinventoryobjects[i]);
        }

        PlayerPrefs.SetString(playercurrentactorsavekey, playercurrentactor);

        PlayerPrefs.SetInt(enzoaffinitysavekey, currentenzoaffinity);
        PlayerPrefs.SetInt(isisaffinitysavekey, currentisisaffinity);
        PlayerPrefs.SetInt(benjaminaffinitysavekey, currentbenjaminaffinity);
        PlayerPrefs.SetInt(malikaaffinitysavekey, currentmalikaaffinity);
        PlayerPrefs.SetInt(zakiaffinitysavekey, currentzakiaffinity);

        PlayerPrefs.SetString(savedatasavekey, data);
        PlayerPrefs.SetFloat(playtimesavekey, playtime);
        issaving = false;
    }
    //Method that save all game information like current scene,text file,dialog line, scene state and current game progress.
    public void SaveAllGameData()
    {
        issaving = true;
        PlayerPrefs.SetInt(playercurrentscenesavekey, playercurrentscene);
        /* testing */PlayerPrefs.SetInt(playerlasttextfilesavekey, playerlasttextfile);
        PlayerPrefs.SetInt(playercurrenttextfilesavekey, playercurrenttextfile);
        PlayerPrefs.SetInt(playercurrentdialoglinesavekey, playercurrentdialogline);
        PlayerPrefs.SetString(currentscenestatesavekey, currentscenestate);
        PlayerPrefs.SetString(currentdialoganswerstate, currentdialoganswerstatesavekey);
        PlayerPrefs.SetInt(currentgameendsavekey, currentgameend);
        PlayerPrefs.SetInt(gameprogresssavekey, gameprogress);
        issaving = false;
    }
    //Method that save player name
    public void SavePlayerName()
    {
        issaving = true;
        PlayerPrefs.SetString(playernamesavekey, playername);
        issaving = false;
    }
    //Method that save player current actor or patner choice
    public void SavePlayerCurrentActor()
    {
        issaving = true;
        PlayerPrefs.SetString(playercurrentactorsavekey, playercurrentactor);
        issaving = false;
    }
    //Method that save affinity with all actors or partners
    public void SaveAllAffinitys()
    {
        issaving = true;
        PlayerPrefs.SetInt(enzoaffinitysavekey, currentenzoaffinity);
        PlayerPrefs.SetInt(isisaffinitysavekey, currentisisaffinity);
        PlayerPrefs.SetInt(benjaminaffinitysavekey, currentbenjaminaffinity);
        PlayerPrefs.SetInt(malikaaffinitysavekey, currentmalikaaffinity);
        PlayerPrefs.SetInt(zakiaffinitysavekey, currentzakiaffinity);
        issaving = false;
    }
    //Method that save data
    public void SaveData()
    {
        issaving = true;
        PlayerPrefs.SetString(savedatasavekey, data);
        issaving = false;
    }
    //Method that save playtime
    public void SavePlayTime()
    {
        issaving = true;
        PlayerPrefs.SetFloat(playtimesavekey, playtime);
        issaving = false;
    }
    #endregion

    //Load Methods

    #region Load Methods
        //Method that load load request to know it the game will gone save or load now
    public void LoadLoadRequest()
    {
        isloading = true;
        loadrequest = PlayerPrefs.GetInt(loadrequestsavekey);
        isloading = false;
    }
    //Method that load save request to know it the game will gone save or load now
    public void LoadSaveRequest()
    {
        isloading = true;
        saverequest = PlayerPrefs.GetInt(saverequestsavekey);
        isloading = false;
    }
    //Method that load all player data like player name, player current actor or patner, etc.
    public void LoadAllPlayerData()
    {
        isloading = true;
        playername = PlayerPrefs.GetString(playernamesavekey);
        playercurrentinventoryobjects = new string[PlayerPrefs.GetInt(playercurrentinventorylengthsavekey)];
        for (int i = 0; i < playercurrentinventoryobjects.Length; i++)
        {
            playercurrentinventoryobjectsavekey = "INVENTORYOBJECT" + i.ToString();
            playercurrentinventoryobjects[i] = PlayerPrefs.GetString(playercurrentinventoryobjectsavekey);
        }

        playercurrentactor = PlayerPrefs.GetString(playercurrentactorsavekey);

        currentenzoaffinity = PlayerPrefs.GetInt(enzoaffinitysavekey);
        currentisisaffinity = PlayerPrefs.GetInt(isisaffinitysavekey);
        currentbenjaminaffinity = PlayerPrefs.GetInt(benjaminaffinitysavekey);
        currentmalikaaffinity = PlayerPrefs.GetInt(malikaaffinitysavekey);
        currentzakiaffinity = PlayerPrefs.GetInt(zakiaffinitysavekey);

        data = PlayerPrefs.GetString(savedatasavekey);
        playtime = PlayerPrefs.GetFloat(playtimesavekey);
        isloading = false;
    }
    //Method that load all game data like current scene, current scene state, etc.
    public void LoadAllGameData()
    {
        isloading = true;
        playercurrentscene = PlayerPrefs.GetInt(playercurrentscenesavekey);
        /* testing*/playerlasttextfile = PlayerPrefs.GetInt(playerlasttextfilesavekey);
        playercurrenttextfile = PlayerPrefs.GetInt(playercurrenttextfilesavekey);
        playercurrentdialogline = PlayerPrefs.GetInt(playercurrentdialoglinesavekey);
        currentscenestate = PlayerPrefs.GetString(currentscenestatesavekey);
        currentdialoganswerstate = PlayerPrefs.GetString(currentdialoganswerstatesavekey);
        currentgameend = PlayerPrefs.GetInt(currentgameendsavekey);
        gameprogress = PlayerPrefs.GetInt(gameprogresssavekey);
        isloading = false;
    }

    #endregion

    #endregion

    #endregion

}
