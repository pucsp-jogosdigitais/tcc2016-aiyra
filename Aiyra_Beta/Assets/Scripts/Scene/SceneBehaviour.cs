using UnityEngine;
using System.Collections;

public class SceneBehaviour : MonoBehaviour {

    #region Attributes

    public enum behaviour { OnDialogEndGoTo, OnTimeEndGoTo, OnDialogEndSaveGameProgress, OnDialogEndSaveGameAndLoadGameScene };
    public behaviour scenebehaviour;

    public int scene;
    public int gamescene;

    public GameController gamecontroller;

    #endregion

    #region Methods

    void Awake ()
    {
        if(gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        
        #region Cases
         
        if (scenebehaviour == behaviour.OnDialogEndGoTo)
        {
            if (gamecontroller.currentscene == 0)
                OnDialogEndGoTo(scene);
        }
        /*
        if(scenebehaviour == behaviour.OnTimeEndGoTo)
        {

        }
        if(scenebehaviour == behaviour.OnDialogEndSaveGameProgress)
        {

        }
        */
        if(scenebehaviour == behaviour.OnDialogEndSaveGameAndLoadGameScene)
        {
            if (gamecontroller.currentscene == 1)
                if(gamecontroller.gamedata.playercurrentactor == "")
                    OnDialogEndSaveGameAndLoadGameScene(1, 1, 0);
        }

        #endregion

    }

    #region SceneBahaviour Fundamental Methods

    void OnDialogEndGoTo(int Scene)
    {
        if (gamecontroller != null)
            if (!gamecontroller.dialogbox.gameObject.activeInHierarchy)
            {
                gamecontroller.currentscene = Scene;
                gamecontroller.dialogbox.StartDialog(0);
            }
    }
    void OnTimeEndGoTo(float Time)
    {
        if (gamecontroller != null)
            if (Time > 0)
                Time--;
            else
            {
                gamecontroller.currentscene = scene;
                gamecontroller.dialogbox.StartDialog(0);
            }

    }
    void OnDialogEndSaveGameProgress()
    {
        if (gamecontroller != null)
            if (!gamecontroller.dialogbox.gameObject.activeInHierarchy)
            {
                /*
                gamecontroller.gamedata.gameprogress++;
                gamecontroller.gamedata.SaveAllGameData();
                */
            }
    }
    void OnDialogEndSaveGameAndLoadGameScene(int SceneReference,int DialogReference,int DialogLineReference)
    {
        if (gamecontroller != null)
            if (gamecontroller.currentscene == SceneReference && gamecontroller.dialogbox.currentdialog == DialogReference && gamecontroller.dialogbox.dialog.currentdialogline == DialogLineReference)
            {
                gamecontroller.gamedata.SaveAllPlayerData();
                gamecontroller.gamedata.SaveAllGameData();
                if (!gamecontroller.gamedata.issaving || !gamecontroller.gamedata.isloading)
                    Application.LoadLevel(gamescene);
            }
    }

    #endregion

    #endregion
}
