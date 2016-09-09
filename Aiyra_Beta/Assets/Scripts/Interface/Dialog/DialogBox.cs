using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {

    #region attributes
    public Scene scene;
    public GameObject dialogbox;
    public Text text;
    public Dialog dialog;
    public GameObject answerbox;
    public Button[] answersbuttons;
    public GameObject PauseMenu;

    public int currentdialog;
    public int endatdialog;

    public bool hasanswered;
    #endregion

    #region methods
    public void DialogUpdate()
    {
        if (scene.scenestate == Scene.state.dialog)
        {
            dialogbox.SetActive(true);
            dialog.dialogtextfile = scene.dialogs[currentdialog];
            endatdialog = scene.dialogs.Length - 1;
            if (!dialog.issplited)
                dialog.SplitDialogText();
            text.text = dialog.dialoglines[dialog.currentdialogline];

            dialog.answertextfile = scene.answers[currentdialog];
            if (!dialog.isanswerssplit)
                dialog.SplitAnswerText();
            if(answerbox.activeInHierarchy)
                for (int i = 0; i < dialog.answers.Length-1; i++)
                {
                    answersbuttons[i].gameObject.SetActive(true);
                    answersbuttons[i].gameObject.GetComponentInChildren<Text>().text = dialog.answers[i];
                }
            else
            {
                foreach (Button button in answersbuttons)
                    button.gameObject.SetActive(false);
            }
        }
        else { dialogbox.SetActive(false); }
    }
    #region methods for adjust dialogbox and dialog
    public void StartDialog()
    {
        currentdialog = 0;
        gameObject.SetActive(true);
        dialog.ChangeDialogText();
        answerbox.SetActive(false);
    }
    public void Processed()
    {
        if (gameObject.activeInHierarchy)
        {
            if (currentdialog < endatdialog)
                if (dialog.currentdialogline < dialog.enddialogatline)
                    dialog.currentdialogline++;
                else
                {
                    if (hasanswered)
                    {
                        hasanswered = false;
                        currentdialog++;
                        RestartDialog();
                        dialog.ChangeDialogText();
                    }
                    EndDialog();
                }
            else
            {
                scene.scenestate = Scene.state.interaction; 
            }
        }
    }
    void RestartDialog()
    {
        dialog.currentdialogline = 0;
    }
    void EndDialog()
    {
        DisplayAnswers();
    }
    void DisplayAnswers()
    {
        answerbox.SetActive(true);   
    }
    #endregion
    #region methods for answers
    public void OnAnswerContinue()
    {
        answerbox.SetActive(false);
        Debug.Log("Respondeu  !!! continua");
        hasanswered = true; 
    }
    public void OnAnswerGoToScene(int Scene)
    {
        answerbox.SetActive(false);
        Debug.Log("Respondeu !!! vai para a scene" + Scene);
        hasanswered = true;
    }
    #endregion
    #region Methods for PauseMenu
    public void OnConfigClick()
    {
        if (!PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(true);
        }
    }
    #endregion
    #endregion
}
