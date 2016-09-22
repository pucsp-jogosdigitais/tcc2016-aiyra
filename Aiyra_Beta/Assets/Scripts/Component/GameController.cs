using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Actors Reference Keys
    private const string EnzoReference = "Enzo";
    private const string IsisReference = "Isis";
    private const string BenjaminEndress = "Benjamin";
    private const string MalikaReference = "Malika";
    private const string ZakiReference = "Zaki";
    #endregion

    #region Attributes
    public GameData gamedata;
    public GameSettings gamesettings;
    public Player player;
    public CameraEyeEffect playereyesfilter;
    public Background background;
    public MusicPlayer musicplayer;
    public DialogBox dialogbox;
    public PauseMenu pausemenu;

    public Actor[] actors;
    public Scene[] scenes;

    public int currentscene;
    public bool canprogress;
    #endregion

    #region Methods

    #region Enable and Disable Methods
    void OnEnable()
    {
        Debug.Log("Game Controller active");
    }
    void OnDisable()
    {
        Debug.Log("Game Controller disactive");
    }
    #endregion

    #region Awake and Start Methods
    void Awake()
    {
        if (gamedata == null)
            gamedata = GameObject.Find("GameData").GetComponent<GameData>();
        if (gamesettings == null)
            gamesettings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        if (player == null)
            player = GameObject.Find("Player Main Camera").GetComponent<Player>();
        if (playereyesfilter == null)
            playereyesfilter = GameObject.Find("PlayerEyesEffect").GetComponent<CameraEyeEffect>();
        if (background == null)
            background = GameObject.Find("Background").GetComponent<Background>();
        if (musicplayer == null)
            musicplayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        if (dialogbox == null)
            dialogbox = GameObject.Find("DialogDisplayBox").GetComponent<DialogBox>();
        if (pausemenu == null)
            pausemenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();

        if (actors == null || actors.Length <= 0)
            Debug.LogWarning("Game Controller has no actors");
        if (scenes == null || scenes.Length <= 0)
            Debug.LogError("No scene on the game controller");
        }
	void Start () {

        LoadAllGameAndPlayerDataToCurrentGame();
        
        musicplayer.PlayMusic();

        dialogbox.scene = scenes[currentscene];

        canprogress = true;
    }
    #endregion

    #region Updates Methods
    void Update()
    {
        #region Background Control

        if (scenes[currentscene].backgrounds.Length <= 0)
            background.gameObject.SetActive(false);
        else
        {
            background.gameObject.SetActive(true);
            background.backgroundimage.sprite = scenes[currentscene].backgrounds[background.currentbackground];
        }

        #endregion
        #region Music Control

        if (musicplayer.music.volume != gamesettings.musicvolume)
            musicplayer.music.volume = gamesettings.musicvolume;

        if (musicplayer.endatmusicclip != scenes[currentscene].musics.Length)
            musicplayer.LimitMusicLengh(scenes[currentscene].musics.Length - 1);

        if (scenes[currentscene].musics.Length > 0)
            musicplayer.music.clip = scenes[currentscene].musics[musicplayer.currentmusicclip];

        if (!musicplayer.music.isPlaying)
            musicplayer.NextMusic();

        #endregion
        #region Dialog Control

        PrepareAnswersMoments();

        dialogbox.DialogUpdate();

        UploadAnswersValue();

        AdjustDialogDisplayBoxToNextDialog();

        dialogbox.scene = scenes[currentscene];

        #endregion
        #region Actors Control

        PrepareActorDialogLines();

        for (int i = 0; i < actors.Length; i++)
        {
            actors[i].hasdialog = CheckActorDialogLines(actors[i]);
            if (actors[i].hasdialog)
                actors[i].gameObject.SetActive(true);
            else { actors[i].gameObject.SetActive(false); }
        }

        #endregion
        #region ObjectsI Control

        UpdateObjectIScene();

        LoadObjectsIInCurrentScene();

        UpdateObjectIVolume();

        #endregion
        #region Scene Control

        UpdateSceneBehaviours();
        /*
        ActiveCurrentSceneAndDisableOthers();
        */

        #endregion
        #region Game Control

        GoToGameSceneWhen();

        SaveAllGameAndPlayerDataOfCurrentGame();

        #endregion
        #region Player Control

        if (Input.GetButtonDown("Confirm"))
            if(canprogress)
                dialogbox.Processed();

        if (Input.GetButtonDown("Pause"))
        {
            if(pausemenu.gameObject.activeInHierarchy)
            {
                pausemenu.hideactors = false;
                pausemenu.affinitybarbox.SetActive(false);
                pausemenu.helpbox.SetActive(false);
                pausemenu.gameObject.SetActive(false);
            }
            else
            {
                pausemenu.OnClickPauseGame();
            }
        }

        #endregion
        #region Player Eye Control

        playereyesfilter.hasaction = CheckPlayerEyesActionMoments();

        UpdateCameraEyeMoments();

        UpdateCameraEyeEffect();

        playereyesfilter.CameraEyesUpdate();

        #endregion
    }
    #endregion

    #region GameController Fundamental Methods

    #region Dialog Methods

    #region Dialog Answer Methods
    void PrepareAnswersMoments()
    {
        if (currentscene == 0)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 8)
                    if(!dialogbox.hasanswered) 
                        dialogbox.dialog.isanswermoment = true;
                if (dialogbox.dialog.currentdialogline == 9)
                    dialogbox.hasanswered = false;
            }
        }
        if (currentscene == 3)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 5)
                    if (!dialogbox.hasanswered)
                        dialogbox.dialog.isanswermoment = true;
                if (dialogbox.dialog.currentdialogline == 6)
                    dialogbox.hasanswered = false;
            }
        }
        if(currentscene == 7)
        {
            if(dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 2)
                    if (!dialogbox.hasanswered)
                        dialogbox.dialog.isanswermoment = true;
                if (dialogbox.dialog.currentdialogline == 3)
                    dialogbox.hasanswered = false;
            }
        }
        #region Test
        /*
        if (currentscene == 1)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                    if (!dialogbox.hasanswered) 
                        dialogbox.dialog.isanswermoment = true;
            }
            if(dialogbox.currentdialog != 0)
            {
                dialogbox.hasanswered = false;
            }
        }
        */
        #endregion
    }
    void UploadAnswersValue()
    {
        if (currentscene == 7)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 0)
                {
                    dialogbox.SetAnswerButtonsValue(5, 10, 3);
                }
            }
        }
        if (currentscene == 1)
        {
            if (dialogbox.currentdialog == 0)
            {
                dialogbox.SetAnswerButtonsValue(0, 0, 0);
            }
        }
    }

    #endregion

    #region Next Dialog Adjust Methods

    void AdjustDialogDisplayBoxToNextDialog()
    {
        if(currentscene == 0)
        {
            if (dialogbox.currentdialog == 0)
                if(dialogbox.lastanswerid < 0 || dialogbox.lastanswerid > 3)
                    dialogbox.nextdialog = 1;
            dialogbox.AnswerSetNextDialog(1, 2, 3);
        }
        else if(currentscene >= 1 && currentscene < 3)
        {
            if(dialogbox.currentdialog == 0)
            {
                if(dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                    dialogbox.nextdialog = 1;
                else { dialogbox.nextdialog = dialogbox.currentdialog; }
            }
        }
        else if (currentscene == 3)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                    dialogbox.nextdialog = 1;
                dialogbox.AnswerSetNextDialog(1, 2, 3);
            }
        }
        else if (currentscene >= 4 && currentscene < 8)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                    dialogbox.nextdialog = 1;
                else { dialogbox.nextdialog = dialogbox.currentdialog; }
            }
        }
    }

    #endregion

    #region Test Methods

    void ControlDialogBehaviour(int Scene,int Dialog,int DialogLine,int AnswerButton0AffinityValue, int AnswerButton1AffinityValue, int AnswerButton2AffinityValue,int NextDialogCaseAnswer0,int NextDialogCaseAnswer1,int NextDialogCaseAnswer2)
    {
        if (currentscene == Scene && dialogbox.currentdialog == Dialog && dialogbox.dialog.currentdialogline == DialogLine)
        {
            dialogbox.SetAnswerButtonsValue(AnswerButton0AffinityValue, AnswerButton1AffinityValue, AnswerButton2AffinityValue);
            dialogbox.AnswerSetNextDialog(NextDialogCaseAnswer0, NextDialogCaseAnswer1, NextDialogCaseAnswer2);
        }
    }

    #endregion

    #endregion

    #region Actors Methods
    bool CheckActorDialogLines(Actor ActorI)
    {
        foreach (int actordialogline in ActorI.dialoglines)
            if (actordialogline == dialogbox.dialog.currentdialogline)
                return true;

        return false;
    }
    void PrepareActorDialogLines()
    {
        for(int i = 0; i < actors.Length; i ++)
        {
            if (!pausemenu.hideactors)
            {
                if (actors[i].name == "Enzo")
                {
                    if (currentscene == 0)
                    {
                        if (dialogbox.currentdialog == 0)
                            actors[i].dialoglines = new int[5] { 3,4,5,6,7 };
                        else { actors[i].dialoglines = new int[0]; }
                    }
                    else { actors[i].dialoglines = new int[0]; }
                    if (currentscene == 7)
                    {
                        if (dialogbox.currentdialog == 0)
                            actors[i].dialoglines = new int[1] { 0 };
                        else { actors[i].dialoglines = new int[0]; }
                    }
                    else { actors[i].dialoglines = new int[0]; }
                }
            }
            else
            {
                actors[i].dialoglines = new int[0];
            }
        }
    }
    #endregion

    #region ObjectI Methods

    void UpdateObjectIScene()
    {
        foreach (GameObject objecti in scenes[currentscene].objects)
        {
            if (objecti != null)
                objecti.GetComponent<ObjectI>().scene = scenes[currentscene];
        }

    }
    void LoadObjectsIInCurrentScene()
    {
        for (int i = 0; i < scenes.Length; i++)
            if(i != currentscene)
                foreach (GameObject objecti in scenes[i].objects)
                    objecti.SetActive(false);

        foreach (GameObject objecti in scenes[currentscene].objects)
            if (objecti.GetComponent<ObjectI>().isavailable)
                objecti.SetActive(true);
    }
    void UpdateObjectIVolume()
    {
        foreach (GameObject objecti in scenes[currentscene].objects)
            if (objecti.GetComponent<ObjectI>().objectsound.volume != gamesettings.effectsvolume)
                objecti.GetComponent<ObjectI>().objectsound.volume = gamesettings.effectsvolume;
    }

    #endregion

    #region Scenes Methods

    void ActiveCurrentSceneAndDisableOthers()
    {
        for (int i = 0; i < scenes.Length; i++)
            if (scenes[i].sceneid == currentscene)
                scenes[i].gameObject.SetActive(true);
            else { scenes[i].gameObject.SetActive(false); }
    }

    void UpdateSceneBehaviours()
    {

        for (int i = 0; i < scenes.Length; i++)
            if (scenes[i].gameObject.activeInHierarchy)
                if(scenes[i].GetComponent<SceneBehaviour>() != null)
                    scenes[i].GetComponent<SceneBehaviour>().UpdateSceneBehaviour();                
    }

    #endregion

    #region Player Eyes Filter Methods

    bool CheckPlayerEyesActionMoments()
    {
        foreach (int actionmoment in playereyesfilter.moments)
            if (actionmoment == dialogbox.dialog.currentdialogline)
                return true;

        return false;
    }

    void UpdateCameraEyeMoments()
    {
        if (currentscene == 0)
        {
            if (dialogbox.currentdialog == 0)
               playereyesfilter.moments = new int[1] { 2 };
        }
        else { playereyesfilter.moments = new int[0]; }

    }

    void UpdateCameraEyeEffect()
    {
        if(currentscene == 0)
        {
            if(dialogbox.currentdialog == 0)
            {
                if(dialogbox.dialog.currentdialogline == 0)
                {
                    playereyesfilter.state = CameraEyeEffect.effectstate.Open;
                }
                if(dialogbox.dialog.currentdialogline == 3)
                {
                    playereyesfilter.state = CameraEyeEffect.effectstate.Close;
                }
            }
        }
    }

    #endregion

    #region Game Methods

    #region Navegation Methods
    void GoToGameSceneWhen()
    {
        if (currentscene == 0)
        {
            if (dialogbox.currentdialog == 0)
            {
                if(dialogbox.dialog.currentdialogline == 6)
                {
                    gamedata.SaveAllPlayerData();
                    gamedata.SaveAllGameData();
                    if (!gamedata.issaving || !gamedata.isloading)
                    { 
                        if (player.playername.Length <= 0) 
                            Application.LoadLevel(9);
                    }
                }
            }
        }
        if (currentscene == 7)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline >= 0)
                {
                    if (gamedata.playercurrentactor == "")
                    {
                        gamedata.SaveAllPlayerData();
                        gamedata.SaveAllGameData();
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            Application.LoadLevel(8);
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Time Adjust Methods

    #endregion

    #region Check Methods
    bool CheckDataChanges()
    {
        if (player.playercurrentactor != "")
        {
            if (player.playercurrentactor == "Enzo")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentenzoaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Isis")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentisisaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Benjamin")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentbenjaminaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Malika")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentmalikaaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Zaki")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentzakiaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                    return true;
                else { return false; }
            }
        }
        else
        {
            if (gamedata.playername != player.playername
                || gamedata.playercurrentactor != player.playercurrentactor

                || gamedata.playercurrentscene != currentscene
                || gamedata.playercurrenttextfile != dialogbox.currentdialog
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString())
                return true;
        }

        return false;
    }

    #endregion

    #region SaveData Methods
    void SaveAllGameAndPlayerDataOfCurrentGame()
    {
        gamedata.issaving = CheckDataChanges();
        if(gamedata.issaving)
        {
            gamedata.playername = player.playername;
            gamedata.playercurrentactor = player.playercurrentactor;

            if (player.playercurrentactor != "")
            {
                if (player.playercurrentactor == "Enzo")
                    gamedata.currentenzoaffinity = player.currentactoraffinity;
                if (player.playercurrentactor == "Isis")
                    gamedata.currentisisaffinity = player.currentactoraffinity;
                if (player.playercurrentactor == "Benjamin")
                    gamedata.currentbenjaminaffinity = player.currentactoraffinity;
                if (player.playercurrentactor == "Malika")
                    gamedata.currentmalikaaffinity = player.currentactoraffinity;
                if (player.playercurrentactor == "Zaki")
                    gamedata.currentzakiaffinity = player.currentactoraffinity;
            }

            gamedata.playercurrentscene = currentscene;
            gamedata.playercurrenttextfile = dialogbox.currentdialog;
            gamedata.playercurrentdialogline = dialogbox.dialog.currentdialogline;
            gamedata.currentscenestate = scenes[currentscene].scenestate.ToString();

            gamedata.gameprogress = currentscene+1;
            gamedata.SetData();
            gamedata.playtime = Time.time;

            gamedata.SaveAllPlayerData();
            gamedata.SaveAllGameData();
        }
    }
    #endregion

    #region LoadData Methods
    void LoadAllGameAndPlayerDataToCurrentGame()
    {
        //Load all GameData that as to load
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();

        //Put the values of the gamedata in the game to it run
        if (gamedata.playername != "")
        {
            //Update player name
            player.playername = gamedata.playername;
        }
        else { Debug.LogWarning("The player has no nome"); }
        if (gamedata.playercurrentactor != "")
        {
            //update player current actor
            player.playercurrentactor = gamedata.playercurrentactor;

            //check which actor history line is the player and update it current
            // affinity with that player
            if (gamedata.playercurrentactor == "Enzo")
                player.currentactoraffinity = gamedata.currentenzoaffinity;
            if (gamedata.playercurrentactor == "Isis")
                player.currentactoraffinity = gamedata.currentisisaffinity;
            if (gamedata.playercurrentactor == "Benjamin")
                player.currentactoraffinity = gamedata.currentbenjaminaffinity;
            if (gamedata.playercurrentactor == "Malika")
                player.currentactoraffinity = gamedata.currentmalikaaffinity;
            if (gamedata.playercurrentactor == "Zaki")
                player.currentactoraffinity = gamedata.currentzakiaffinity;
        }
        else { Debug.LogWarning("The player has no actor history line"); }

        //Put the values of the player current state in the history of the game
        currentscene = gamedata.playercurrentscene;
        dialogbox.currentdialog = gamedata.playercurrenttextfile;
        dialogbox.dialog.currentdialogline = gamedata.playercurrentdialogline;
        if (gamedata.currentscenestate == "dialog")
            scenes[currentscene].scenestate = Scene.state.dialog;
        if (gamedata.currentscenestate == "interaction")
            scenes[currentscene].scenestate = Scene.state.interaction;
        if (gamedata.currentscenestate == "puzzle")
            scenes[currentscene].scenestate = Scene.state.puzzle;

    }
    #endregion

    #endregion

    #endregion

    #endregion
}
