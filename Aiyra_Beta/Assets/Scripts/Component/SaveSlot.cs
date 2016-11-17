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
    public string saveslotcurrentlastdialogsavekey;
    public string saveslotcurrentdialogsavekey;
    public string saveslotcurrentdialoglinesavekey;
    public string saveslotcurrentscenestatesavekey;
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
    public int saveslotcurrentlasttextfile;
    public int saveslotcurrenttextfile;
    public int saveslotcurrentdialogline;
    public string saveslotcurrentscenestate;

    public float saveslottime;

    public int saveslotid;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Save Slot " + saveslotid + " Active and Enable");
    }

    void OnDisable()
    {
        Debug.Log("Save Slot " + saveslotid + " Disable");
    }

    #endregion

    #region Awake And Start Methods

    void Start()
    {
        //Prepare saveslot for use and display it information.
        UploadSaveSlotKeys();
        UploadSaveSlotInformation();
    }

    #endregion

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
        saveslotcurrentlastdialogsavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTLASTDIALOG";
        saveslotcurrentdialogsavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOG";
        saveslotcurrentdialoglinesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTDIALOGLINE";
        saveslotcurrentscenestatesavekey = "SAVESLOT" + saveslotid.ToString() + "CURRENTSCENESTATE";
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
        if (saveslotactorimage.name == "Enzo")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Enzo/Enzo_Save");
        if (saveslotactorimage.name == "Benjamin")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Benjamin/Ben_Save");
        if (saveslotactorimage.name == "Isis")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Isis/Isis_Save");
            #region Obsolety
            //saveslotactorimage.sprite = Resources.Load<Actor>("Prefabs/Actor/Enzo").actorimage;
            #endregion
        PlayerPrefs.SetString(saveslotdatasavekey, saveslotdatatext.text);
        saveslottime += gamedata.playtime;
        PlayerPrefs.SetFloat(saveslottimesavekey, saveslottime);

        PlayerPrefs.SetString(saveslotplayernamesavekey, saveslotplayername);
        PlayerPrefs.SetInt(saveslotplayeractoraffinitysavekey, saveslotplayeractoraffinity);

        PlayerPrefs.SetInt(saveslotcurrentscenesavekey, saveslotcurrentscene);
        PlayerPrefs.SetInt(saveslotcurrentlastdialogsavekey, saveslotcurrentlasttextfile);
        PlayerPrefs.SetInt(saveslotcurrentdialogsavekey, saveslotcurrenttextfile);
        PlayerPrefs.SetInt(saveslotcurrentdialoglinesavekey, saveslotcurrentdialogline);
        PlayerPrefs.SetString(saveslotcurrentscenestatesavekey, saveslotcurrentscenestate);
    }

    #endregion

    #region Load Methods

    void SaveSlotLoadData()
    {
        saveslotchaptertext.text = PlayerPrefs.GetString(saveslotchaptersavekey);
        saveslotactorimage.name = PlayerPrefs.GetString(saveslotactorsavekey);
        if (saveslotactorimage.name == "Enzo")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Enzo/Enzo_Save");
        if (saveslotactorimage.name == "Benjamin")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Benjamin/Ben_Save");
        if (saveslotactorimage.name == "Isis")
            saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Isis/Isis_Save");
        #region Obsolety
        //saveslotactorimage.sprite = Resources.Load<Actor>("Prefabs/Actor/Enzo").actorimage;
        #endregion
        saveslotdatatext.text = PlayerPrefs.GetString(saveslotdatasavekey);
        saveslottime = PlayerPrefs.GetFloat(saveslottimesavekey);
        saveslottimetext.text = saveslottime.ToString();

        saveslotplayername = PlayerPrefs.GetString(saveslotplayernamesavekey);
        saveslotplayeractoraffinity = PlayerPrefs.GetInt(saveslotplayeractoraffinitysavekey);

        saveslotcurrentscene = PlayerPrefs.GetInt(saveslotcurrentscenesavekey);
        saveslotcurrentlasttextfile = PlayerPrefs.GetInt(saveslotcurrentlastdialogsavekey);
        saveslotcurrenttextfile = PlayerPrefs.GetInt(saveslotcurrentdialogsavekey);
        saveslotcurrentdialogline = PlayerPrefs.GetInt(saveslotcurrentdialoglinesavekey);
        saveslotcurrentscenestate = PlayerPrefs.GetString(saveslotcurrentscenestatesavekey);
    }
    void LoadDataToGameData()
    {
        gamedata.isloading = true;
        gamedata.playername = saveslotplayername;
        gamedata.playercurrentactor = saveslotactorimage.name;
        if (saveslotactorimage.name == "Enzo")
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

        gamedata.playercurrentscene = saveslotcurrentscene;
        gamedata.playerlasttextfile = saveslotcurrentlasttextfile;
        gamedata.playercurrenttextfile = saveslotcurrenttextfile;
        gamedata.playercurrentdialogline = saveslotcurrentdialogline;
        gamedata.currentscenestate = saveslotcurrentscenestate;

        gamedata.SetData();
        //gamedata.data = saveslotdatatext.text;
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

            if (gamedata.playercurrentscene <= 7)
            {
                saveslotchaptertext.text = "Prologo";
            }
            if(gamedata.playercurrentscene >= 8 && gamedata.playercurrentscene <= 12)
            {
                saveslotchaptertext.text = "Benjamin Capitulo 1";
            }
            if (gamedata.playercurrentscene >= 13 && gamedata.playercurrentscene <= 16)
            {
                saveslotchaptertext.text = "Lorenzo Capitulo 1";
            }
            if (gamedata.playercurrentscene >= 17 && gamedata.playercurrentscene <= 17)
            {
                saveslotchaptertext.text = "Isis Capitulo 1";
            }

            saveslotactorimage.name = gamedata.playercurrentactor;

            if(gamedata.playercurrentactor == "Enzo")
            {
                saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Enzo/Enzo_Save");
            }
            if (gamedata.playercurrentactor == "Benjamin")
            {
                saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Benjamin/Ben_Save");
            }
            if (gamedata.playercurrentactor == "Isis")
            {
                saveslotactorimage.sprite = Resources.Load<Sprite>("Actors/Isis/Isis_Save");
            }

            #region Obsolety
            /*
            if(gamedata.playercurrentactor == "Enzo")
                saveslotactorimage.sprite = Resources.Load<Actor>("Prefabs/Actor/Enzo").actorimage;
            */
            #endregion

            saveslotdatatext.text = gamedata.data;

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

            saveslotcurrentlasttextfile = gamedata.playerlasttextfile;

            saveslotcurrenttextfile = gamedata.playercurrenttextfile;

            saveslotcurrentdialogline = gamedata.playercurrentdialogline;

            saveslotcurrentscenestate = gamedata.currentscenestate;

            SaveSlotSaveData();

        }

        #endregion

        #region Case Load

        if ( gamedata.loadrequest >= -1 && gamedata.saverequest < 0)
        {

            LoadDataToGameData();

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
