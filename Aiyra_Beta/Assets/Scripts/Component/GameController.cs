using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Actors Reference Keys
    private const string unkownreference = "???";
    private const string juruparireference = "Jurupari";
    private const string motherreference = "Mãe";
    private const string enzoreference = "Enzo";
    private const string isisreference = "Isis";
    private const string benjaminreference = "Benjamin";
    private const string malikareference = "Malika";
    private const string zakireference = "Zaki";

    private const string enzogalleryreference = "ENZOGALLERY" /* + CGID;*/;
    private const string isisgalleryreference = "ISISGALLERY" /* + CGID;*/;
    private const string benjamingalleryreference = "BENJAMINGALLERY" /*  + CGID*/;
    private const string malikagalleryreference = "MALIKAGALLERY" /* + CGID;*/;
    private const string zakigalleryreference = "ZAKIGALLERY" /* + CGID;*/;

    private const string enzodiaryreference = "ENZODIARY" /* + CGID*/;
    private const string isisdiaryreference = "ISISDIARY" /* + CGID*/;
    private const string benjamindiaryreference = "BENJAMINDIARY" /* + CGID*/;
    private const string malikadiaryreference = "MALIKADIARY" /* + CGID*/;
    private const string zakidiaryreference = "ZAKIDIARY" /* + CGID*/;

    private const string enzoendreference = "ENZOEND" /*  + CGID*/;
    #endregion

    #region Attributes

    public CollectionData gamecollection;
    public GameData gamedata;
    public GameSettings gamesettings;
    public LoadingInterface loadinginterface;
    public Player player;
    public CameraEyeEffect playereyesfilter;
    public MotionBlur effectscamerablurfilter;
    public LiveBackground livebackground;
    public Background background;
    public MusicPlayer musicplayer;
    public DialogBox dialogbox;
    public DialogProcedIcon dialogboxprocedicon;
    public PauseMenu pausemenu;

    public Actor[] actors;
    public Scene[] scenes;

    public int currentscene;
    public int currentgameend;
    public bool[] answersresultreaded;
    public bool canprogress;
    public bool isloading;
    public bool isgamefinal;

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
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (gamedata == null)
            gamedata = GameObject.Find("GameData").GetComponent<GameData>();
        if (gamesettings == null)
            gamesettings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        if (loadinginterface == null)
            loadinginterface = GameObject.Find("CanvasLoadingInterface").GetComponent<LoadingInterface>();
        if (player == null)
            player = GameObject.Find("Player Main Camera").GetComponent<Player>();
        if (playereyesfilter == null)
            playereyesfilter = GameObject.Find("PlayerEyesEffect").GetComponent<CameraEyeEffect>();
        if (livebackground == null)
            livebackground = GameObject.Find("LiveBackground").GetComponent<LiveBackground>();
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

        SetScenesID();

        SetScenesName();

        SetFinalScenes();

        LoadAllGameAndPlayerDataToCurrentGame();

        musicplayer.PlayMusic();

        dialogbox.scene = scenes[currentscene];

        canprogress = true;

        isgamefinal = false;
    }
    #endregion

    #region Updates Methods
    //Method that will upload all scripts, game objects,etc that is in game scene 
    void Update()
    {
        #region LoadingInterface Control

        LoadingInterfaceManager();

        #endregion
        #region Background Control

        BackgroundManager();

        #endregion
        #region Music Control

        MusicManager();

        #endregion
        #region Dialog Control

        UpdateSpeaker();

        UpdateNextCGToUnlockAndItStatus();

        PrepareAnswersMoments();

        dialogbox.DialogUpdate();

        DialogProcedIconManager();

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

        PrepareActorExpressions();

        #endregion
        #region ObjectsI Control

        UpdateObjectIScene();

        LoadObjectsIInCurrentScene();

        UpdateObjectIVolume();

        #endregion
        #region Puzzle Control

        UpdatePuzzleScene();

        UpdatePuzzle();

        LoadPuzzleInCurrentScene();

        UpdatePuzzleIVolume();

        #endregion
        #region Scene Control
        /*
        UpdateSceneBehaviours();
        
        ActiveCurrentSceneAndDisableOthers();
        */
        ScenesManager();

        #endregion
        #region Game Control

        GoToGameSceneWhen();

        SaveAllGameAndPlayerDataOfCurrentGame();

        isgamefinal = CheckIsFinalScene(scenes[currentscene].GetComponent<SceneBehaviour>());

        currentgameend = CheckFinalAffinityWithCurrentActor();

        if (isgamefinal)
        {
            CompleteGameCheckAffinityUnlockEndAndPlayGameEnd();
        }

        #endregion
        #region Player Control

        PlayerInputs();

        PlayerInventaryAdjust();

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

    #region LoadingInterface Methods

    void LoadingInterfaceManager()
    {
        switch (currentscene)
        {
            case 0:
                switch (dialogbox.currentdialog)
                {
                    case 0:
                        switch(dialogbox.dialog.currentdialogline)
                        {
                            case 0:
                                if (loadinginterface.loadingtimescount <= 0)
                                {
                                    loadinginterface.loadingmessegestext = new string[1] { "Como Jogar" };
                                    loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                    loadinginterface.tutorial.sprite = Resources.Load<Sprite>("");
                                    loadinginterface.gameObject.SetActive(true);
                                    canprogress = false;
                                    if (loadinginterface.loadingtime > 0)
                                    {
                                        loadinginterface.UploadLoadingMessage();
                                        loadinginterface.loadingtime -= 1f;
                                    }
                                    else
                                    {
                                        loadinginterface.loadingtimescount++;
                                        loadinginterface.loadingtime = 200f;
                                    }
                                }
                                else
                                {
                                    loadinginterface.gameObject.SetActive(false);
                                    canprogress = true;
                                }
                                break;
                        }
                        break;
                }
                break;
        }
    }

    #endregion

    #region Background Methods

    //method that manage the livebackground and background
    void BackgroundManager()
    {
        //Check if the current scene have a livebackground to desactive or active the livebackground script
        if (scenes[currentscene].livebackground.Length <= 0)
            livebackground.gameObject.SetActive(false);
        else
        {
            livebackground.gameObject.SetActive(true);

            if (livebackground.movies.Length != scenes[currentscene].livebackground.Length)
            {
                livebackground.movies = new MovieTexture[scenes[currentscene].livebackground.Length];
                for (int i = 0; i < scenes[currentscene].livebackground.Length; i++)
                    livebackground.movies[i] = scenes[currentscene].livebackground[i];
            }

            livebackground.UpdateLiveBackground();
        }

        //Check if the current scene have a background to desactive or active the background script
        if (scenes[currentscene].backgrounds.Length <= 0)
            background.gameObject.SetActive(false);
        else
        {
            background.gameObject.SetActive(true);
            background.backgroundimage.sprite = scenes[currentscene].backgrounds[background.currentbackground];
        }
    }

    #endregion

    #region Music Methods

    void MusicManager()
    {
        //check if the volume of musicplayer in´t equal to the gamesettings volume and in case of diference make the ajusts
        if (musicplayer.music.volume != gamesettings.musicvolume)
            musicplayer.music.volume = gamesettings.musicvolume;

        //check if the length of the musicplayer music list and in case of not correct if value make it length equal to the playlist of the current scene
        if (musicplayer.endatmusicclip != scenes[currentscene].musics.Length)
            musicplayer.LimitMusicLengh(scenes[currentscene].musics.Length - 1);

        //check if the current scene length is greater than 0 to make the music player play and work
        if (scenes[currentscene].musics.Length > 0)
            musicplayer.music.clip = scenes[currentscene].musics[musicplayer.currentmusicclip];

        //check if the music player has end the current music to play the next music
        if (!musicplayer.music.isPlaying)
            musicplayer.NextMusic();
    }

    #endregion

    #region Dialog Methods

    #region Set Speaker(Current Actor that is Speaking) Methods

    //Method that update the speaker box of the dialog box changing the name of the speaker to the current and correct actor name
    void UpdateSpeaker()
    {
        switch(currentscene)
        {
            case 0:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline >= 0 && dialogbox.dialog.currentdialogline < 5)
                        dialogbox.SetSpeakerName(unkownreference);
                    if (dialogbox.dialog.currentdialogline == 6 || dialogbox.dialog.currentdialogline >= 8)
                        dialogbox.SetSpeakerName(juruparireference);
                    if (dialogbox.dialog.currentdialogline == 7)
                        dialogbox.SetSpeakerName(player.playername);
                }
                else if(dialogbox.currentdialog == 1)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if(dialogbox.currentdialog == 2)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if (dialogbox.currentdialog == 3)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if(dialogbox.currentdialog == 4)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                break;
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if(dialogbox.currentdialog == 1)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if(dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline >= 0 && dialogbox.dialog.currentdialogline < 6 || dialogbox.dialog.currentdialogline >= 8)
                        dialogbox.SetSpeakerName(player.playername);
                    else { dialogbox.SetSpeakerName(motherreference); }
                }

                break;
            case 2:
                if(dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 0
                       || dialogbox.dialog.currentdialogline == 3)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 1 
                || dialogbox.currentdialog == 2
                || dialogbox.currentdialog == 3)
                {
                    dialogbox.SetSpeakerName(motherreference);
                }
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }

    #endregion

    #region DialogProcedIcon Methods

    void DialogProcedIconManager()
    {
        if (dialogbox.dialog.currentdialogline < dialogbox.dialog.enddialogatline)
        {
            dialogboxprocedicon.procediconanimator.SetBool("isprocedtime", canprogress);
        }
    }

    #endregion

    #region Dialog Answer Methods

    #region Dialog Prepare Answer Methods
    //method that prepare dialog for a answer moment and alert when is the moment of give the object
    void PrepareAnswersMoments()
    {
        switch (currentscene)
        {
            case 0:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 9)
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    if (dialogbox.dialog.currentdialogline == 10)
                        dialogbox.hasanswered = false;
                }
                else
                {
                    if (dialogbox.nextdialog != 4)
                    {
                        if (dialogbox.currentdialog != 4)
                        {
                            if (dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                            {
                                if (!dialogbox.hasanswered)
                                    dialogbox.dialog.isanswermoment = true;
                            }
                            else
                            {
                                dialogbox.hasanswered = false;
                            }
                        }
                        else
                        {
                            if (dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                                dialogbox.nextdialog = dialogbox.currentdialog;
                        }
                    }
                }
                break;
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 3)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                break;
            case 2:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 3)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                break;
            case 7:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 3)
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    if (dialogbox.dialog.currentdialogline != 3)
                        dialogbox.hasanswered = false;
                }
                break;
        }
    }

    #endregion

    #region Dialog Answer Values Methods
    //method that give the value of affinity the answer buttons will have 
    void UploadAnswersValue()
    {
        switch (currentscene)
        {
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.SetAnswerButtonsValue(0, 0, 0);
                }
                break;
            case 7:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.SetAnswerButtonsValue(5, 10, 3);
                }
                break;
        }
        
    }
    
    #endregion

    #region CG To Unlock Methods
    //Method that update the cg that will unlock on click of it respective answer button
    void UpdateNextCGToUnlockAndItStatus()
    {
        switch (currentscene)
        {
            case 0:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.AnswerButtonsSetNextCG(enzodiaryreference + 0.ToString(), isisdiaryreference + 0.ToString(), isisdiaryreference + 1.ToString());
                    dialogbox.AnswerButtonsSetNextCGStatus(false, false, false);
                }
                break;
            default:
                dialogbox.AnswerButtonsSetNextCG("","","");
                dialogbox.AnswerButtonsSetNextCGStatus(false, false, false);
                break;
        }
    }
    #endregion

    #region Next Dialog Adjust Methods

    //method that adjust next dialog for current dialog not value has the final dialog causing the dialog to end
    void AdjustDialogDisplayBoxToNextDialog()
    {
        //check the current scene to programming next dialog according
        switch (currentscene)
        {
            case 0:
                if (dialogbox.currentdialog == 0)
                {
                    answersresultreaded = new bool[3];
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.lastanswerid < 0)
                    {
                        dialogbox.nextdialog = 5;
                    }
                }
                else
                {
                    if (CheckAnswersResultReaded() == true)
                    {
                        dialogbox.nextdialog = 4;
                    }

                    if (dialogbox.currentdialog == 1)
                    {
                        if (answersresultreaded.Length <= 0)
                            answersresultreaded = new bool[3];
                        else
                        {
                            answersresultreaded[0] = true;
                        }
                    }
                    if (dialogbox.currentdialog == 2)
                    {
                        if (answersresultreaded.Length <= 0)
                            answersresultreaded = new bool[3];
                        else
                        {
                            answersresultreaded[1] = true;
                            answersresultreaded[2] = false;
                        }
                    }
                    if (dialogbox.currentdialog == 2)
                    {
                        if (answersresultreaded.Length <= 0)
                            answersresultreaded = new bool[3];
                        else
                        {
                            answersresultreaded[2] = true;
                        }
                    }

                    if (dialogbox.nextdialog == dialogbox.currentdialog)
                        dialogbox.nextdialog++;
                }
                break;
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline < 3)
                        dialogbox.nextdialog = 3;

                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 0);
                }
                if (dialogbox.currentdialog == 1 || dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline <= dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 3;
                }
                break;
            case 2:
                if (dialogbox.currentdialog == 0 || dialogbox.currentdialog == 4)
                {
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if(dialogbox.currentdialog == 1 || dialogbox.currentdialog == 2 || dialogbox.currentdialog == 3)
                {
                    dialogbox.nextdialog = 4;
                }
                break;
            case 3:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.nextdialog = 1;

                }
                break;
            case 4:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 1;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 5:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 1;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 6:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 1;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 7:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 1;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 8:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = 1;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            default:
                Debug.Log("Their is no default to next dialog yeat");
                break;
        }
    }

    #endregion
    
    #endregion

    #region Check AnswerResultReaded

    //Method that check if the player has read all the possible results of a dialog questions
    bool CheckAnswersResultReaded()
    {
        switch (answersresultreaded.Length)
        {
            case 0:
                Debug.Log("No Space in the answers readed check array");
                break;
            case 1:
                if (answersresultreaded[0] == true)
                    return true;
                break;
            case 2:
                if (answersresultreaded[0] == true && answersresultreaded[1] == true)
                    return true;
                break;
            case 3:
                if (answersresultreaded[0] == true && answersresultreaded[1] == true && answersresultreaded[2] == true)
                    return true;
                break;
            default:
                for (int i = 0; i < answersresultreaded.Length; i++)
                    if (answersresultreaded[i] == true)
                        for (int j = 0; j < answersresultreaded.Length; j++)
                            if (j != i)
                                if (answersresultreaded[i] == true && answersresultreaded[j] == true)
                                    if (j == answersresultreaded.Length)
                                        return true;
                break;
        }

        return false;
    }

    #endregion

    #endregion

    #region Actors Methods
    bool CheckActorDialogLines(Actor ActorI)
    {
        foreach (int actordialogline in ActorI.dialoglines)
            if (!pausemenu.hideactors)
            {
                if (actordialogline == dialogbox.dialog.currentdialogline)
                    return true;
            }
            else { Debug.Log("Game Paused and request actor to hide their selfts"); return false; }

        return false;
    }
    void PrepareActorDialogLines()
    {
        for (int i = 0; i < actors.Length; i++)
        {
            switch (actors[i].name)
            {
                #region Benjamin lines
                case "Benjamin":
                    switch (currentscene)
                    {
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Benjamin lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                #endregion
                #region Enzo Lines
                case "Enzo":
                    switch (currentscene)
                    {
                        case 7:
                            if (dialogbox.currentdialog == 0)
                                actors[i].dialoglines = new int[5] { 0, 1, 2, 3, 4 };
                            else { actors[i].dialoglines = new int[0]; }
                            break;
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Enzo lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                #endregion
                #region Isis Lines
                    /*
                case "Isis":
                    switch (currentscene)
                    {
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Isis lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                    */
                #endregion
                #region Jurupari Lines
                case "Jurupari":
                    switch (currentscene)
                    {
                        case 0:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                                    break;
                                case 1:
                                    actors[i].dialoglines = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
                                    break;
                                case 2:
                                    actors[i].dialoglines = new int[4] { 0, 1, 2, 3 };
                                    break;
                                case 3:
                                    actors[i].dialoglines = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
                                    break;
                                case 4:
                                    actors[i].dialoglines = new int[3] { 0, 1, 2 };
                                    break;
                                default:
                                    actors[i].dialoglines = new int[0];
                                    break;
                            }
                            break;
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Jurupari lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                #endregion
                #region Mae do jogador Lines
                case "MaedaLuna":
                    switch (currentscene)
                    {
                        case 2:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[4] { 0, 1, 2, 3 };
                                    break;
                            }
                            break;
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Mae da Personagem lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                #endregion
            }
        }
    }
    void PrepareActorExpressions()
    {
        for (int i = 0; i < actors.Length; i++)
        {
            if (actors[i].isActiveAndEnabled)
            {
                switch (actors[i].name)
                {
                    #region Benjamin Emotions
                    case "Benjamin":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 0:
                                    switch (dialogbox.dialog.currentdialogline)
                                    {
                                        case 0:
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                            break;
                                        default:
                                            Debug.Log("Doing actor Benjamin Default motion");
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                            break;
                                    }
                                    break;
                                default:
                                    if (actors[i].actoranimator.GetInteger(actors[i].motionreference) != 0)
                                    {
                                        Debug.Log("Doing actor Benjamin Default motion");
                                        actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("Actor " + actors[i].actorname + " Not Active gamecontroller will not set it animation");
                        }
                            break;
                    #endregion
                    #region Enzo Emotions
                    case "Enzo":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 7:
                                    switch (dialogbox.dialog.currentdialogline)
                                    {
                                        case 0:
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                            break;
                                        default:
                                            Debug.Log("Doing actor Enzo Default motion");
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                            break;
                                    }
                                    break;
                                default:
                                    if (actors[i].actoranimator.GetInteger(actors[i].motionreference) != 0)
                                    {
                                        Debug.Log("Doing Default of actor Enzo motion");
                                        actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("Actor " + actors[i].actorname + " Not Active gamecontroller will not set it animation");
                        }
                        break;
                    #endregion
                    #region Isis Emotions
                    /*
                case "Isis":
                    break;
                    */
                    #endregion
                    #region Jurupari Emotions
                    case "Jurupari":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 0:
                                    switch (dialogbox.currentdialog)
                                    {
                                        case 0:
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                            }
                                            break;
                                        case 1:
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                            }
                                            break;
                                        case 2:
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                            }
                                            break;
                                        default:
                                            if (actors[i].actoranimator.GetInteger(actors[i].motionreference) != 0)
                                            {
                                                Debug.Log("Doing actor Jurupari Default motion");
                                                actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                            }
                                            break;
                                    }
                                    break;
                                default:
                                    if (actors[i].actoranimator.GetInteger(actors[i].motionreference) != 0)
                                    {
                                        Debug.Log("Doing Default of actor Jurupari motion");
                                        actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("Actor " + actors[i].actorname + " Not Active gamecontroller will not set it animation");
                        }
                        break;
                    #endregion
                    #region Mae Do Personagem Emotions
                    case "MaedaLuna":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 0:
                                    switch (dialogbox.dialog.currentdialogline)
                                    {
                                        case 0:
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                            break;
                                    }
                                    break;
                                default:
                                    if (actors[i].actoranimator.GetInteger(actors[i].motionreference) != 0)
                                    {
                                        Debug.Log("Doing Default of actor Mae do jogador motion");
                                        actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("Actor " + actors[i].actorname + " Not Active gamecontroller will not set it animation");
                        }
                        break;
                        #endregion
                }
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

    #region Puzzle Methods
    //Method that update the scene variable of the puzzle script
    void UpdatePuzzleScene()
    {
        foreach (Puzzle puzzle in scenes[currentscene].puzzles)
        {
            if (puzzle != null)
                puzzle.scene = scenes[currentscene];
        }
    }
    //Method that desactive all puzzle except the ones of current scene and active this puzzles
    void LoadPuzzleInCurrentScene()
    {
        //Whip that desactive all puzzle that is not part of the current scene
        for (int i = 0; i < scenes.Length; i++)
            if (i != currentscene)
                foreach (Puzzle puzzle in scenes[i].puzzles)
                    puzzle.gameObject.SetActive(false);

        //Forreach that active any no resolved puzzle of the current scene
        foreach (Puzzle puzzle in scenes[currentscene].puzzles)
            if (!puzzle.resolved)
                puzzle.gameObject.SetActive(true);
    }
    //Method that update puzzle volume if it is active
    void UpdatePuzzleIVolume()
    {
        foreach (Puzzle puzzle in scenes[currentscene].puzzles)
            if (puzzle.gameObject.activeInHierarchy)
                if (puzzle.puzzlesound.volume != gamesettings.effectsvolume)
                    puzzle.puzzlesound.volume = gamesettings.effectsvolume;
    }
    //Method that update puzzle status and behaviour
    void UpdatePuzzle()
    {
        foreach (Puzzle puzzlei in scenes[currentscene].puzzles)
        {
            //Check if the puzzle status save key has any letter in it to save the puzzle status
            if(puzzlei.puzzlestatussavekey.Length <= 0)
                puzzlei.UploadPuzzleSaveKey();

            //Check if puzzle has not be loaded to load it
            if (!puzzlei.hasbeenloaded)
            {
                puzzlei.LoadPuzzleStatus();
                puzzlei.hasbeenloaded = true;
            }
            //Check if puzzle has been resolved to save it status
            if (puzzlei.resolved && puzzlei.isreplayabel == false)
                puzzlei.SavePuzzleStatus();
            
            //Check if is puzzle is not resolve so the method can try to get it puzzlepicture script to update picture status
            if (!puzzlei.resolved && puzzlei.gameObject.activeInHierarchy)
            {
                if (puzzlei.GetComponent<PuzzlePicture>() != null)
                    puzzlei.GetComponent<PuzzlePicture>().UpdatePicuteStatus();
            }
        }
    }

    #endregion

    #region Scenes Methods

    void SetScenesID()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].sceneid = i;
            Debug.Log("Scene " + scenes[i].gameObject.name + " ID equal " + scenes[i].sceneid);
        }
    }
    void SetScenesName()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            switch (scenes[i].sceneid)
            {
                case 0: scenes[i].scenename = "Sonho"; break;
                case 1: scenes[i].scenename = "Quarto"; break;
                case 2: scenes[i].scenename = "Sala de Estar"; break;
                case 3: scenes[i].scenename = "Rua Casa Abandonada"; break;
                case 4: scenes[i].scenename = "Sala de Aula"; break;
                case 5: scenes[i].scenename = "Rua da Escola"; break;
                case 6: scenes[i].scenename = "Jardim da Casa Abandonada"; break;
                case 7: scenes[i].scenename = "Sala de Aula Magica"; break;
            }
            Debug.Log("Scene " + scenes[i].scenename + " Avaliable");
        }
    }
    void SetFinalScenes()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            switch (i)
            {
                case 0: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 1: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 2: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 3: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 4: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 5: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 6: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 7: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = true; break;
            }
            Debug.Log("Scene " + scenes[i].scenename + " is a final scene?: " + scenes[i].GetComponent<SceneBehaviour>().isfinalscene);
        }
    }
    void ScenesManager()
    {
        switch (currentscene)
        {
            case 0:
                OnDialogEndGoTo(1);
                break;
            case 3:
                if (dialogbox.currentdialog >= 1 && dialogbox.currentdialog <= 2)
                {
                    OnDialogEndGoTo(4);
                }
                if (dialogbox.currentdialog == 3)
                {
                    OnDialogEndGoTo(6);
                }
                break;
            case 5:
                TimeToSetScene();
                OnDialogEndGoTo(6);
                break;
            case 6:
                OnTimeEndGoTo(7);
                break;
        }
    }

    #region Scene progression Cases Methods

    void OnDialogEndGoTo(int SceneToGo)
    {
        if (canprogress && !dialogbox.gameObject.activeInHierarchy)
        {
            currentscene = SceneToGo;
            dialogbox.StartDialog(0);
            Debug.Log("You has go to scene" + SceneToGo);
        }
    }  
    void OnDialogEndSaveGameProgress()
    {
        if (!dialogbox.gameObject.activeInHierarchy)
        {
            gamedata.SaveAllPlayerData();
            gamedata.SaveAllGameData();
            Debug.Log("Game has been save");
        }
    }
    void OnDialogEndCompleteTheGame()
    {
        CompleteGameCheckAffinityUnlockEndAndPlayGameEnd();
    }
    void OnDialogEndSaveGameAndLoadGameScene(int DialogReference, int DialogLineReference, int GameSceneToGo)
    {
        if (dialogbox.currentdialog == DialogReference && dialogbox.dialog.currentdialogline == DialogLineReference)
        {
            gamedata.SaveAllPlayerData();
            gamedata.SaveAllGameData();
            if (!gamedata.issaving || !gamedata.isloading)
                Application.LoadLevel(GameSceneToGo);
        }
    }

    #endregion

    #endregion

    #region Player Methods

    void PlayerInputs()
    {
        if (Input.GetButtonDown("Confirm"))
            if (canprogress)
                dialogbox.Processed();

        if (Input.GetButtonDown("Pause"))
        {
            if (pausemenu.gameObject.activeInHierarchy)
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
    }
    void PlayerInventaryAdjust()
    {
        if (player.inventary.Length <= 0)
        {
            player.inventary = new string[1];
        }
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
        switch (currentscene)
        {
            case 0:
                if (dialogbox.currentdialog == 0)
                {
                    SceneBehaviour scenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();

                    if (dialogbox.dialog.currentdialogline == 6)
                    {
                        gamedata.SaveAllPlayerData();
                        gamedata.SaveAllGameData();
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            if (player.playername.Length <= 0)
                            {
                                if (scenebehaviour.timertogo > 0)
                                {
                                    scenebehaviour.timertogo--;
                                    if (canprogress == true)
                                        canprogress = false;
                                }
                                else { Application.LoadLevel(9); }
                            }
                            else { scenebehaviour.timertogo = 100; }
                        }
                    }
                }
                break;
            case 6:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline >= 0)
                    {
                        //check if player has a actor selected so the game can put him the portal and in the scene were the player can select the actor
                        if (gamedata.playercurrentactor == "")
                        {
                            gamedata.SaveAllPlayerData();
                            gamedata.SaveAllGameData();
                            if (!gamedata.issaving || !gamedata.isloading)
                            {
                                Application.LoadLevel(8);
                            }
                        }
                        //this are the checks that will guide the player to the scene of his actor
                        else if(gamedata.playercurrentactor == "Benjamin")
                        {
                            currentscene = 1;
                        }
                        else if(gamedata.playercurrentactor == "Enzo")
                        {
                            currentscene = 2;
                        }
                        else if(gamedata.playercurrentactor == "Isis")
                        {
                            currentscene = 3;
                        }
                        else if(gamedata.playercurrentactor == "Malika")
                        {
                            currentscene = 4;
                        }
                        else if(gamedata.playercurrentactor == "Zaki")
                        {
                            currentscene = 5;
                        }
                        else
                        {
                            Debug.Log("The game can´t guide the player on this scene: " + currentscene + scenes[currentscene].scenename);
                        }
                    }
                }
                break;
        }
    }
    #endregion

    #region Time Adjust Methods
    //Method that frezze game on the start of wich new scene so the game not pass direct trougth the scene
    void TimeToSetScene()
    {
        SceneBehaviour currentscenebehaviour;

        if (scenes[currentscene].GetComponent<SceneBehaviour>() != null)
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
        else
        {
            scenes[currentscene].gameObject.AddComponent<SceneBehaviour>();
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
            currentscenebehaviour.timertostart = 5;
        }

        if (currentscenebehaviour != null)
        {
            CheckTimeToStartScene(currentscenebehaviour);
        }
        else { Debug.LogError("No SceneBehaviour on scene " + currentscene); }
    }
    //Method that check if is time to go to next scene
    void OnTimeEndGoTo(int SceneToGo)
    {
        SceneBehaviour currentscenebehaviour;

        if (scenes[currentscene].GetComponent<SceneBehaviour>() != null)
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
        else
        {
            scenes[currentscene].gameObject.AddComponent<SceneBehaviour>();
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
            currentscenebehaviour.timertogo = 200;
        }

        if(currentscenebehaviour != null)
        {
            CheckTimeEndSceneAndGoToNext(currentscenebehaviour, SceneToGo);
        }
        else { Debug.LogError("No SceneBehaviour on scene " + currentscene); }
    }

    #endregion

    #region Complete Game Methods

    void CompleteGameCheckAffinityUnlockEndAndPlayGameEnd()
    {
        Debug.Log("Current game has ended");
       /*
        SceneBehaviour currentscenebehaviour;

        if (scenes[currentscene].GetComponent<SceneBehaviour>() != null)
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
        else
        {
            scenes[currentscene].gameObject.AddComponent<SceneBehaviour>();
            currentscenebehaviour = scenes[currentscene].GetComponent<SceneBehaviour>();
            currentscenebehaviour.isfinalscene = CheckIsCurrentSceneAFinalScene();
        }
        
        if (currentscenebehaviour != null)
        {
            if(CheckIsFinalScene(currentscenebehaviour) == true)
            {
               gamedata.currentgameend = CheckFinalAffinityWithCurrentActor();
            }

        }
        else { Debug.LogError("No SceneBehaviour on scene " + currentscene); }
    */
    }

    #endregion

    #region Check Methods
    //Method that check if the current game data is diferent for the gamedata data so it can be save;
    bool CheckDataChanges()
    {
        if (player.playercurrentactor != "")
        {
            if (player.playercurrentactor == "Enzo")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentinventoryobjects != player.inventary

                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentenzoaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                    || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Isis")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentinventoryobjects != player.inventary
                    
                    || gamedata.playercurrentactor != player.playercurrentactor

                    || gamedata.currentisisaffinity != player.currentactoraffinity
                    
                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                    || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Benjamin")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentinventoryobjects != player.inventary
                    
                    || gamedata.playercurrentactor != player.playercurrentactor
                    
                    || gamedata.currentbenjaminaffinity != player.currentactoraffinity
                    
                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                    || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Malika")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentinventoryobjects != player.inventary
                    
                    || gamedata.playercurrentactor != player.playercurrentactor
                    
                    || gamedata.currentmalikaaffinity != player.currentactoraffinity
                    
                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                    || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                    return true;
                else { return false; }
            }
            if (player.playercurrentactor == "Zaki")
            {
                if (gamedata.playername != player.playername
                    || gamedata.playercurrentinventoryobjects != player.inventary

                    || gamedata.playercurrentactor != player.playercurrentactor
                    
                    || gamedata.currentzakiaffinity != player.currentactoraffinity

                    || gamedata.playercurrentscene != currentscene
                    || gamedata.playercurrenttextfile != dialogbox.currentdialog
                    || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                    || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                    || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                    return true;
                else { return false; }
            }
        }
        else
        {
            if (gamedata.playername != player.playername
                || gamedata.playercurrentinventoryobjects != player.inventary

                || gamedata.playercurrentactor != player.playercurrentactor

                || gamedata.playercurrentscene != currentscene
                || gamedata.playercurrenttextfile != dialogbox.currentdialog
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline
                || gamedata.currentscenestate != scenes[currentscene].scenestate.ToString()
                || gamedata.currentdialoganswerstate != dialogbox.hasanswered.ToString())
                return true;
        }

        return false;
    }
    //Method that check the final affinity with current actor or patner so the game can unlock the coresponding end with that actor
    int CheckFinalAffinityWithCurrentActor()
    {
        if (player.currentactoraffinity >= 100)
            return 2;
        else if (player.currentactoraffinity >= 50 && player.currentactoraffinity < 100)
            return 1;
        else
        {
            return 0;
        }
    }
    //Method that check from the current scene scenebahviour if is final scene
    bool CheckIsFinalScene(SceneBehaviour CurrentSceneBehaviour)
    {
        if (CurrentSceneBehaviour.isfinalscene)
            return true;
        else { return false; }
    }
    //Method that will check if the time to end loading and it behaviour
    bool CheckLoadingEnd()
    {
        if (loadinginterface.loadingtime > 0)
            return true;
        else { return false; }
    }
    //Method that check if the time to start the current scene is 0 
    void CheckTimeToStartScene(SceneBehaviour CurrentSceneBehaviour)
    {
        if (CurrentSceneBehaviour.timertostart > 0)
        {
            CurrentSceneBehaviour.timertostart--;
            if (canprogress == true)
                canprogress = false;
            Debug.Log("Tempo para liberar progressão " + CurrentSceneBehaviour.timertostart);
        }
        else
        {
            if (canprogress == false)
                canprogress = true;
            Debug.Log("Scene Ready");
        }
    }
    //Method that check if is time to go to next scene
    void CheckTimeEndSceneAndGoToNext(SceneBehaviour CurrentSceneBehaviour,int SceneToGo)
    {
        if (canprogress && CurrentSceneBehaviour.timertogo > 0)
        {
            CurrentSceneBehaviour.timertogo--;
            Debug.Log("Tempo para ir para proxima scene " + CurrentSceneBehaviour.timertogo);
        }
        else
        {
            currentscene = SceneToGo;
            dialogbox.StartDialog(0);
            Debug.Log("You are on the Scene " + currentscene);
        }
    }
    
    #endregion

    #region SaveData Methods
    //Method that read all current game data and adjust it to be save the gamedata of the game saving it  in a correct way
    void SaveAllGameAndPlayerDataOfCurrentGame()
    {
        gamedata.issaving = CheckDataChanges();
        if(gamedata.issaving)
        {
            gamedata.playername = player.playername;
            gamedata.playercurrentinventoryobjects = player.inventary;

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
            gamedata.currentdialoganswerstate = dialogbox.hasanswered.ToString();

            gamedata.currentgameend = currentgameend;

            gamedata.gameprogress = currentscene+1;
            gamedata.SetData();
            gamedata.playtime = Time.time;

            gamedata.SaveAllPlayerData();
            gamedata.SaveAllGameData();
        }
    }
    #endregion

    #region LoadData Methods
    //Method load all gamedata data and adjust it to the current game
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

        //Check the game data player inventory has object in it and case as load it to the current game if not leave a message to developer
        if (gamedata.playercurrentinventoryobjects.Length > 0)
        {
            player.inventary = gamedata.playercurrentinventoryobjects;
        }
        else { Debug.LogWarning("The player has no object in inventory"); }

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

        //Update the game data to the current game play and put the values of the player current state in the history of the game
        currentscene = gamedata.playercurrentscene;
        dialogbox.currentdialog = gamedata.playercurrenttextfile;
        dialogbox.dialog.currentdialogline = gamedata.playercurrentdialogline;
        if (gamedata.currentscenestate == "dialog")
            scenes[currentscene].scenestate = Scene.state.dialog;
        if (gamedata.currentscenestate == "interaction")
            scenes[currentscene].scenestate = Scene.state.interaction;
        if (gamedata.currentscenestate == "puzzle")
            scenes[currentscene].scenestate = Scene.state.puzzle;
        if (gamedata.currentdialoganswerstate == "True")
            dialogbox.hasanswered = true;
        if (gamedata.currentdialoganswerstate == "False")
            dialogbox.hasanswered = false;
        //Get back the last gameend
        currentgameend = gamedata.currentgameend;

    }
    #endregion

    #endregion

    #endregion

    #endregion
}
