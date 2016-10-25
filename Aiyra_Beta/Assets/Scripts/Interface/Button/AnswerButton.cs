using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerButton : MonoBehaviour {

    #region Attributes

    public Button answerbuttonbutton;

    public int answerbuttonid;
    public int nextdialog;
    public int currentvalue;

    public string currentcgtounlock;
    public bool currentcgtounlockstatus;

    #endregion

    #region Methods
    //Methods that set the values of the attributes of the answer button
    #region Set Values Methods
    //Method that set the next dialog following the specfic answer button
    public void ButtonSetNextDialog(int NextDialog)
    {
        nextdialog = NextDialog;
    }
    //Method that set value as affinity of the specfic answer button
    public void ButtonSetValue(int Value)
    {
        currentvalue = Value;
    }
    //Method that set the next cg that will be unlock on click of the specific answer button
    public void ButtonSetNextCG(string NextCG)
    {
        currentcgtounlock = NextCG;
    }
    //Method that will set the next cg unlock or locked
    public void ButtonSetNextCGStatus(bool NextCGNewStatus)
    {
        currentcgtounlockstatus = NextCGNewStatus;
    }

    #endregion

    #endregion
}
