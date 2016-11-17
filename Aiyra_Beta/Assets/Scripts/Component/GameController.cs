using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class GameController : MonoBehaviour
{
    //Always set last dialog trougth this script

    #region Actors Reference Keys
    private const string unkownreference = "???";
    private const string juruparireference = "Jurupari";
    private const string motherreference = "Mãe";
    private const string teacherreference = "Professora";
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
    private const string isisendreference = "ISISEND" /*  + CGID*/;
    private const string benjaminendreference = "BENJAMINEND" /*  + CGID*/;
    #endregion

    #region Attributes

    public CollectionData gamecollection;
    public GameData gamedata;
    public GameSettings gamesettings;
    public MessageBox messagebox;
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
        if (messagebox == null)
            messagebox = GameObject.Find("MessageBox").GetComponent<MessageBox>();
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
    void Start()
    {

        SetScenesID();

        SetScenesName();

        SetFinalScenes();

        LoadAllGameAndPlayerDataToCurrentGame();

        ResetObjectIStatus();

        ResetPuzzleStatus();

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

        //testing
        PrepareActorExpressions();

        #endregion
        #region ObjectsI Control

        UpdateObjectIScene();

        UpdateObjectI();

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

        ScenesManager();

        #endregion
        #region Game Control

        //GoToGameSceneWhen();

        SaveAllGameAndPlayerDataOfCurrentGame();

        if (isgamefinal)
        {
            CompleteGameCheckAffinityUnlockEndAndPlayGameEnd();
        }
        /*
        isgamefinal = CheckIsFinalScene(scenes[currentscene].GetComponent<SceneBehaviour>());

        currentgameend = CheckFinalAffinityWithCurrentActor();

        if (isgamefinal)
        {
            CompleteGameCheckAffinityUnlockEndAndPlayGameEnd();
        }
        */

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
        #region Effect Camera Control

        AdjustEffectCameraToGameInterface();

        #endregion
        #region MessageBox Control

        MessageBoxManager();

        #endregion
        #region LoadingInterface Control

        LoadingInterfaceManager();

        #endregion
    }
    #endregion

    #region GameController Fundamental Methods

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
            if (background.currentbackground > scenes[currentscene].backgrounds.Length)
                background.currentbackground = 0;
            background.backgroundimage.sprite = scenes[currentscene].backgrounds[background.currentbackground];
        }

        #region Background change lines
        //Method that check the scene and it current dialog to change the background according to it 
        switch (currentscene)
        {
            case 3:
                if (dialogbox.currentdialog == 0 && dialogbox.dialog.currentdialogline >= 16)
                {
                    background.currentbackground = 1;
                }
                if(dialogbox.currentdialog == 1 && dialogbox.dialog.currentdialogline >= 0)
                {
                    background.currentbackground = 2;
                }
                break;
            case 6:
                if(dialogbox.currentdialog == 0 && dialogbox.dialog.currentdialogline >= 45)
                {
                    background.currentbackground = 3;
                }
                if(dialogbox.currentdialog == 1 && dialogbox.dialog.currentdialogline >= 57)
                {
                    background.currentbackground = 3;
                }
                if(dialogbox.currentdialog == 2)
                {
                    background.currentbackground = 3;
                }
                if(dialogbox.currentdialog == 3)
                {
                    background.currentbackground = 3;
                }
                break;
            default:
                //check if is a scene that have a lightswitch to do not give a conflict between the scripts: objectI script and gamecontroller script
                if(currentscene != 1)
                    background.currentbackground = 0;
                break;
        }
        #endregion
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

        #region Music change lines

        switch (currentscene)
        {
            case 1:
                musicplayer.musicplayerbehaviour = MusicPlayer.MusicPlayerBehaviour.justone;
                break;
            default:
                musicplayer.musicplayerbehaviour = MusicPlayer.MusicPlayerBehaviour.random;
                break;
        }

        #endregion
    }

    #endregion

    #region Dialog Methods

    #region Set Speaker(Current Actor that is Speaking) Methods

    //Method that update the speaker box of the dialog box changing the name of the speaker to the current and correct actor name
    void UpdateSpeaker()
    {
        switch (currentscene)
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
                else if (dialogbox.currentdialog == 1)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if (dialogbox.currentdialog == 2)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if (dialogbox.currentdialog == 3)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                else if (dialogbox.currentdialog == 4)
                {
                    dialogbox.SetSpeakerName(juruparireference);
                }
                break;
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if (dialogbox.currentdialog == 1)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if (dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline != 10)
                        dialogbox.SetSpeakerName(player.playername);
                    else { dialogbox.SetSpeakerName(motherreference); }
                }

                break;
            case 2:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 0 
                     || dialogbox.dialog.currentdialogline == 4 
                     || dialogbox.dialog.currentdialogline == 5)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 1)
                {
                    if (dialogbox.dialog.currentdialogline == 1
                     || dialogbox.dialog.currentdialogline == 2
                     || dialogbox.dialog.currentdialogline == 3
                     || dialogbox.dialog.currentdialogline == 4
                     || dialogbox.dialog.currentdialogline == 6
                     || dialogbox.dialog.currentdialogline == 7
                     || dialogbox.dialog.currentdialogline == 8)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline == 0
                     || dialogbox.dialog.currentdialogline == 1
                     || dialogbox.dialog.currentdialogline == 2
                     || dialogbox.dialog.currentdialogline == 3
                     || dialogbox.dialog.currentdialogline == 4
                     || dialogbox.dialog.currentdialogline == 5
                     || dialogbox.dialog.currentdialogline == 6
                     || dialogbox.dialog.currentdialogline == 7
                     || dialogbox.dialog.currentdialogline == 8
                     || dialogbox.dialog.currentdialogline == 9)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 3)
                {
                    if (dialogbox.dialog.currentdialogline == 0
                     || dialogbox.dialog.currentdialogline == 1
                     || dialogbox.dialog.currentdialogline == 2
                     || dialogbox.dialog.currentdialogline == 3
                     || dialogbox.dialog.currentdialogline == 4
                     || dialogbox.dialog.currentdialogline == 6
                     || dialogbox.dialog.currentdialogline == 7
                     || dialogbox.dialog.currentdialogline == 8
                     || dialogbox.dialog.currentdialogline == 9)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 4)
                {
                    if (dialogbox.dialog.currentdialogline == 0
                     || dialogbox.dialog.currentdialogline == 1
                     || dialogbox.dialog.currentdialogline == 3
                     || dialogbox.dialog.currentdialogline == 4
                     || dialogbox.dialog.currentdialogline == 5
                     || dialogbox.dialog.currentdialogline == 6
                     || dialogbox.dialog.currentdialogline == 7
                     || dialogbox.dialog.currentdialogline == 8)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 5)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if (dialogbox.currentdialog == 6)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if (dialogbox.currentdialog == 7)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                if (dialogbox.currentdialog == 8)
                {
                    if (dialogbox.dialog.currentdialogline == 7
                        || dialogbox.dialog.currentdialogline == 13
                        || dialogbox.dialog.currentdialogline == 14
                        || dialogbox.dialog.currentdialogline == 16
                        || dialogbox.dialog.currentdialogline == 20
                        || dialogbox.dialog.currentdialogline == 21
                        || dialogbox.dialog.currentdialogline == 23
                        || dialogbox.dialog.currentdialogline == 24
                        || dialogbox.dialog.currentdialogline == 26
                        || dialogbox.dialog.currentdialogline == 27
                        || dialogbox.dialog.currentdialogline == 28
                        || dialogbox.dialog.currentdialogline == 30
                        || dialogbox.dialog.currentdialogline == 31
                        || dialogbox.dialog.currentdialogline == 32
                        || dialogbox.dialog.currentdialogline == 33
                        || dialogbox.dialog.currentdialogline == 34
                        || dialogbox.dialog.currentdialogline == 35
                        || dialogbox.dialog.currentdialogline == 36
                        || dialogbox.dialog.currentdialogline == 37
                        || dialogbox.dialog.currentdialogline == 38
                        || dialogbox.dialog.currentdialogline == 40
                        || dialogbox.dialog.currentdialogline == 41
                        || dialogbox.dialog.currentdialogline == 53
                        || dialogbox.dialog.currentdialogline == 58)
                        dialogbox.SetSpeakerName(motherreference);
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if (dialogbox.currentdialog == 9
                || dialogbox.currentdialog == 11
                || dialogbox.currentdialog == 12
                || dialogbox.currentdialog == 13
                || dialogbox.currentdialog == 14)
                {
                    dialogbox.SetSpeakerName(player.playername);
                }
                break;
            case 3:
                if (dialogbox.currentdialog == 0)
                {
                    //Cenario Rua
                    if (dialogbox.dialog.currentdialogline == 22
                      ||dialogbox.dialog.currentdialogline == 27)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if (dialogbox.currentdialog == 1)
                {
                    //Seguir Jurupari
                    if (dialogbox.dialog.currentdialogline == 6
                      ||dialogbox.dialog.currentdialogline == 7
                      ||dialogbox.dialog.currentdialogline == 9
                      ||dialogbox.dialog.currentdialogline == 10)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if (dialogbox.currentdialog == 2)
                {
                    //Não seguir Jurupari
                    if (dialogbox.dialog.currentdialogline == 4
                      || dialogbox.dialog.currentdialogline == 5
                      || dialogbox.dialog.currentdialogline == 12
                      || dialogbox.dialog.currentdialogline == 13
                      || dialogbox.dialog.currentdialogline == 22
                      || dialogbox.dialog.currentdialogline == 23)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                break;
            case 4:
                if (dialogbox.currentdialog == 0)
                {
                    //Sala de aula normal
                    if (dialogbox.dialog.currentdialogline == 6
                      || dialogbox.dialog.currentdialogline == 7
                      || dialogbox.dialog.currentdialogline == 11
                      || dialogbox.dialog.currentdialogline == 16)
                    {
                        dialogbox.SetSpeakerName(teacherreference);
                    }
                    else if(dialogbox.dialog.currentdialogline == 17)
                    {
                        dialogbox.SetSpeakerName("Colegas");
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                break;
            case 5:
                //Rua da casa abandonada volta da sala de aula
                dialogbox.SetSpeakerName(player.playername);
                break;
            case 6:
                if (dialogbox.currentdialog == 0)
                {
                    //cena Jardim continuação do seguir jurupari
                    if (dialogbox.dialog.currentdialogline == 5
                     || dialogbox.dialog.currentdialogline == 9
                     || dialogbox.dialog.currentdialogline == 13
                     || dialogbox.dialog.currentdialogline == 17
                     || dialogbox.dialog.currentdialogline == 18
                     || dialogbox.dialog.currentdialogline == 21
                     || dialogbox.dialog.currentdialogline == 22
                     || dialogbox.dialog.currentdialogline == 23
                     || dialogbox.dialog.currentdialogline == 25
                     || dialogbox.dialog.currentdialogline == 28
                     || dialogbox.dialog.currentdialogline == 29
                     || dialogbox.dialog.currentdialogline == 31
                     || dialogbox.dialog.currentdialogline == 33
                     || dialogbox.dialog.currentdialogline == 35
                     || dialogbox.dialog.currentdialogline == 37
                     || dialogbox.dialog.currentdialogline == 38
                     || dialogbox.dialog.currentdialogline == 41
                     || dialogbox.dialog.currentdialogline == 42
                     || dialogbox.dialog.currentdialogline == 46
                     || dialogbox.dialog.currentdialogline == 51
                     || dialogbox.dialog.currentdialogline == 52)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 1)
                {
                    //cena Jardim continuação do nao seguir jurupari
                    if (dialogbox.dialog.currentdialogline == 5
                     || dialogbox.dialog.currentdialogline == 11
                     || dialogbox.dialog.currentdialogline == 15
                     || dialogbox.dialog.currentdialogline == 18
                     || dialogbox.dialog.currentdialogline == 19
                     || dialogbox.dialog.currentdialogline == 23
                     || dialogbox.dialog.currentdialogline == 24
                     || dialogbox.dialog.currentdialogline == 27
                     || dialogbox.dialog.currentdialogline == 28
                     || dialogbox.dialog.currentdialogline == 29
                     || dialogbox.dialog.currentdialogline == 30
                     || dialogbox.dialog.currentdialogline == 32
                     || dialogbox.dialog.currentdialogline == 35
                     || dialogbox.dialog.currentdialogline == 36
                     || dialogbox.dialog.currentdialogline == 37
                     || dialogbox.dialog.currentdialogline == 39
                     || dialogbox.dialog.currentdialogline == 40
                     || dialogbox.dialog.currentdialogline == 43
                     || dialogbox.dialog.currentdialogline == 44
                     || dialogbox.dialog.currentdialogline == 48
                     || dialogbox.dialog.currentdialogline == 50
                     || dialogbox.dialog.currentdialogline == 51
                     || dialogbox.dialog.currentdialogline == 55
                     || dialogbox.dialog.currentdialogline == 56
                     || dialogbox.dialog.currentdialogline == 58
                     || dialogbox.dialog.currentdialogline == 63
                     || dialogbox.dialog.currentdialogline == 64)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if(dialogbox.currentdialog == 2)
                {
                    //cena Jardim resposta entrar no portal
                    if (dialogbox.dialog.currentdialogline == 24
                     || dialogbox.dialog.currentdialogline == 25
                     || dialogbox.dialog.currentdialogline == 26)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                if (dialogbox.currentdialog == 3)
                {
                    //cena Jardim resposta não entrar no portal
                    if (dialogbox.dialog.currentdialogline == 25
                     || dialogbox.dialog.currentdialogline == 26
                     || dialogbox.dialog.currentdialogline == 27
                     || dialogbox.dialog.currentdialogline == 29
                     || dialogbox.dialog.currentdialogline == 30
                     || dialogbox.dialog.currentdialogline == 31)
                    {
                        dialogbox.SetSpeakerName(juruparireference);
                    }
                    else
                    {
                        dialogbox.SetSpeakerName(player.playername);
                    }
                }
                break;
            case 7:
                //Cena Portal explosão
                dialogbox.SetSpeakerName(player.playername);
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
                    dialogbox.currentdialoganswers = 0;
                    if (dialogbox.dialog.currentdialogline == 14)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                if (dialogbox.currentdialog == 1)
                {
                    dialogbox.currentdialoganswers = 1;
                    if (dialogbox.dialog.currentdialogline == 14)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                if (dialogbox.currentdialog == 2)
                {
                    dialogbox.currentdialoganswers = 2;
                    if (dialogbox.dialog.currentdialogline == 8)
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
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 4)
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
                    if (dialogbox.dialog.currentdialogline == 6)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                if (dialogbox.currentdialog == 10)
                {
                    if (dialogbox.dialog.currentdialogline >= 1)
                    {
                        dialogbox.currentdialoganswers = 1;
                        if (!dialogbox.hasanswered)
                        {
                            dialogbox.dialog.isanswerssplit = false;
                            dialogbox.dialog.isanswermoment = true;
                        }
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                #region obsolety
                /*
                else if (dialogbox.currentdialog == 7)
                {
                    if(dialogbox.dialog.currentdialogline == 49)
                    {
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                */
                #endregion
                break;
            case 3:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 33)
                    {
                        dialogbox.currentdialoganswers = 0;
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                break;
            case 6:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline == 53)
                    {
                        dialogbox.currentdialoganswers = 0;
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                if(dialogbox.currentdialog == 1)
                {
                    if (dialogbox.dialog.currentdialogline == 65 )
                    {
                        dialogbox.currentdialoganswers = 0;
                        if (!dialogbox.hasanswered)
                            dialogbox.dialog.isanswermoment = true;
                    }
                    else
                    {
                        dialogbox.hasanswered = false;
                    }
                }
                break;
                #region Obsolety
                /*
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
                */
                #endregion
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
                dialogbox.AnswerButtonsSetNextCG("", "", "");
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
                    // not usuing answersresultreaded = new bool[3];
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.lastanswerid < 0)
                    {
                        dialogbox.nextdialog = 5;
                    }
                }
                else if (dialogbox.currentdialog == 1)
                {
                    dialogbox.AnswerButtonsSetNextDialog(2, 1, 3);
                    if (dialogbox.lastanswerid < 0)
                    {
                        dialogbox.nextdialog = 5;
                    }
                }
                else if (dialogbox.currentdialog == 2)
                {
                    dialogbox.AnswerButtonsSetNextDialog(3, 2, 1);
                    if (dialogbox.lastanswerid < 0)
                    {
                        dialogbox.nextdialog = 5;
                    }
                }
                else if (dialogbox.currentdialog == 3)
                {
                    dialogbox.AnswerButtonsSetNextDialog(0, 0, 0);
                    dialogbox.nextdialog = 4;

                }
                else
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                    {
                        dialogbox.nextdialog = 5;
                    }
                    else
                    {
                        dialogbox.nextdialog = dialogbox.currentdialog;
                    }
                }
                #region Obslety
                /*
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
                */
                #endregion
                break;
            case 1:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline < 4)
                        dialogbox.nextdialog = dialogbox.endatdialog;

                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 0);
                }
                if (dialogbox.currentdialog == 1 || dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline <= dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if(dialogbox.currentdialog >= 3 && dialogbox.currentdialog <= 11)
                {
                    dialogbox.nextdialog = dialogbox.currentdialog;
                }
                break;
            case 2:
                if (dialogbox.currentdialog == 0 || dialogbox.currentdialog == 4)
                {
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if (dialogbox.currentdialog == 1 || dialogbox.currentdialog == 2 || dialogbox.currentdialog == 3)
                {
                    dialogbox.nextdialog = 4;
                }
                if (dialogbox.currentdialog == 10)
                {
                    dialogbox.AnswerButtonsSetNextDialog(6, 7, 8);
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if (dialogbox.currentdialog == 6 || dialogbox.currentdialog == 7)
                {
                    dialogbox.nextdialog = 8;
                }
                if (dialogbox.currentdialog == 8)
                {
                    //dialogbox.AnswerButtonsSetNextDialog(5,6,8);
                    if (dialogbox.dialog.currentdialogline != 60)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else
                    {
                        dialogbox.nextdialog = dialogbox.currentdialog;
                    }
                }
                if(dialogbox.currentdialog == 9
                || dialogbox.currentdialog == 11
                || dialogbox.currentdialog == 12
                || dialogbox.currentdialog == 13
                || dialogbox.currentdialog == 14)
                {
                    dialogbox.nextdialog = dialogbox.currentdialog;
                }
                break;
            case 3:
                if (dialogbox.currentdialog == 0)
                {
                    dialogbox.AnswerButtonsSetNextDialog(1, 2, 3);
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.nextdialog = 1;
                }
                if(dialogbox.currentdialog == 1)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                if (dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
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
                    dialogbox.AnswerButtonsSetNextDialog(2, 3, 4);
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if(dialogbox.currentdialog == 1)
                {
                    dialogbox.AnswerButtonsSetNextDialog(2, 3, 4);
                    if (dialogbox.lastanswerid < 0)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                }
                if (dialogbox.currentdialog == 2)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                if(dialogbox.currentdialog == 3)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 7:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != 7)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 8:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 9:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
                    else { dialogbox.nextdialog = dialogbox.currentdialog; }
                }
                break;
            case 10:
                if (dialogbox.currentdialog == 0)
                {
                    if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                        dialogbox.nextdialog = dialogbox.endatdialog;
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
            if (!pausemenu.hideactors /* Need to watch the second condition*/&& dialogbox.gameObject.activeInHierarchy)
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
                        /*
                        //test ok
                        case 9:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[1] { 0 };
                                    break;
                            }
                            break;
                            */
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
                        /* test not ok 
                        case 9:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[1] { 0 };
                                    break;
                            }
                            break;
                            /**/
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
                case "Isis":
                    switch (currentscene)
                    {
                        /* test ok
                        8
                        case 9:
                            switch(dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[1] { 0 };
                                    break;
                            }
                            break;
                    */
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Isis lines");
                                actors[i].dialoglines = new int[0];
                            }
                            break;
                    }
                    break;
                #endregion
                #region Jurupari Lines
                case "Jurupari":
                    switch (currentscene)
                    {
                        case 0:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
                                    break;
                                case 1:
                                    actors[i].dialoglines = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                                    break;
                                case 2:
                                    actors[i].dialoglines = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                                    break;
                                case 3:
                                    actors[i].dialoglines = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                                    break;
                                case 4:
                                    actors[i].dialoglines = new int[5] { 0, 1, 2, 3, 4 };
                                    break;
                                default:
                                    actors[i].dialoglines = new int[0];
                                    break;
                            }
                            break;
                        case 3:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    //deixada na escola
                                    actors[i].dialoglines = new int[14] { 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
                                    break;
                                case 1:
                                    //seguir jurupari
                                    actors[i].dialoglines = new int[25] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
                                    break;
                                case 2:
                                    //Não seguir jurupari
                                    actors[i].dialoglines = new int[27] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
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
                        case 6:
                            switch(dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[50] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                                    break;
                                case 1:
                                    actors[i].dialoglines = new int[61] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65 };
                                    break;
                                case 2:
                                    actors[i].dialoglines = new int[29] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
                                    break;
                                case 3:
                                    actors[i].dialoglines = new int[40] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39};
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
                #region Klaus Lines
                case "Klaus":
                    switch (currentscene)
                    {
                        /* test ok
                        case 9:
                            switch(dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[1] { 0 };
                                    break;
                            }
                            break;
                    */
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("Doing Default of Isis lines");
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
                                    actors[i].dialoglines = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
                                    break;
                                case 1:
                                    actors[i].dialoglines = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                    break;
                                case 2:
                                    actors[i].dialoglines = new int[11] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                                    break;
                                case 3:
                                    actors[i].dialoglines = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                    break;
                                case 4:
                                    actors[i].dialoglines = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                    break;
                                case 8:
                                    actors[i].dialoglines = new int[50] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56 };
                                    break;
                                default:
                                    Debug.Log("Doing Default of Mae da Personagem lines");
                                    actors[i].dialoglines = new int[0];
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
                #region Teacher Lines
                case "Teacher":
                    switch (currentscene)
                    {
                        case 4:
                            switch (dialogbox.currentdialog)
                            {
                                case 0:
                                    actors[i].dialoglines = new int[23] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                                    break;
                            }
                            break;
                        default:
                            if (actors[i].dialoglines.Length > 0)
                            {
                                Debug.Log("DOing Default Of Isis lines");
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
                                case 9:
                                    switch (dialogbox.dialog.currentdialogline)
                                    {
                                        case 0:
                                            actors[i].actoranimator.SetInteger(actors[i].motionreference, 7);
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
                                //test not ok
                                /*
                                case 9:
                                    switch(dialogbox.dialog.currentdialogline)
                                    {
                                        case 0:
                                            //actors[i].actormodel.motionfile = Resources.Load<TextAsset>("Actors/Enzo/motions/Angry.mtnb");
                                            //actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                            break;
                                    }
                                    break;
                                    /**/
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
                    case "Isis":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                /*test ok
                                case 9:
                                    switch(dialogbox.currentdialog)
                                    {
                                        case 0:
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 7);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                    */
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
                            Debug.Log("Actor " + actors[i].actorname + "Not Active gamecontroller will not set it animation");
                        }
                        break;
                    #endregion
                    #region Jurupari Emotions
                    case "Jurupari":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 0://ok
                                    switch (dialogbox.currentdialog)
                                    {
                                        //texto sonho com indizinho
                                        case 0://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 1:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 10:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 12:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;
                                        case 1://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //pergunta sobre dimensões
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 3:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 9:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 10:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 12:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 14:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;
                                        case 2://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //pergunta quem é jurupari
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 3:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;
                                        case 3://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //pergunta onde estou
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 1:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;
                                        case 4://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //despedida sonho
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 1:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 3://ok
                                    switch (dialogbox.currentdialog)
                                    {
                                        case 0://ok
                                            //chegar na escola
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 3:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 22:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 24:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 27:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                            }
                                            break;
                                        case 1://Ok
                                            //seguir jurupari
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 18:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                            }
                                            break;
                                        case 2://ok
                                            //não seguir jurupari
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                //terminar
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 10:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 12:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 17:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 23:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 6://ok
                                    switch(dialogbox.currentdialog)
                                    {
                                        case 0://ok
                                            //continuação seguir jurupari
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 9:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 13:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 17:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 21:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 23:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 28:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 29:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 33:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 37:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 41:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 42:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 46:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 51:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                            }
                                            break;
                                    case 1://ok
                                        //continuar se soltar do jurupari
                                        switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 15:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 21:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 23:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 27:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 30:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 35:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 37:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 43:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 50:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 55:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 56:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 58:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 6);
                                                    break;
                                                case 63:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                            }
                                            break;
                                        case 2://ok
                                            //10 entrar no portal
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 20:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 24:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                            }
                                            break;
                                        case 3://ok
                                            //11 nao entrar no portal
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 20:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 25:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 31:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
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
                    #region Klaus Emotions
                    case "Klaus":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                /* test ok
                                case 9:
                                    switch(dialogbox.currentdialog)
                                    {
                                        case 0:
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                    */
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
                            Debug.Log("Actor " + actors[i].actorname + "Not Active gamecontroller will not set it animation");
                        }
                        break;
                    #endregion
                    #region Mae Do jogador Emotions
                    case "MaedaLuna"://ok
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 2:
                                    switch (dialogbox.currentdialog)
                                    {
                                        case 0://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //Bom dia mae do jogador
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 1:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;

                                        case 1://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //TO animada
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 3:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                            }
                                            break;
                                        case 2://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //nao sabe muito bem
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 4:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                            }
                                            break;
                                        case 3://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //Nao esta animada
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 3:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 9:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                            }
                                            break;
                                        case 4://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //Mae oferece o cafe da manha e fala do presente
                                                case 0:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 2:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 5:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
                                            break;

                                        case 8://ok
                                            switch (dialogbox.dialog.currentdialogline)
                                            {
                                                //Mae comenta sobre a historia da luna
                                                case 7:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 13:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 16:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 17:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 20:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 21:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 22:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 23:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 28:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 29:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 5);
                                                    break;
                                                case 30:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 32:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 35:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 37:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 39:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 40:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                                case 42:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 53:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 58:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 4);
                                                    break;
                                            }
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
                    #region Teacher Emotions
                    case "Teacher":
                        if (actors[i].gameObject.activeInHierarchy)
                        {
                            switch (currentscene)
                            {
                                case 4:
                                    switch (dialogbox.currentdialog)
                                    {
                                        case 0:
                                            switch(dialogbox.dialog.currentdialogline)
                                            {
                                                case 1:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 3);
                                                    break;
                                                case 6:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 8:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 11:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 2);
                                                    break;
                                                case 13:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                                case 16:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 1);
                                                    break;
                                                case 19:
                                                    actors[i].actoranimator.SetInteger(actors[i].motionreference, 0);
                                                    break;
                                            }
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
                            Debug.Log("Actor " + actors[i].actorname + "Not Active gamecontroller will not set it animation");
                        }
                        break;
                        #endregion
                }
            }
        }
    }

    #endregion

    #region ObjectI Methods
    void ResetObjectIStatus()
    {
        //Check if the player has start a new game
        if (player.playername.Length <= 0 && player.playercurrentactor.Length <= 0)
        {
            //If the player has start a new game start a whip of type for to run all scenes and another of type forreach to run througth all objects in the scene
            for (int i = 0; i < scenes.Length; i++)
                foreach (GameObject objecti in scenes[i].objects)
                {
                    //Check if the object status save key has any letter in it to save the object status
                    if (objecti.GetComponent<ObjectI>().objectinventarystatussavekey.Length <= 0)
                        objecti.GetComponent<ObjectI>().UploadObjectISaveKey();
                    //Reset the value of object based on it name and save it
                    if (objecti.gameObject.name == "Broche")
                    {
                        objecti.GetComponent<ObjectI>().isininventary = 0;
                    }
                    if(objecti.gameObject.name == "LivingroomDiaryBox")
                    {
                        objecti.GetComponent<ObjectI>().isininventary = 0;
                    }
                    if(objecti.gameObject.name != "")
                    {
                        objecti.GetComponent<ObjectI>().isininventary = 0;
                    }

                    objecti.GetComponent<ObjectI>().SaveObjectStatus();
                }
        }
    }
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
            if (i != currentscene)
                foreach (GameObject objecti in scenes[i].objects)
                    objecti.SetActive(false);

        foreach (GameObject objecti in scenes[currentscene].objects)
            if (objecti.GetComponent<ObjectI>().isavailable)
                objecti.SetActive(true);
    }
    void UpdateObjectIVolume()
    {
        foreach (GameObject objecti in scenes[currentscene].objects)
            if(objecti.GetComponent<ObjectI>().objectsound != null)
                if (objecti.GetComponent<ObjectI>().objectsound.volume != gamesettings.effectsvolume)
                    objecti.GetComponent<ObjectI>().objectsound.volume = gamesettings.effectsvolume;
    }
    void UpdateObjectI()
    {
        foreach (GameObject objecti in scenes[currentscene].objects)
        {
            //Check if the objecti status save key has any letter in it to save the objecti status
            if (objecti.GetComponent<ObjectI>().objectinventarystatussavekey.Length <= 0)
                objecti.GetComponent<ObjectI>().UploadObjectISaveKey();

            //Check if objectI has not be loaded to load it
            if (!objecti.GetComponent<ObjectI>().hasbeenloaded)
            {
                objecti.GetComponent<ObjectI>().LoadObjectStatus();
                objecti.GetComponent<ObjectI>().hasbeenloaded = true;
            }
            //Check if objectI has been resolved to save it status
            if (objecti.GetComponent<ObjectI>().isininventary < 0)
            {
                objecti.GetComponent<ObjectI>().SaveObjectStatus();
                objecti.GetComponent<ObjectI>().isavailable = false;
                objecti.gameObject.SetActive(false);
            }

            //Update objecti self life
            objecti.GetComponent<ObjectI>().UpdateObjectIShelfLife();
        }
    }
    #endregion

    #region Puzzle Methods
    //Method that reset the puzzle status for a new gameplay
    void ResetPuzzleStatus()
    {
        //Check if the player has start a new game
        if (player.playername.Length <= 0 && player.playercurrentactor.Length <= 0)
        {

            //If the player has start a new game start a whip of type for to run all scenes and another of type forreach to run througth all puzzle in the scene
            for (int i = 0; i < scenes.Length; i++)
                foreach (Puzzle puzzle in scenes[i].puzzles)
                {
                    //Check if the puzzle status save key has any letter in it to save the puzzle status
                    if (puzzle.puzzlestatussavekey.Length <= 0)
                        puzzle.UploadPuzzleSaveKey();
                    //Reset the resolution of the puzzle and save
                    puzzle.resolved = false;
                    puzzle.SavePuzzleStatus();
                }
        }
    }
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
            if (puzzlei.puzzlestatussavekey.Length <= 0)
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
                if (puzzlei.GetComponent<PuzzlePortalFall>() != null)
                    puzzlei.GetComponent<PuzzlePortalFall>().UpdateStunStatus();
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
                case 7: scenes[i].scenename = "Portal"; break;
                case 8: scenes[i].scenename = "QuedaPortalBenjamin"; break;
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
                case 7: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 8: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 9: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 10: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 11: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 12: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 13: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 14: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 15: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 16: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
                case 17: scenes[i].GetComponent<SceneBehaviour>().isfinalscene = false; break;
            }
            Debug.Log("Scene " + scenes[i].scenename + " is a final scene?: " + scenes[i].GetComponent<SceneBehaviour>().isfinalscene);
        }
    }
    //Method that manager all changes, events and conditions to happen on the scene
    void ScenesManager()
    {
        switch (currentscene)
        {
            case 0:
                //Check if the current dialog is 0 and current line 6 so the game can guide player to game scene 9
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
                                if (scenebehaviour.timertogo <= 0)
                                {
                                    Application.LoadLevel(9);
                                }
                                else
                                {
                                    scenebehaviour.timertogo--;
                                    if (canprogress == true)
                                        canprogress = false;
                                }
                            }
                            else
                            {
                                dialogbox.dialog.currentdialogline += 1;
                                scenebehaviour.timertogo = 100;
                            }
                        }
                    }
                }
                OnDialogEndGoTo(1);
                break;
            case 3:
                if (dialogbox.currentdialog == 1)
                {
                    dialogbox.lastdialog = 1;
                    OnDialogEndGoTo(6);
                }
                if (dialogbox.currentdialog == 2)
                {
                    dialogbox.lastdialog = 2;
                    OnDialogEndGoTo(4);
                }
                break;
            case 5:
                TimeToSetScene();
                OnDialogEndGoTo(6);
                break;
            case 6:
                TimeToSetScene();
                if (dialogbox.lastdialog == 1)
                {
                    dialogbox.StartDialog(0);
                    dialogbox.lastdialog = -1;
                }
                if (dialogbox.lastdialog == 2)
                {
                    dialogbox.StartDialog(1);
                    dialogbox.lastdialog = -1;
                }
                if (dialogbox.currentdialog == 2 || dialogbox.currentdialog == 3)
                {
                    if (gamedata.playercurrentactor.Length <= 0)
                    {
                        if (dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                            if (!gamedata.issaving || !gamedata.isloading)
                                Application.LoadLevel(8);
                    }
                    else{ dialogbox.Processed() ; OnDialogEndGoTo(7); }
                }
                #region Obsolety
                /*
                if (scenes[currentscene].backgrounds.Length > 1)
                {
                    if (scenes[currentscene].GetComponent<SceneBehaviour>().timertogo < 170)
                        background.currentbackground = 1;
                    else if (scenes[currentscene].GetComponent<SceneBehaviour>().timertogo < 120)
                        background.currentbackground = 2;

                }
                if (dialogbox.dialog.currentdialogline >= 0)
                {
                    //check if player has a actor selected so the game can put him the portal and in the scene were the player can select the actor
                    if (gamedata.playercurrentactor.Length <= 0)
                    {
                        //Check if game game is not saving or loading to go to next scene
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            if (scenes[currentscene].GetComponent<SceneBehaviour>().timertogo < 0)
                            {
                                //Save Game Status to load it to another scene
                                gamedata.SaveAllPlayerData();
                                gamedata.SaveAllGameData();
                                //Load Actor selection scene
                                Application.LoadLevel(8);
                            }
                            else
                            {
                                scenes[currentscene].GetComponent<SceneBehaviour>().timertogo--;
                                Debug.Log("Tempo para proceguir: " + scenes[currentscene].GetComponent<SceneBehaviour>().timertogo);
                            }
                        }
                    }
                    else
                    {
                        currentscene = 7;
                        dialogbox.StartDialog(0);
                    }
                }
                */
                #endregion
                break;
            case 7:
                //Update the timer to start the scene
                TimeToSetScene();
                //Check wich actor the player has choice to start the actor history line
                if (gamedata.playercurrentactor == "Benjamin")
                {
                    //Check if game game is not saving or loading to go to next scene
                    if (!gamedata.issaving || !gamedata.isloading)
                    {
                        //Load Actor selection scene
                        OnDialogEndGoTo(8);
                        //Auto processed text
                        if (dialogbox.currentdialog == dialogbox.endatdialog)
                        {
                            Debug.Log("Doing Auto Processed on text after loading interface");
                            dialogbox.Processed();
                        }
                    }
                }
                if (gamedata.playercurrentactor == "Enzo")
                {
                    //Check if game game is not saving or loading to go to next scene
                    if (!gamedata.issaving || !gamedata.isloading)
                    {
                        //Load Actor selection scene
                        OnDialogEndGoTo(13);
                        //Auto processed text
                        if (dialogbox.currentdialog == dialogbox.endatdialog)
                        {
                            Debug.Log("Doing Auto Processed on text after loading interface");
                            dialogbox.Processed();
                        }
                    }
                }
                if (gamedata.playercurrentactor == "Isis")
                {
                    //Check if game game is not saving or loading to go to next scene
                    if (!gamedata.issaving || !gamedata.isloading)
                    {
                        //Load Actor selection scene
                        OnDialogEndGoTo(17);
                        //Auto processed text
                        if (dialogbox.currentdialog == dialogbox.endatdialog)
                        {
                            Debug.Log("Doing Auto Processed on text after loading interface");
                            dialogbox.Processed();
                        }
                    }
                }
                #region Obsolety
                /*
                scenes[currentscene].scenestate = Scene.state.interaction;
                //Check if the timer is lower than 0 to go to next scene
                if (scenes[currentscene].GetComponent<SceneBehaviour>().timertogo < 0)
                {
                    if (gamedata.playercurrentactor == "Benjamin")
                    {
                        //Check if game game is not saving or loading to go to next scene
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            //Load Actor selection scene
                            currentscene = 8;
                            dialogbox.StartDialog(0);
                        }
                    }
                    else if (gamedata.playercurrentactor == "Enzo")
                    {
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            //Load Actor selection scene
                            currentscene = 9;
                            dialogbox.StartDialog(0);
                        }
                    }
                    else if (gamedata.playercurrentactor == "Isis")
                    {
                        if (!gamedata.issaving || !gamedata.isloading)
                        {
                            //Load Actor selection scene
                            currentscene = 10;
                            dialogbox.StartDialog(0);
                        }
                    }
                    else
                    {
                        Debug.LogError("Player has no actor history line to follow");
                    }
                }
                else
                {
                    scenes[currentscene].GetComponent<SceneBehaviour>().timertogo--;
                    Debug.Log("Tempo para proceguir: " + scenes[currentscene].GetComponent<SceneBehaviour>().timertogo);
                }
                break;
                */
                break;
            #endregion
            //testando
            case 8:
                if (!scenes[currentscene].puzzles[0].resolved)
                {
                    if (!scenes[currentscene].puzzles[0].active)
                    {
                        scenes[currentscene].puzzles[0].GetComponent<PuzzlePortalFall>().transform.Rotate(0, 0, 180);
                    }
                    scenes[currentscene].puzzles[0].active = true;
                    scenes[currentscene].scenestate = Scene.state.puzzle;
                }
                if (scenes[currentscene].puzzles[0].resolved && scenes[currentscene].puzzles[0].active)
                {
                    //fazer o desbloqueio de cgs 0 de cada actor
                    string[] CGsToUnlock = new string[5] { benjamingalleryreference + 0.ToString(), enzogalleryreference + 0.ToString(), isisgalleryreference + 0.ToString(), malikagalleryreference + 0.ToString(), zakigalleryreference + 0.ToString() };
                    UnlockCGs(CGsToUnlock);

                    dialogbox.currentdialog = 0;
                    dialogbox.dialog.currentdialogline = 0;
                    if (dialogbox.currentdialog == 0)
                        if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                            scenes[currentscene].scenestate = Scene.state.dialog;
                    scenes[currentscene].puzzles[0].active = false;
                }
                if (scenes[currentscene].puzzles[0].resolved && !scenes[currentscene].puzzles[0].active)
                {
                    if (dialogbox.dialog.currentdialogline == 37)
                            OnDialogEndGoTo(9);
                }
                break;
            case 9:
                TimeToSetScene();
                OnDialogEndGoTo(10);
                break;
            case 10:
                TimeToSetScene();
                OnDialogEndGoTo(11);
                break;
            case 11:
                TimeToSetScene();
                if (gamedata.currentbenjaminaffinity > 7)
                {
                    OnDialogEndGoTo(12);
                }
                else
                {
                    scenes[currentscene].GetComponent<SceneBehaviour>().isfinalscene = true;
                }
                break;
            case 12:
                TimeToSetScene();
                break;
                //testando
            case 13:
                if (!scenes[currentscene].puzzles[0].resolved)
                {
                    if (!scenes[currentscene].puzzles[0].active)
                    {
                        scenes[currentscene].puzzles[0].GetComponent<PuzzlePortalFall>().transform.Rotate(0, 0, 180);
                    }
                    scenes[currentscene].puzzles[0].active = true;
                    scenes[currentscene].scenestate = Scene.state.puzzle;
                }
                if (scenes[currentscene].puzzles[0].resolved && scenes[currentscene].puzzles[0].active)
                {
                    //fazer o desbloqueio de cgs 0 de cada actor
                    string[] CGsToUnlock = new string[5] { benjamingalleryreference + 0.ToString(), enzogalleryreference + 0.ToString(), isisgalleryreference + 0.ToString(), malikagalleryreference + 0.ToString(), zakigalleryreference + 0.ToString() };
                    UnlockCGs(CGsToUnlock);

                    dialogbox.currentdialog = 0;
                    dialogbox.dialog.currentdialogline = 0;
                    if (dialogbox.currentdialog == 0)
                        if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                            scenes[currentscene].scenestate = Scene.state.dialog;
                    scenes[currentscene].puzzles[0].active = false;
                }
                if (scenes[currentscene].puzzles[0].resolved && !scenes[currentscene].puzzles[0].active)
                {
                    if(dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                        OnDialogEndGoTo(14);
                }
                break;
            case 14:
                TimeToSetScene();
                OnDialogEndGoTo(15);
                break;
            case 15:
                TimeToSetScene();
                if(gamedata.currentenzoaffinity > 7)
                {
                    OnDialogEndGoTo(16);
                }
                else
                {
                    scenes[currentscene].GetComponent<SceneBehaviour>().isfinalscene = true;
                }
                break;
            case 16:
                TimeToSetScene();
                if(dialogbox.dialog.currentdialogline == 30)
                {
                    scenes[currentscene].GetComponent<SceneBehaviour>().isfinalscene = true;
                }
                break;
            case 17:
                if (!scenes[currentscene].puzzles[0].resolved)
                {
                    if (!scenes[currentscene].puzzles[0].active)
                    {
                        scenes[currentscene].puzzles[0].GetComponent<PuzzlePortalFall>().transform.Rotate(0, 0, 180);
                    }
                    scenes[currentscene].puzzles[0].active = true;
                    scenes[currentscene].scenestate = Scene.state.puzzle;
                }
                if (scenes[currentscene].puzzles[0].resolved && scenes[currentscene].puzzles[0].active)
                {
                    //fazer o desbloqueio de cgs 0 de cada actor
                    string[] CGsToUnlock = new string[5] { benjamingalleryreference + 0.ToString(), enzogalleryreference + 0.ToString(), isisgalleryreference + 0.ToString(), malikagalleryreference + 0.ToString(), zakigalleryreference + 0.ToString() };
                    UnlockCGs(CGsToUnlock);

                    dialogbox.currentdialog = 0;
                    dialogbox.dialog.currentdialogline = 0;
                    if (dialogbox.currentdialog == 0)
                        if (dialogbox.dialog.currentdialogline != dialogbox.dialog.enddialogatline)
                            scenes[currentscene].scenestate = Scene.state.dialog;

                    scenes[currentscene].puzzles[0].active = false;
                }
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
        if (gamedata.playername.Length <= 0 && gamedata.playercurrentactor.Length <= 0)
        {
            player.inventary = new string[0];
        }
        if (currentscene == 1 && player.inventary.Length <= 0)
        {
            player.inventary = new string[6];
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
        if (dialogbox.gameObject.activeInHierarchy)
        {
            if (currentscene == 1)
            {
                if (dialogbox.currentdialog == 0)
                    playereyesfilter.moments = new int[1] { 0 };
                if (dialogbox.currentdialog == 1)
                    playereyesfilter.moments = new int[5] { 0, 1, 2, 3, 4 };
                if (dialogbox.currentdialog == 2)
                    playereyesfilter.moments = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }
            else { playereyesfilter.moments = new int[0]; }
        }
        else { playereyesfilter.moments = new int[0]; }

    }

    void UpdateCameraEyeEffect()
    {
        if (currentscene == 1)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 0)
                {
                    playereyesfilter.state = CameraEyeEffect.effectstate.Open;
                }
                if (dialogbox.dialog.currentdialogline == 3)
                {
                    playereyesfilter.state = CameraEyeEffect.effectstate.Close;
                }
            }
            else if (dialogbox.currentdialog == 1)
            {
                playereyesfilter.state = CameraEyeEffect.effectstate.Close;
            }
            else
            {
                if (playereyesfilter.moments.Length <= 0)
                    playereyesfilter.state = CameraEyeEffect.effectstate.Open;
            }
        }
    }

    #endregion

    #region Game Methods

    #region RewardPlayerMethods

    void UnlockCGs(string[] CGsThatWillUnlockArray)
    {
        bool NewCGSStatus = true;
        for (int i = 0; i < CGsThatWillUnlockArray.Length; i++)
        {
            gamecollection.SaveActorCGStatusWithCGName(CGsThatWillUnlockArray[i], NewCGSStatus);
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

        if (currentscenebehaviour != null)
        {
            CheckTimeEndSceneAndGoToNext(currentscenebehaviour, SceneToGo);
        }
        else { Debug.LogError("No SceneBehaviour on scene " + currentscene); }
    }

    #endregion

    #region Complete Game Methods

    void CompleteGameCheckAffinityUnlockEndAndPlayGameEnd()
    {
        canprogress = false;

        gamedata.currentgameend = CheckFinalAffinityWithCurrentActor();

        Debug.Log("Current game has ended");

        Application.LoadLevel(10);
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
    void CheckTimeEndSceneAndGoToNext(SceneBehaviour CurrentSceneBehaviour, int SceneToGo)
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
        if (gamedata.issaving)
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
            gamedata.playerlasttextfile = dialogbox.lastdialog;
            gamedata.playercurrenttextfile = dialogbox.currentdialog;
            gamedata.playercurrentdialogline = dialogbox.dialog.currentdialogline;
            gamedata.currentscenestate = scenes[currentscene].scenestate.ToString();
            gamedata.currentdialoganswerstate = dialogbox.hasanswered.ToString();

            gamedata.currentgameend = currentgameend;

            gamedata.gameprogress = currentscene + 1;
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
        dialogbox.lastdialog = gamedata.playerlasttextfile;
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

    #region MessageBox Methods
    //Method that update message path and message name acording to the current scene
    void MessageBoxManager()
    {
        switch(currentscene)
        {
            case 1:
                bool hasshowafterdialog = false;
                if (messagebox.messagepath != "LoadingInterfaces/" || messagebox.messagename != "BoxBroche")
                {
                    messagebox.SetMessagePath("LoadingInterfaces/");
                    messagebox.SetMessageName("BoxBroche");
                }
                else
                {
                    //test
                    if (!hasshowafterdialog && !dialogbox.gameObject.activeInHierarchy)
                    {
                        messagebox.UploadMessageBox();
                        if (messagebox.gameObject.activeInHierarchy)
                        {
                            messagebox.messagetime -= 1f;
                            canprogress = false;

                            if (messagebox.messagetime < 0)
                            {
                                messagebox.gameObject.SetActive(false);
                                hasshowafterdialog = true;
                            }
                        }
                        else
                        {
                            if (messagebox.messagetime < 0)
                                canprogress = true;
                            //set timer of message for a possibly next display
                            messagebox.SetMessageTime(300f);
                        }
                    }
                    else
                    {
                        //running
                        messagebox.UploadMessageBox();
                        if (messagebox.gameObject.activeInHierarchy)
                        {
                            messagebox.messagetime -= 1f;
                            canprogress = false;

                            if (messagebox.messagetime < 0)
                            {
                                messagebox.gameObject.SetActive(false);
                                hasshowafterdialog = true;
                            }
                        }
                        else
                        {
                            if (messagebox.messagetime < 0)
                                canprogress = true;
                            //set timer of message for a possibly next display
                            messagebox.SetMessageTime(300f);
                        }
                    }
                }
                break;
            case 2:
                if (messagebox.messagepath != "LoadingInterfaces/" || messagebox.messagename != "BoxDiario")
                {
                    messagebox.SetMessagePath("LoadingInterfaces/");
                    messagebox.SetMessageName("BoxDiario");
                }
                else
                {
                    messagebox.UploadMessageBox();
                    if (messagebox.gameObject.activeInHierarchy)
                    {
                        messagebox.messagetime -= 1f;
                        canprogress = false;

                        if (messagebox.messagetime < 0)
                            messagebox.gameObject.SetActive(false);
                    }
                    else
                    {
                        if(messagebox.messagetime < 0)
                            canprogress = true;
                        //set timer of message for a possibly next display
                        messagebox.SetMessageTime(300f);
                    }
                }
                break;
        }
    }

    #endregion

    #region LoadingInterface Methods

    void LoadingInterfaceManager()
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
                                //Declare the variable hasshowhowtoplay to check if the game has show to player the tutorial of the game
                                bool hasshowhowtoplay = false;
                                //If the game has not show the player the tutorial set and display it to the player has a loading and set the canprogress and active the loading time if the load end
                                if (!hasshowhowtoplay)
                                {
                                    //Active the tutorial image to player
                                    loadinginterface.tutorial.gameObject.SetActive(true);
                                    //If the loading timer count is less or equal to 0 declare the message of loading screen and set it to player
                                    if (loadinginterface.loadingtimescount <= 0)
                                    {
                                        //Declare the messages of the loading message
                                        loadinginterface.loadingmessegestext = new string[1] { "Como Jogar" };
                                        //Display the message on the text of loading screen
                                        loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                        //Change the image of the tutorial varible of the loading screen
                                        //loadinginterface.tutorial.sprite = Resources.Load<Sprite>("");
                                        //After set the message text and the tutorial image active the loading screen it self
                                        loadinginterface.gameObject.SetActive(true);
                                        //until the loading not end set the canprogress has false
                                        canprogress = false;
                                        //Update the loading message time and message if is time of loading else set the next loading time and update it current loading interface
                                        if (loadinginterface.loadingtime > 0)
                                        {
                                            loadinginterface.UploadLoadingMessage();
                                            loadinginterface.loadingtime -= 1f;
                                        }
                                        else
                                        {
                                            loadinginterface.loadingtimescount++;
                                            loadinginterface.loadingtime = 500f;
                                        }
                                    }
                                    else
                                    {
                                        loadinginterface.gameObject.SetActive(true);
                                        canprogress = false;
                                        hasshowhowtoplay = true;
                                    }
                                }
                                if (hasshowhowtoplay)
                                {
                                    
                                    if (loadinginterface.loadingtimescount <= 1)
                                    {
                                        loadinginterface.loadingmessegestext = new string[1] { "____________________________________________________prólogo" };
                                        loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                        loadinginterface.tutorial.sprite = Resources.Load<Sprite>("Filters/EyeFilter/NormalFilter");
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
                                            loadinginterface.loadingtime = 500f;
                                        }
                                    }
                                    else
                                    {
                                        loadinginterface.gameObject.SetActive(false);
                                        canprogress = true;
                                        //TEST loadinginterface.hasbeendisplayed = true;
                                    }
                                }
                                break;
                        }
                        break;
                }
                break;
            case 7:
                switch(dialogbox.dialog.currentdialogline)
                {
                    case 6:
                        if (gamedata.playercurrentactor == "Benjamin")
                        {
                            if (!loadinginterface.hasbeendisplayed)
                            {
                                //Active the tutorial image to player
                                loadinginterface.tutorial.gameObject.SetActive(true);
                                //If the loading timer count is less or equal to 0 declare the message of loading screen and set it to player
                                if (loadinginterface.loadingtimescount <= 0)
                                {
                                    //Declare the messages of the loading message
                                    loadinginterface.loadingmessegestext = new string[1] { "__________________________________________________Benjamin Capítulo 1" };
                                    //Display the message on the text of loading screen
                                    loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                    //Change the image of the tutorial varible of the loading screen
                                    loadinginterface.tutorial.sprite = Resources.Load<Sprite>("LoadingInterfaces/Puzzlebox");
                                    //After set the message text and the tutorial image active the loading screen it self
                                    loadinginterface.gameObject.SetActive(true);
                                    //until the loading not end set the canprogress has false
                                    canprogress = false;
                                    //Update the loading message time and message if is time of loading else set the next loading time and update it current loading interface
                                    if (loadinginterface.loadingtime > 0)
                                    {
                                        loadinginterface.UploadLoadingMessage();
                                        loadinginterface.loadingtime -= 1f;
                                    }
                                    else
                                    {
                                        loadinginterface.loadingtimescount++;
                                        loadinginterface.loadingtime = 500f;
                                    }
                                }
                                else
                                {
                                    loadinginterface.gameObject.SetActive(false);
                                    canprogress = true;
                                    loadinginterface.hasbeendisplayed = true;
                                    //End text 0 to the game auto run
                                    dialogbox.Processed();
                                }
                            }
                        }
                        if (gamedata.playercurrentactor == "Enzo")
                        {
                            if (!loadinginterface.hasbeendisplayed)
                            {
                                //Active the tutorial image to player
                                loadinginterface.tutorial.gameObject.SetActive(true);
                                //If the loading timer count is less or equal to 0 declare the message of loading screen and set it to player
                                if (loadinginterface.loadingtimescount <= 0)
                                {
                                    //Declare the messages of the loading message
                                    loadinginterface.loadingmessegestext = new string[1] { "__________________________________________________Enzo Capítulo 1" };
                                    //Display the message on the text of loading screen
                                    loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                    //Change the image of the tutorial varible of the loading screen
                                    loadinginterface.tutorial.sprite = Resources.Load<Sprite>("LoadingInterfaces/Puzzlebox");
                                    //After set the message text and the tutorial image active the loading screen it self
                                    loadinginterface.gameObject.SetActive(true);
                                    //until the loading not end set the canprogress has false
                                    canprogress = false;
                                    //Update the loading message time and message if is time of loading else set the next loading time and update it current loading interface
                                    if (loadinginterface.loadingtime > 0)
                                    {
                                        loadinginterface.UploadLoadingMessage();
                                        loadinginterface.loadingtime -= 1f;
                                    }
                                    else
                                    {
                                        loadinginterface.loadingtimescount++;
                                        loadinginterface.loadingtime = 500f;
                                    }
                                }
                                else
                                {
                                    loadinginterface.gameObject.SetActive(false);
                                    canprogress = true;
                                    loadinginterface.hasbeendisplayed = true;
                                    //End text 0 to the game auto run
                                    dialogbox.Processed();
                                }
                            }
                        }
                        if (gamedata.playercurrentactor == "Isis")
                        {
                            if (!loadinginterface.hasbeendisplayed)
                            {
                                //Active the tutorial image to player
                                loadinginterface.tutorial.gameObject.SetActive(true);
                                //If the loading timer count is less or equal to 0 declare the message of loading screen and set it to player
                                if (loadinginterface.loadingtimescount <= 0)
                                {
                                    //Declare the messages of the loading message
                                    loadinginterface.loadingmessegestext = new string[1] { "__________________________________________________Isis Capítulo 1" };
                                    //Display the message on the text of loading screen
                                    loadinginterface.loadingtext.text = loadinginterface.loadingmessegestext[loadinginterface.counterofloops];
                                    //Change the image of the tutorial varible of the loading screen
                                    loadinginterface.tutorial.sprite = Resources.Load<Sprite>("LoadingInterfaces/Puzzlebox");
                                    //After set the message text and the tutorial image active the loading screen it self
                                    loadinginterface.gameObject.SetActive(true);
                                    //until the loading not end set the canprogress has false
                                    canprogress = false;
                                    //Update the loading message time and message if is time of loading else set the next loading time and update it current loading interface
                                    if (loadinginterface.loadingtime > 0)
                                    {
                                        loadinginterface.UploadLoadingMessage();
                                        loadinginterface.loadingtime -= 1f;
                                    }
                                    else
                                    {
                                        loadinginterface.loadingtimescount++;
                                        loadinginterface.loadingtime = 500f;
                                    }
                                }
                                else
                                {
                                    loadinginterface.gameObject.SetActive(false);
                                    canprogress = true;
                                    loadinginterface.hasbeendisplayed = true;
                                    //End text 0 to the game auto run
                                    dialogbox.Processed();
                                }
                            }
                        }
                        break;
                }
                break;
            default:
                loadinginterface.gameObject.SetActive(false);
                loadinginterface.hasbeendisplayed = false;
                loadinginterface.loadingtime = 500f;
                break;
        }
    }

    #endregion

    #region Effect Camera Methods

    void AdjustEffectCameraToGameInterface()
    {
        Rect newrect;
        if (currentscene == 8 || currentscene == 13 || currentscene == 17)
        {
            if (pausemenu.gameObject.activeInHierarchy)
            {
                newrect = new Rect(0, -0.06f, 1, 1);
                effectscamerablurfilter.GetComponent<Camera>().rect = newrect;
            }
            else
            {
                newrect = new Rect(0, 0, 1, 1);
                effectscamerablurfilter.GetComponent<Camera>().rect = newrect;
            }
        }
    }

    #endregion

    #endregion

    #endregion

}
