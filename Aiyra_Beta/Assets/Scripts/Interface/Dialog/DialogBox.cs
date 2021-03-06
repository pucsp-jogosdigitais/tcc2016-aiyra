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
    public GameObject speakerbox;
    public Text speakernametext;
    public GameObject answerbox;
    public AnswerButton[] answersbuttons;

    public int lastdialog;
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

    public float processtimer;

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
    //Method is run only one and when the gameobject associed with the script awake
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

    #region Speaker Methods

    public void SetSpeakerName(string NewSpeaker)
    {
        speakernametext.text = NewSpeaker;
    }

    #endregion

    #region Dialog Start Methods

    public void StartDialog(int DialogToStart)
    {
        currentdialog = DialogToStart;
        dialog.currentdialogline = 0;
        gameObject.SetActive(true);
        answerbox.SetActive(false);
        dialog.ChangeDialogText();
        lastanswerid = -1;
    }

    #endregion

    #region Dialog Restart Methods

    void RestartDialog()
    {
        dialog.currentdialogline = 0;
        lastanswerid = -1;
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
                    if (!dialog.isanswermoment)
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
    //Method that make all the final and essencial action when the answerbutton is unlock like process with the dialog and etc.
    public void OnAnswerContinue()
    {
        if (currentdialoganswers < gamecontroller.scenes[gamecontroller.currentscene].answers.Length - 1)
            currentdialoganswers++;
        hasanswered = true;
        dialog.isanswermoment = false;
        Processed();
        answerbox.SetActive(false);
    }
    //Method that give to player current affinity with actor the value of the cliked answer
    public void OnAnswerGainAffinity(AnswerButton AnswerButton)
    {
        if (gamecontroller.player.currentactoraffinity < 100)
        {
            gamecontroller.player.currentactoraffinity += AnswerButton.currentvalue;
            Debug.Log("Você ganhou " + AnswerButton.currentvalue + " pontos de affinidade com " + gamecontroller.player.playercurrentactor);
        }
        SetAnswerButtonsValue(0, 0, 0);
    }
    //Method that decide what will be the next dialog based on the clicked answer
    public void OnAnswerChangeNextDialog(AnswerButton AnswerButton)
    {
        lastanswerid = AnswerButton.answerbuttonid;
        Debug.Log("Respondeu " + lastanswerid);
        nextdialog = AnswerButton.nextdialog;
        Debug.Log("NextDialog" + nextdialog);
    }
    //Method that check the clicked answer and unlock cg based on the choice
    public void OnAnswerUnlockCG(AnswerButton AnswerButton)
    {
        if (AnswerButton.currentcgtounlock != "")
        {
            gamecontroller.gamecollection.SaveActorCGStatusWithCGName(AnswerButton.currentcgtounlock, AnswerButton.currentcgtounlockstatus);
        }
        else
        {
            Debug.Log("This last answer has no cg to unlock");
        }
    }

    #endregion

    //Methods that set the values of the variables of the answer buttons
    #region Methods for answersButtons
    public void SetAnswerButtonsValue(int NewAnswerButton0Value,int NewAnswerButton1Value,int NewAnswerButton2Value)
    {
        answersbuttons[0].ButtonSetValue(NewAnswerButton0Value);
        answersbuttons[1].ButtonSetValue(NewAnswerButton1Value);
        answersbuttons[2].ButtonSetValue(NewAnswerButton2Value);
    }
    public void AnswerButtonsSetNextDialog(int AnswerButton0NextDialog, int AnswerButton1NextDialog, int AnswerButton2NextDialog)
    {
        answersbuttons[0].ButtonSetNextDialog(AnswerButton0NextDialog);
        answersbuttons[1].ButtonSetNextDialog(AnswerButton1NextDialog);
        answersbuttons[2].ButtonSetNextDialog(AnswerButton2NextDialog);
    }
    public void AnswerButtonsSetNextCG(string AnswerButton0NextCG, string AnswerButton1NextCG, string AnswerButton2NextCG)
    {
        answersbuttons[0].ButtonSetNextCG(AnswerButton0NextCG);
        answersbuttons[1].ButtonSetNextCG(AnswerButton1NextCG);
        answersbuttons[2].ButtonSetNextCG(AnswerButton2NextCG);
    }
    public void AnswerButtonsSetNextCGStatus(bool AnswerButton0NextCGStatus, bool AnswerButton1NextCGStatus, bool AnswerButton2NextCGStatus)
    {
        answersbuttons[0].ButtonSetNextCGStatus(AnswerButton0NextCGStatus);
        answersbuttons[1].ButtonSetNextCGStatus(AnswerButton1NextCGStatus);
        answersbuttons[2].ButtonSetNextCGStatus(AnswerButton2NextCGStatus);
    }

    #endregion

    #endregion

    #endregion
  
    #region Methods for processed button
    //processed dialog with timer less than 0 else take one float of the timer
    public void ProcessedWithTimer()
    {
        if (gamecontroller.canprogress)
        {
            if (processtimer <= 0)
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
                            if (!dialog.isanswermoment)
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
                //reset timer according to the time take
                processtimer = 0.1f;
            }
            else { processtimer -= 1.0f; }
        }
    }

    #endregion

    #endregion

}
