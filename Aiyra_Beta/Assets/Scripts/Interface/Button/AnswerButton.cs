using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerButton : MonoBehaviour {

    public Button answerbuttonbutton;

    public int answerbuttonid;
    public int currentvalue;

    public void ButtonSetValue(int Value)
    {
        currentvalue = Value;
    }
}
