using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    #region Attributes
    public GameData gamedata;
    public GameSettings gamesettings;
    public Player player;
    public Background background;
    public MusicPlayer musicplayer;
    public DialogBox dialogbox;
    

    public Actor[] actors;
    public Scene[] scenes;

    public int currentscene;
    public bool canprogress;
    #endregion
    #region Methods
    void OnEnable()
    {
        Debug.Log("Game Controller active");
    }
    void OnDisable()
    {
        Debug.Log("Game Controller disactive");
    }
    void Awake()
    {

    }
	void Start () {

        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();

        player.playercurrentactor = gamedata.playercurrentactor;

        musicplayer.PlayMusic();

        dialogbox.scene = scenes[currentscene];
    }
	void Update ()
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
            musicplayer.LimitMusicLengh(scenes[currentscene].musics.Length-1);

        if(scenes[currentscene].musics.Length > 0)
            musicplayer.music.clip = scenes[currentscene].musics[musicplayer.currentmusicclip];

        if (!musicplayer.music.isPlaying)
            musicplayer.NextMusic();
        #endregion
        #region Dialog Control
        dialogbox.DialogUpdate();

        dialogbox.scene = scenes[currentscene];
        #endregion
        #region Actors Control

        PrepareActorDialogLines();

        for(int i =0; i < actors.Length; i ++)
        {
            actors[i].hasdialog = CheckActorDialogLines(actors[i]);
            if (actors[i].hasdialog)
                actors[i].gameObject.SetActive(true);
            else { actors[i].gameObject.SetActive(false); }
        }

        #endregion
    }
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
            if(actors[i].name == "Enzo")
            {
                if (currentscene == 0)
                {
                    if (dialogbox.currentdialog == 0)
                        actors[i].dialoglines = new int[1] { 0 };
                    else { actors[i].dialoglines = new int[0]; }
                }
            }
        }
    }
    #endregion
    #region LoadData Methods

    #endregion
    #endregion
}
