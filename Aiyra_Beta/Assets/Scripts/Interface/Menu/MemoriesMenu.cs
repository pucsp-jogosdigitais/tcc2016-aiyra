using UnityEngine;
using System.Collections;

public class MemoriesMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;

    public int gamelevel;

    #endregion

    #region Methods

    #region Enable and Disable Methods

    void OnEnable()
    {
        Debug.Log("Memories Menu Active");
    }
    void OnDisable()
    {
        Debug.Log("Memories Menu Desactive");
    }

    #endregion

    #region Awake And Start Methods

    void Awake()
    {
        if (gamedata == null)
            gamedata = GameObject.Find("GameData").GetComponent<GameData>();
    }
    void Start()
    {
        if (gamedata != null)
        {
            gamedata.LoadAllPlayerData();
            gamedata.LoadAllGameData();
        }
    }

    #endregion

    #region Buttons Methods
    public void EnzoMemorieButton()
    {
        gamedata.playercurrentscene = 1;
        gamedata.currentscenestate = "dialog";
        gamedata.playercurrenttextfile = 0;
        gamedata.playercurrentdialogline = 0;

        gamedata.SaveAllGameData();
        Application.LoadLevel(gamelevel);
    }
    public void IsisMemorieButton()
    {
        gamedata.playercurrentscene = 2;
        gamedata.currentscenestate = "dialog";
        gamedata.playercurrenttextfile = 0;
        gamedata.playercurrentdialogline = 0;

        gamedata.SaveAllGameData();
        Application.LoadLevel(gamelevel);
    }
    public void BenjaminMemorieButton()
    {
        gamedata.playercurrentscene = 3;
        gamedata.currentscenestate = "dialog";
        gamedata.playercurrenttextfile = 0;
        gamedata.playercurrentdialogline = 0;

        gamedata.SaveAllGameData();
        Application.LoadLevel(gamelevel);
    }
    public void MalikaMemorieButton()
    {
        gamedata.playercurrentscene = 4;
        gamedata.currentscenestate = "dialog";
        gamedata.playercurrenttextfile = 0;
        gamedata.playercurrentdialogline = 0;

        gamedata.SaveAllGameData();
        Application.LoadLevel(gamelevel);
    }
    public void ZakiMemorieButton()
    {
        gamedata.playercurrentscene = 5;
        gamedata.currentscenestate = "dialog";
        gamedata.playercurrenttextfile = 0;
        gamedata.playercurrentdialogline = 0;

        gamedata.SaveAllGameData();
        Application.LoadLevel(gamelevel);
    }
    public void ReturnButton()
    {
        Application.LoadLevel(2);
    }

    #endregion

    #endregion
}
