using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameData gamedata;
    public Animator cameraanimator;

    public float lastclicktime;
    public float clicktime;

    public bool canclick;

    void Awake()
    {
        if (cameraanimator == null)
        {
            cameraanimator = GameObject.Find("Main Menu Main Camera").GetComponent<Animator>();
        }
        canclick = true;
    }
    void Start()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();
    }
    bool CheckTimeOfAnimator()
    {
        clicktime = Time.time;
        if (clicktime > lastclicktime + 1.5f)
            return true;

        return false;
    }
    public void NewGameButton()
    {
        if (canclick)
        {
            Debug.Log("StartNewGame");
            gamedata.SetLoadRequest(-1);
            gamedata.SaveLoadRequest();
            gamedata.SetSaveRequest(-1);
            gamedata.SaveSaveRequest();

            gamedata.SetPlayerName("Luna");
            gamedata.SetPlayerCurrentActor("");
            gamedata.SetAffinityPoints(0, 0, 0, 0, 0);
            gamedata.SetPlayTime(gamedata.playtime);
            gamedata.SaveAllPlayerData();

            gamedata.playercurrentscene = 0;
            gamedata.playercurrenttextfile = 0;
            gamedata.playercurrentdialogline = 0;
            gamedata.SaveAllGameData();
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
}
