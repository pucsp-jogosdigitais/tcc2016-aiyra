using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSlot : MonoBehaviour {

    #region Keys
    public string saveslotchaptersavekey;
    public string saveslotactorsavekey;
    public string saveslotdatasavekey;
    public string saveslottimesavekey;

    public string saveslotplayernamesavekey;
    public string saveslotplayeractoraffinitysavekey;

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

    public string saveslotplayername;
    public int saveslotplayeractoraffinity;

    public int saveslotcurrentscene;
    public int saveslotcurrenttextfile;
    public int saveslotcurrentdialogline;

    public float saveslottime;

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

        saveslotplayernamesavekey = "SAVESLOT" + saveslotid.ToString() + "PLAYERNAME";
        saveslotplayeractoraffinitysavekey = "SAVESLOT" + saveslotid.ToString() + "ACTORAFFINITY";

        saveslotcurrentscenesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTSCENE";
        saveslotcurrentdialogsavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOG";
        saveslotcurrentdialoglinesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOGLINE";
    }
    void UploadSaveSlotInformation()
    {
        SaveSlotLoadData();
    }

    #endregion

    #region Save And Load Methods

    #region Save Methods

    void SaveSlotSaveData()
    {
        PlayerPrefs.SetString(saveslotchaptersavekey, saveslotchaptertext.text);
        PlayerPrefs.SetString(saveslotactorsavekey, saveslotactorimage.name);
        PlayerPrefs.SetString(saveslotdatasavekey, saveslotdatatext.text);
        PlayerPrefs.SetFloat(saveslottimesavekey, saveslottime);

        PlayerPrefs.SetString(saveslotplayernamesavekey, saveslotplayername);
        PlayerPrefs.SetInt(saveslotplayeractoraffinitysavekey, saveslotplayeractoraffinity);

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
            saveslotactorimage.sprite = Resources.Load<Actor>("Prefabs/Actor/Enzo").actorimage;
        saveslotdatatext.text = PlayerPrefs.GetString(saveslotdatasavekey);
        saveslottime = PlayerPrefs.GetFloat(saveslottimesavekey);
        saveslottimetext.text = saveslottime.ToString();

        saveslotplayername = PlayerPrefs.GetString(saveslotplayernamesavekey);
        saveslotplayeractoraffinity = PlayerPrefs.GetInt(saveslotplayeractoraffinitysavekey);

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

    #endregion

    #region SaveSlot OnClick Methods

    public void SaveSlotClick()
    {
        #region Case Save

        if (gamedata.saverequest >= 0 && gamedata.loadrequest < 0)
        {

            if (gamedata.playercurrentscene < 7)
            {
                saveslotchaptertext.text = "Prologo";
            }

            saveslotactorimage.name = gamedata.playercurrentactor;

            if(gamedata.playercurrentactor == "Enzo")
                saveslotactorimage.sprite = Resources.Load<Actor>("Prefabs/Actor/Enzo").actorimage;

            saveslotdatatext.text = gamedata.playtime.ToString();

            saveslottime = gamedata.playtime;
            saveslottimetext.text = saveslottime.ToString();

            saveslotplayername = gamedata.playername;

            if (gamedata.playercurrentactor == "Enzo")
            {
                saveslotplayeractoraffinity = gamedata.currentenzoaffinity;
            }
            if (gamedata.playercurrentactor == "Isis")
            {
                saveslotplayeractoraffinity = gamedata.currentisisaffinity;
            }
            if (gamedata.playercurrentactor == "Benjamin")
            {
                saveslotplayeractoraffinity = gamedata.currentbenjaminaffinity;
            }
            if (gamedata.playercurrentactor == "Malika")
            {
                saveslotplayeractoraffinity = gamedata.currentmalikaaffinity;
            }
            if (gamedata.playercurrentactor == "Zaki")
            {
                saveslotplayeractoraffinity = gamedata.currentzakiaffinity;
            }

            saveslotcurrentscene = gamedata.playercurrentscene;

            saveslotcurrenttextfile = gamedata.playercurrenttextfile;

            saveslotcurrentdialogline = gamedata.playercurrentdialogline;

            SaveSlotSaveData();

        }

        #endregion

        #region Case Load

        if (gamedata.loadrequest >= 0 || gamedata.loadrequest < 0 && gamedata.saverequest < 0)
        {
            gamedata.playercurrentscene = saveslotcurrentscene;

            gamedata.playercurrentactor = saveslotactorimage.name;

            // data has make in gamedata gamedata.data

            gamedata.playtime += saveslottime;

            gamedata.playername = saveslotplayername;

            if(saveslotactorimage.name == "Enzo")
            {
                gamedata.currentenzoaffinity = saveslotplayeractoraffinity;
            }
            if (saveslotactorimage.name == "Isis")
            {
                gamedata.currentisisaffinity = saveslotplayeractoraffinity;
            }
            if (saveslotactorimage.name == "Benjamin")
            {
                gamedata.currentbenjaminaffinity = saveslotplayeractoraffinity;
            }
            if (saveslotactorimage.name == "Malika")
            {
                gamedata.currentmalikaaffinity = saveslotplayeractoraffinity;
            }
            if (saveslotactorimage.name == "Zaki")
            {
                gamedata.currentzakiaffinity = saveslotplayeractoraffinity;
            }

            gamedata.playercurrenttextfile = saveslotcurrenttextfile;

            gamedata.playercurrentdialogline = saveslotcurrentdialogline;

            gamedata.SaveAllPlayerData();

            gamedata.SaveAllGameData();

            Application.LoadLevel(7);
        }

        #endregion
    }
    /*
    public void SaveSlotClick()
    {
        //If the gamedata is requesting more than 6 slots to save, the saveslot will work as a saveslot to save
        //if gamedata is requesting more than 6 slots to load, the saveslot will work as load saveslot and will
        //load the game. 
        if (gamedata.saverequest >= 0)
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
        if(gamedata.loadrequest >= 0)
        {
            if (saveslotactorimage.name == "Enzo" /* || == "Zaki" , etc. )
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
    */
    #endregion

    #endregion

}
