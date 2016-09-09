using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

    public TextAsset dialogtextfile;
    public TextAsset answertextfile;

    public string[] dialoglines;
    public int currentdialogline;
    public int enddialogatline;

    public bool issplited;

    public string[] answers;

    public bool isanswerssplit;

    public void SplitDialogText()
    {
        dialoglines = dialogtextfile.text.Split('\n');
        enddialogatline = dialoglines.Length - 1;
        issplited = true;
    }
    public void SplitAnswerText()
    {
        answers = answertextfile.text.Split('\n');
        isanswerssplit = true;
    }
    public void ChangeDialogText()
    {
        issplited = false;
        isanswerssplit = false;
        Debug.Log("New Dialog: " + dialogtextfile);
    }
}
