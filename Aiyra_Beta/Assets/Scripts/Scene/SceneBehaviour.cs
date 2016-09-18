using UnityEngine;
using System.Collections;

public class SceneBehaviour : MonoBehaviour {

    #region Attributes

    public enum behaviour { OnDialogEndGoTo, OnTimeEndGoTo, OnDialogEndSaveGameProgress, OnDialogEndSaveGameAndLoadGameScene };
    public behaviour scenebehaviour;

    public float timertogo;

    public int scene;
    public int gamescene;

    public GameController gamecontroller;

    #endregion

    #region Methods

    #region Awake And Start Methods

    void Awake ()
    {
        if(gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    #endregion

    #region Update Methods

    public void UpdateSceneBehaviour()
    {
        
        #region Cases
         
        if (scenebehaviour == behaviour.OnDialogEndGoTo)
        {
            if (gamecontroller.currentscene == 0)
                OnDialogEndGoTo(scene);

            if (gamecontroller.currentscene == 3)
            {
                if (gamecontroller.dialogbox.lastanswerid >= 0)
                {
                    if (gamecontroller.dialogbox.lastanswerid == 2)
                        OnDialogEndGoTo(6);
                    else { OnDialogEndGoTo(4); }
                }
            }
                //else { OnDialogEndGoTo(4); }

            if (gamecontroller.currentscene == 4)
            {
                OnDialogEndGoTo(5);
            }

            if (gamecontroller.currentscene == 5)
                OnDialogEndGoTo(6);
        }

        if(scenebehaviour == behaviour.OnTimeEndGoTo)
        {
            if (gamecontroller.currentscene == 6)
                OnTimeEndGoTo();
        }

        if(scenebehaviour == behaviour.OnDialogEndSaveGameProgress)
        {

        }

        if(scenebehaviour == behaviour.OnDialogEndSaveGameAndLoadGameScene)
        {
        }

        #endregion

    }

    #endregion

    #region SceneBahaviour Fundamental Methods

    public void OnDialogEndGoTo(int Scene)
    {
        if (gamecontroller != null)
            if (!gamecontroller.dialogbox.gameObject.activeInHierarchy)
            {
                Debug.Log("You has go to scene" + Scene);
                gamecontroller.currentscene = Scene;
                gamecontroller.dialogbox.StartDialog(0);
            }
    }
    public void OnTimeEndGoTo()
    {
        if (gamecontroller != null)
            if (timertogo > 0)
            {
                timertogo --;
                Debug.Log( "Tempo para entrar no portal: " + timertogo);
            }
            else
            {
                gamecontroller.currentscene = scene;
                gamecontroller.dialogbox.StartDialog(0);
                Debug.Log("You has go to Scene " + scene);
            }

    }
    public void OnDialogEndSaveGameProgress()
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
    public void OnDialogEndSaveGameAndLoadGameScene(int DialogReference,int DialogLineReference)
    {
        if (gamecontroller != null)
            if (gamecontroller.dialogbox.currentdialog == DialogReference && gamecontroller.dialogbox.dialog.currentdialogline == DialogLineReference)
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
