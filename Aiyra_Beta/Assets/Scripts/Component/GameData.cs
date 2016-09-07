using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    private const string loadrequestsavekey = "LOADREQUEST";
    private const string saverequestsavekey = "SAVEREQUEST";

    private const string playernamesavekey = "PLAYERNAME";

    private const string playercurrentactorsavekey = "PLAYERCURRENTACTOR";
    private const string enzoaffinitysavekey = "ENZOAFFINITY";
    private const string isisaffinitysavekey = "ISISAFFINITY";
    private const string benjaminaffinitysavekey = "BENJAMINAFFINITY";
    private const string malikaaffinitysavekey = "MALIKAAFFINITY";
    private const string zakiaffinitysavekey = "ZAKIAFFINITY";

    private const string playercurrentscenesavekey = "PLAYERCURRENTSCENE";
    private const string playercurrenttextfilesavekey = "PLAYERCURRENTTEXTFILE";
    private const string playercurrentdialoglinesavekey = "PLAYERCURRENTDIALOGLINE";

    private const string gameprogresssavekey = "CURRENTGAMEPROGRESS";
    private const string playtimesavekey = "PLAYTIME";

    private const string saveslot0chaptertitlesavekey = "SAVESLOT0CHAPTERTITLE";
    private const string saveslot0datasavekey = "SAVESLOT0DATA";
    private const string saveslot0timesavekey = "SAVESLOT0TIME";
    private const string saveslot0actorsavekey = "SAVESLOT0ACTOR";

    public int loadrequest;
    public int saverequest;

    public bool isloading;
    public bool issaving;

    public string playername;

    public string playercurrentactor;

    public int currentenzoaffinity;
    public int currentisisaffinity;
    public int currentbenjaminaffinity;
    public int currentmalikaaffinity;
    public int currentzakiaffinity;

    public int playercurrentscene;
    public int playercurrenttextfile;
    public int playercurrentdialogline;

    public int gameprogress;
    public int playtime;

    public string saveslot0chaptertitle;
    public string saveslot0data;
    public float saveslot0time;
    public Actor saveslot0actor;

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
        PlayerPrefs.SetString(playercurrentactorsavekey, playercurrentactor);

        PlayerPrefs.SetInt(enzoaffinitysavekey, currentenzoaffinity);
        PlayerPrefs.SetInt(isisaffinitysavekey, currentisisaffinity);
        PlayerPrefs.SetInt(benjaminaffinitysavekey, currentbenjaminaffinity);
        PlayerPrefs.SetInt(malikaaffinitysavekey, currentmalikaaffinity);
        PlayerPrefs.SetInt(zakiaffinitysavekey, currentzakiaffinity);

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
        playercurrentactor = PlayerPrefs.GetString(playercurrentactorsavekey);

        currentenzoaffinity = PlayerPrefs.GetInt(enzoaffinitysavekey);
        currentisisaffinity = PlayerPrefs.GetInt(isisaffinitysavekey);
        currentbenjaminaffinity = PlayerPrefs.GetInt(benjaminaffinitysavekey);
        currentmalikaaffinity = PlayerPrefs.GetInt(malikaaffinitysavekey);
        currentzakiaffinity = PlayerPrefs.GetInt(zakiaffinitysavekey);

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
