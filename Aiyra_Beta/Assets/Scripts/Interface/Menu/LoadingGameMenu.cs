using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingGameMenu : MonoBehaviour {

    public GameData gamedata;
    public SaveSlot[] savesslots;

    public void ReturnButton()
    {
        if (gamedata.loadrequest > 6 || gamedata.saverequest > 6)
            Application.LoadLevel(7);
        else { Application.LoadLevel(2); }
    }
}
