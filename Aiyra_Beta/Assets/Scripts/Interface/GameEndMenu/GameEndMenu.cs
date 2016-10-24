using UnityEngine;
using System.Collections;

public class GameEndMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;

    #endregion

    #region Methods

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
    }
    void Start () {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();
	}

    #endregion

    #endregion
}
