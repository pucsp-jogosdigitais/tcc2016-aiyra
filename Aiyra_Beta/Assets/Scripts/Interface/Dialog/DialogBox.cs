using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {

    #region attributes
    public GameData gamedata;
    public GameController gamecontroller;
    public Scene scene;
    public Dialog dialog;
    public GameObject dialogbox;
    public Text text;
    public GameObject answerbox;
    public AnswerButton[] answersbuttons;

    public int currentdialog;
    public int nextdialog;
    public int endatdialog;

    public int currentdialoganswers;
    public int nextdialoganswers;

    public int lastanswerid;

    public bool hasanswered;
    public bool hasnextdialog;
    public bool hasnextanswers;
    public bool onclickenddialog;
    #endregion

    #region methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("DialogDisplayBox active");
    }
    void OnDisable()
    {
        Debug.Log("DialogDisplayBox disactive");
    }

    #endregion

    #region Awake And Start

    void Awake()
    {
        if(gamedata == null)
            gamedata = GameObject.Find("GameData").GetComponent<GameData>();
        if (gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        if (dialogbox == null)
            dialogbox = gameObject;
        if (text == null)
            text = gameObject.GetComponentInChildren<Text>();
    }
    void Start()
    {
        lastanswerid = -1;
    }

    #endregion

    #region Dialog Update Methods

    public void DialogUpdate()
    {

        if (hasnextdialog)
            onclickenddialog = false;
        else { onclickenddialog = true; }

        if (scene.scenestate == Scene.state.dialog)
        {
            if (scene.dialogs.Length >= 2)
            {
                if (nextdialog != currentdialog)
                    hasnextdialog = true;
                else { hasnextdialog = false; }

                dialogbox.SetActive(true);
                dialog.dialogtextfile = scene.dialogs[currentdialog];
                endatdialog = scene.dialogs.Length - 1;
                if (!dialog.istextsplited)
                    dialog.SplitDialogText();
                text.text = dialog.dialoglines[dialog.currentdialogline];
            }

            if (scene.answers.Length >= 2)
            {
                dialog.answertextfile = scene.answers[currentdialoganswers];
                if (!dialog.isanswerssplit)
                    dialog.SplitAnswerText();
                if (answerbox.activeInHierarchy)
                    for (int i = 0; i < dialog.answers.Length - 1; i++)
                    {
                        answersbuttons[i].gameObject.SetActive(true);
                        answersbuttons[i].gameObject.GetComponentInChildren<Text>().text = dialog.answers[i];
                    }
                else
                {
                    foreach (AnswerButton button in answersbuttons)
                        button.gameObject.SetActive(false);
                }
            }
        }
        else { dialogbox.SetActive(false); }
    }

    #endregion

    #region Dialog Fundamental Methods

    #region methods for adjust dialogbox and dialog

    #region Dialog Start Methods

    public void StartDialog(int DialogToStart)
    {
        currentdialog = DialogToStart;
        gameObject.SetActive(true);
        dialog.ChangeDialogText();
        answerbox.SetActive(false);
        lastanswerid = -1;
    }

    #endregion

    #region Dialog Restart Methods

    void RestartDialog()
    {
        dialog.currentdialogline = 0;
        if (currentdialog != nextdialog)
            hasnextdialog = true;
    }

    void RestartDialogAnswers()
    {
        currentdialoganswers = 0;
    }

    #endregion

    #region Dialog End Methods

    void EndDialog()
    {
        scene.scenestate = Scene.state.interaction;
    }

    #endregion

    #region Dialog Answers Methods

    void DisplayAnswers()
    {
        answerbox.SetActive(true);
    }

    #endregion

    #region Player Interaction Process

    public void Processed()
    {
        if (dialogbox.gameObject.activeInHierarchy && !gamecontroller.pausemenu.gameObject.activeInHierarchy)
        {
            if (onclickenddialog)
                scene.scenestate = Scene.state.interaction;
            else
            {
                if (dialog.isanswermoment)
                {
                    DisplayAnswers();
                }
                if (hasnextdialog)
                {

                    onclickenddialog = false;

                    if (!dialog.isanswermoment)
                    {

                        if (dialog.currentdialogline == dialog.enddialogatline)
                        {
                            currentdialog = nextdialog;
                            currentdialoganswers = nextdialoganswers;
                            RestartDialog();
                            dialog.ChangeDialogText();
                        }
                        else
                        {
                            dialog.currentdialogline++;
                        }
                    }
                }
                if (!hasnextdialog)
                {
                    if(!dialog.isanswermoment)
                        if (dialog.currentdialogline == dialog.enddialogatline)
                            onclickenddialog = true;
                }
            }
        }
        else
        {
            if (!gamecontroller.pausemenu.gameObject.activeInHierarchy)
            {
                Debug.Log("DialogBox not active can�t procced with dialog");
                EndDialog();
            }
        }
    }

    #endregion

    #endregion

    #region methods for answers

    #region Main Methods

    public void OnAnswerContinue()
    {
        answerbox.SetActive(false);
        Debug.Log("Respondeu " + lastanswerid);
        if (currentdialoganswers < gamecontroller.scenes[gamecontroller.currentscene].answers.Length - 1)
            currentdialoganswers++;
        hasanswered = true;
        dialog.isanswermoment = false;
    }
    public void OnAnswerGoToScene(int Scene)
    {
        answerbox.SetActive(false);
        Debug.Log("Respondeu vai para a scene " + Scene);
        hasanswered = true;
    }
    public void OnAnswerGainAffinity(AnswerButton AnswerButton)
    {
        if (gamecontroller.player.currentactoraffinity < 100)
        {
            gamecontroller.player.currentactoraffinity += AnswerButton.currentvalue;
            Debug.Log("Você ganhou " + AnswerButton.currentvalue + " pontos de affinidade com " + gamecontroller.player.playercurrentactor);
        }
        SetAnswerButtonsValue(0, 0, 0);
        /*
        if (gamedata.playercurrentactor == "Enzo")
        {
            if (gamedata.currentenzoaffinity < 100)
            {
                Debug.Log("You Gain " + AnswerButton.currentvalue + " With Enzo");
                gamecontroller.player.currentactoraffinity += 
                gamedata.currentenzoaffinity += AnswerButton.currentvalue;
                gamedata.SaveAllAffinitys();
            }
            else { Debug.LogWarning("Player already reach the limit of affinity with this character"); }
        }
        */
    }
    public void OnAnswerChangeNextDialog(AnswerButton AnswerButton)
    {
        lastanswerid = AnswerButton.answerbuttonid;
        currentdialog = nextdialog;
        RestartDialog();
        dialog.ChangeDialogText();
    }

    #endregion

    #region Methods for answersButtons
    public void SetAnswerButtonsValue(int NewAnswerButton0Value,int NewAnswerButton1Value,int NewAnswerButton2Value)
    {
        answersbuttons[0].currentvalue = NewAnswerButton0Value;
        answersbuttons[1].currentvalue = NewAnswerButton1Value;
        answersbuttons[2].currentvalue = NewAnswerButton2Value;
    }
    public void AnswerSetNextDialog(int CaseAnswer1, int CaseAnswer2, int CaseAnswer3)
    {
        if (lastanswerid == 0)
            nextdialog = CaseAnswer1;
        if (lastanswerid == 1)
            nextdialog = CaseAnswer2;
        if (lastanswerid == 2)
            nextdialog = CaseAnswer3;
    }
    #endregion

    #endregion

    #endregion

    #endregion

}
