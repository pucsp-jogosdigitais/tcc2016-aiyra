using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingGameMenu : MonoBehaviour {

    public GameData gamedata;
    public GameObject confirmbox;
    public SaveSlot[] savesslots;
    public SaveSlot currentsaveslot;
    
    void Awake()
    {
        if(gamedata.loadrequest != 0) { }

    }
    void LoadSaveSlotsData()
    {
    }
    public void SaveSlotButton(SaveSlot SaveSlot)
    {
        currentsaveslot = SaveSlot;
    }
    public void ReturnButton()
    {
        if (gamedata.loadrequest == 0)
            Application.LoadLevel(2);
    }
    public void ConfirmButton()
    {
        //currentsaveslot
    }
    public void CancelButton()
    {
        confirmbox.SetActive(false);
    }
}
