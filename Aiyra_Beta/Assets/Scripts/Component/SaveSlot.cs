using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSlot : MonoBehaviour {

    #region Keys
    public string saveslotchaptersavekey;
    public string saveslotactorsavekey;
    public string saveslotdatasavekey;
    public string saveslottimesavekey;

    public string saveslotcurrentscenesavekey;
    public string saveslotcurrentdialogsavekey;
    public string saveslotcurrentdialoglinesavekey;
    #endregion

    #region Attributes
    public GameData gamedata;
 
    public Text saveslotchaptertext;
    public Image saveslotactorimage;
    public Text saveslotdatatext;
    public Text saveslottimetext;
    public float saveslottime;

    public int saveslotcurrentscene;
    public int saveslotcurrenttextfile;
    public int saveslotcurrentdialogline;

    public int saveslotid;
    #endregion

    #region Methods
    void Start()
    {
        //Prepare saveslot for use and display it information.
        UploadSaveSlotKeys();
        UploadSaveSlotInformation();
    }
    #region UploadSaveSlotKeys and Display Elements Methods
    void UploadSaveSlotKeys()
    {
        saveslotchaptersavekey = "SAVESLOT" + saveslotid.ToString() + "CHAPTER";
        saveslotactorsavekey = "SAVESLOT" + saveslotid.ToString() + "ACTOR";
        saveslotdatasavekey = "SAVESLOT" + saveslotid.ToString() + "DATA";
        saveslottimesavekey = "SAVESLOT" + saveslotid.ToString() + "TIME";

        saveslotcurrentscenesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTSCENE";
        saveslotcurrentdialogsavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOG";
        saveslotcurrentdialoglinesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOGLINE";
    }
    void UploadSaveSlotInformation()
    {
        //SaveSlotLoadData();
    }
    #endregion
    #region Save Methods
    void SaveSlotSaveData()
    {
        PlayerPrefs.SetString(saveslotchaptersavekey, saveslotchaptertext.text);
        PlayerPrefs.SetString(saveslotactorsavekey, saveslotactorimage.name);
        PlayerPrefs.SetString(saveslotdatasavekey, saveslotdatatext.text);
        PlayerPrefs.SetFloat(saveslottimesavekey, saveslottime);

        PlayerPrefs.SetInt(saveslotcurrentscenesavekey, saveslotcurrentscene);
        PlayerPrefs.SetInt(saveslotcurrentdialogsavekey, saveslotcurrenttextfile);
        PlayerPrefs.SetInt(saveslotcurrentdialoglinesavekey, saveslotcurrentdialogline);
    }
    #endregion
    #region Load Methods
    void SaveSlotLoadData()
    {
        saveslotchaptertext.text = PlayerPrefs.GetString(saveslotchaptersavekey);
        saveslotactorimage.name = PlayerPrefs.GetString(saveslotactorsavekey);
        if (saveslotactorimage.name == "Enzo")
            saveslotactorimage.sprite = Resources.Load<Actor>("Resources/Prefabs/Actor/Enzo.prefab").actorimage;
        saveslotdatatext.text = PlayerPrefs.GetString(saveslotdatasavekey);
        saveslottime = PlayerPrefs.GetFloat(saveslottimesavekey);
        saveslottimetext.text = saveslottime.ToString();

        saveslotcurrentscene = PlayerPrefs.GetInt(saveslotcurrentscenesavekey);
        saveslotcurrenttextfile = PlayerPrefs.GetInt(saveslotcurrentdialogsavekey);
        saveslotcurrentdialogline = PlayerPrefs.GetInt(saveslotcurrentdialoglinesavekey);
    }
    void LoadDataToGameData()
    {
        gamedata.isloading = true;
        gamedata.playercurrentactor = saveslotactorimage.name;
        gamedata.playercurrentscene = saveslotcurrentscene;
        gamedata.playercurrenttextfile = saveslotcurrenttextfile;
        gamedata.playercurrentdialogline = saveslotcurrentdialogline;
        gamedata.playtime = saveslottime;
        gamedata.isloading = false;
    }
    #endregion
    #region SaveSlot OnClick Methods
    public void SaveSlotClick()
    {
        //If the gamedata is requesting more than 6 slots to save, the saveslot will work as a saveslot to save
        //if gamedata is requesting more than 6 slots to load, the saveslot will work as load saveslot and will
        //load the game. 
        if (gamedata.saverequest > 6)
        {
            //put the values of gamedata into the saveslot and save it has a saveslot variable.
            if(gamedata.playercurrentscene < 7)
                saveslotchaptertext.text = "Prologo";
            if (gamedata.playercurrentactor == "Enzo")
            {
                saveslotactorimage.sprite = Resources.Load<Actor>("Resources/Prefabs/Actor/Enzo.prefab").actorimage;
            }
            saveslotactorimage.name = gamedata.playercurrentactor;
            saveslotdatatext.text = gamedata.playtime.ToString();
            saveslottimetext.text = saveslottime.ToString();


            saveslotcurrentscene = gamedata.playercurrentscene;
            saveslotcurrenttextfile = gamedata.playercurrenttextfile;
            saveslotcurrentdialogline = gamedata.playercurrentdialogline;

            saveslottime = gamedata.playtime;

            SaveSlotSaveData();
        }
        if(gamedata.loadrequest > 6)
        {
            if (saveslotactorimage.name == "Enzo" /* || == "Zaki" , etc. */)
            {
                gamedata.playercurrentactor = saveslotactorimage.name;

                gamedata.playercurrentscene = saveslotcurrentscene;
                gamedata.playercurrenttextfile = saveslotcurrenttextfile;
                gamedata.playercurrentdialogline = saveslotcurrentdialogline;

                gamedata.playtime = gamedata.playtime + saveslottime;

                gamedata.SaveAllPlayerData();
                gamedata.SaveAllGameData();
                Debug.Log("Load game");
                //Application.LoadLevel(7);
            }
        }
    }
    #endregion
    #endregion
}
