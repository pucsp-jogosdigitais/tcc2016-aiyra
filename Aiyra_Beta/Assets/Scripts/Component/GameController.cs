using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Actors Name
    private const string actorEnzoEndress = "Enzo";
    private const string actorIsisEndress = "Isis";
    private const string actorBenjaminEndress = "Benjamin";
    private const string actorMalikaEndress = "Malika";
    private const string actorZakiEndress = "Zaki";
    #endregion

    #region Attributes
    public GameData gamedata;
    public GameSettings gamesettings;
    public Player player;
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
        if (background == null)
            background = GameObject.Find("Background").GetComponent<Background>();
        if (musicplayer == null)
            musicplayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        if (dialogbox == null)
            dialogbox = GameObject.Find("DialogDisplayBox").GetComponent<DialogBox>();
        if (pausemenu == null)
            pausemenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();

        if (actors == null)
            Debug.LogWarning("Game Controller has no actors");
        if (scenes == null || scenes.Length <= 0)
            Debug.LogError("No scene on the game controller");
    }
	void Start () {

        LoadAllGameAndPlayerDataToCurrentGame();
        
        musicplayer.PlayMusic();

        dialogbox.scene = scenes[currentscene];
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
        
        if(Input.GetButtonDown("Confirm"))
            dialogbox.Processed();

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
        #region Game Control
        SaveAllGameAndPlayerDataOfCurrentGame();
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
                if (dialogbox.dialog.currentdialogline == 1)
                    if(!dialogbox.hasanswered) 
                        dialogbox.dialog.isanswermoment = true;
                if (dialogbox.dialog.currentdialogline == 2)
                    dialogbox.hasanswered = false;
            }
        }
        if (currentscene == 1)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == dialogbox.dialog.enddialogatline)
                    if (!dialogbox.hasanswered) 
                        dialogbox.dialog.isanswermoment = true;
            }
        }
    }
    void UploadAnswersValue()
    {
        if (currentscene == 0)
        {
            if (dialogbox.currentdialog == 0)
            {
                if (dialogbox.dialog.currentdialogline == 1)
                {
                    dialogbox.SetAnswerButtonsValue(5, 10, 3);
                }
                else { dialogbox.SetAnswerButtonsValue(0, 0, 0); }
            }
        }
    }

    #endregion

    #region Next Dialog Adjust Methods

    void AdjustDialogDisplayBoxToNextDialog()
    {
        if(currentscene == 0)
        {
            if(dialogbox.currentdialog == 0)
            {
                dialogbox.SetNextDialog(1, 1, 1);
            }
        }
        if (currentscene == 1)
        {
            if (dialogbox.currentdialog == 0)
            {
                dialogbox.SetNextDialog(1, 2, 3);
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
            dialogbox.SetNextDialog(NextDialogCaseAnswer0, NextDialogCaseAnswer1, NextDialogCaseAnswer2);
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

    #region Game Methods

    #region Time Adjust Methods

    #endregion

    #region Check Methods
    bool CheckDataChanges()
    {
        if (player.playercurrentactor == "Enzo")
        {
            if (gamedata.playername != player.playername
                || gamedata.playercurrentactor != player.playercurrentactor
                
                ||gamedata.currentenzoaffinity != player.currentactoraffinity
                
                ||gamedata.playercurrentscene != currentscene
                ||gamedata.playercurrenttextfile != dialogbox.currentdialog
                ||gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline)
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
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline)
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
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline)
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
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline)
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
                || gamedata.playercurrentdialogline != dialogbox.dialog.currentdialogline)
                return true;
            else { return false; }
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

            gamedata.gameprogress = currentscene+1;
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

    }
    #endregion

    #endregion

    #endregion

    #endregion
}
