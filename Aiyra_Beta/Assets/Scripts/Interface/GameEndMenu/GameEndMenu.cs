using UnityEngine;
using System.Collections;

public class GameEndMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public VideoPlayer videoplayer;
    public GameEndsLibrary endlibrary;

    #endregion

    #region Methods
    //Methods that alert developer of the activaction and desactivetion of the gameobject and scripts
    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("EndMenu Active");
    }
    void OnDisable()
    {
        Debug.Log("EndMenu Disactive");
    }

    #endregion

    #region Awake And Start Methods

    void Awake()
    {
        if (gamedata == null)
            gamedata = GameObject.Find("GameData").GetComponent<GameData>();
        if (videoplayer == null)
            videoplayer = GetComponentInChildren<VideoPlayer>();
        if (endlibrary == null)
            endlibrary = GetComponentInChildren<GameEndsLibrary>();
    }
    void Start ()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();

        CheckAndUploadGameEndVideo();
	}

    #endregion

    #region GameEndMenu Fundamental Methods
    void CheckAndUploadGameEndVideo()
    {
        videoplayer.videotype = VideoPlayer.videoType.gameend;

        if(gamedata.playercurrentactor == "Benjamin")
        {
            if (gamedata.currentgameend == 2)
                videoplayer.movie = endlibrary.Ends[0];
            else if (gamedata.currentgameend == 1)
                videoplayer.movie = endlibrary.Ends[1];
            else
            {
                videoplayer.movie = endlibrary.Ends[2];
            }
        }
    }

    #endregion

    #endregion
}
