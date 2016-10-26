using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingGameMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public SaveSlot[] savesslots;

    #endregion

    #region Methods

    #region Awake and Start

    void Start()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();

        UpdateSaveSlotsID();
    }

    #endregion

    #region Buttons Methods

    public void ReturnButton()
    {
        if (gamedata.loadrequest > 0 || gamedata.saverequest > 0)
            Application.LoadLevel(7);
        else { Application.LoadLevel(2); }
    }

    #endregion

    #region LoadingGameMenu Fundamental Methods

    public void UpdateSaveSlotsID()
    {
        for (int i = 0; i < savesslots.Length; i++)
            savesslots[i].saveslotid = i;
    }

    #endregion

    #endregion
}
