using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingGameMenu : MonoBehaviour {

    public GameData gamedata;
    public SaveSlot[] savesslots;

    void Start()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();

        UpdateSaveSlotsID();
    }
    public void ReturnButton()
    {
        if (gamedata.loadrequest > 0 || gamedata.saverequest > 0)
            Application.LoadLevel(7);
        else { Application.LoadLevel(2); }
    }
    public void UpdateSaveSlotsID()
    {
        for (int i = 0; i < savesslots.Length; i++)
            savesslots[i].saveslotid = i;
    }
}
