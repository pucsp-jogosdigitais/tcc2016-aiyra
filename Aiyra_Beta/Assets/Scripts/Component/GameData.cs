using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    private const string loadrequestsavekey = "LOADREQUEST";
    private const string saverequestsavekey = "SAVEREQUEST";

    private const string playernamesavekey = "PLAYERNAME";

    private const string playercurrentscenesavekey = "PLAYERCURRENTSCENE";
    private const string playercurrenttextfilesavekey = "PLAYERCURRENTTEXTFILE";
    private const string playercurrentdialoglinesavekey = "PLAYERCURRENTDIALOGLINE";

    private const string gameprogresssavekey = "CURRENTGAMEPROGRESS";
    private const string playtimesavekey = "PLAYTIME";

    private const string saveslot1savekey = "SAVESLOT1";
    private const string saveslot2savekey = "SAVESLOT2";

    public int loadrequest;
    public int saverequest;

    public bool isloading;
    public bool issaving;

    public string playername;

    public int playercurrentscene;
    public int playercurrenttextfile;
    public int playercurrentdialogline;

    public int gameprogress;
    public int playtime;

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
    public void SetGameData(int CurrentScene, int CurrentTextFile, int CurrentDialogLine)
    {
        playercurrentscene = CurrentScene;
        playercurrenttextfile = CurrentTextFile;
        playercurrentdialogline = CurrentDialogLine;
    }
    public void SetGameProgress(int CurrentGameProgress)
    {
        gameprogress = CurrentGameProgress;
    }
    public void SetPlayTime(int CurrentTime)
    {
        playtime = CurrentTime;
    }
    //Save Methods
    public void SaveLoadRequest()
    {
        issaving = true;
        PlayerPrefs.SetInt(loadrequestsavekey, loadrequest);
        issaving = false;
    }
    public void SaveSaveRequest()
    {
        issaving = true;
        PlayerPrefs.SetInt(saverequestsavekey, saverequest);
        issaving = false;
    }
    public void SaveAllPlayerData()
    {
        issaving = true;
        PlayerPrefs.SetString(playernamesavekey, playername);
        PlayerPrefs.SetInt(playtimesavekey, playtime);
        issaving = false;
    }
    public void SaveAllGameData()
    {
        issaving = true;
        PlayerPrefs.SetInt(playercurrentscenesavekey, playercurrentscene);
        PlayerPrefs.SetInt(playercurrenttextfilesavekey, playercurrenttextfile);
        PlayerPrefs.SetInt(playercurrentdialoglinesavekey, playercurrentdialogline);
        PlayerPrefs.SetInt(gameprogresssavekey, gameprogress);
        issaving = false;
    }
    public void SavePlayerName()
    {
        issaving = true;
        PlayerPrefs.SetString(playernamesavekey, playername);
        issaving = false;
    }

   //Load Methods
    public void LoadLoadRequest()
    {
        isloading = true;
        loadrequest = PlayerPrefs.GetInt(loadrequestsavekey);
        isloading = false;
    }
    public void LoadSaveRequest()
    {
        isloading = true;
        loadrequest = PlayerPrefs.GetInt(saverequestsavekey);
        isloading = false;
    }
    public void LoadAllPlayerData()
    {
        isloading = true;
        playername = PlayerPrefs.GetString(playernamesavekey);
        playtime = PlayerPrefs.GetInt(playtimesavekey);
        isloading = false;
    }
    public void LoadAllGameData()
    {
        isloading = true;
        playercurrentscene = PlayerPrefs.GetInt(playercurrentscenesavekey);
        playercurrenttextfile = PlayerPrefs.GetInt(playercurrenttextfilesavekey);
        playercurrentdialogline = PlayerPrefs.GetInt(playercurrentdialoglinesavekey);
        gameprogress = PlayerPrefs.GetInt(gameprogresssavekey);
        isloading = false;
    }

}
