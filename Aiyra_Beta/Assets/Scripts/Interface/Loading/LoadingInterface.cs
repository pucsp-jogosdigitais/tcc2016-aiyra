using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Text))]
public class LoadingInterface : MonoBehaviour {

    #region Attributes

    public Image background;
    public Image tutorial;
    public Text loadingtext;

    public string[] loadingmessegestext;
    public int counterofloops;
    public float loadingtextmessagetimer;
    public float loadingtime;

    #endregion

    #region Methods

    //Methods that check if object is active or disable to give a message to player or developer about the state of the loading inteface
    #region Enable and Disable Methods

    void OnEnable()
    {
        Debug.Log("Loading Interface Enable");
    }
    void OnDisable()
    {
        Debug.Log("Loading Interface Disable");
    }

    #endregion

    #region Awake And Start

    void Awake()
    {
        if (background == null)
            background = GetComponent<Image>();
        if (loadingtext == null)
            loadingtext = GetComponent<Text>();
    }
    void Start()
    {
        if (loadingtextmessagetimer == 0)
        {
            loadingtextmessagetimer = 50f;
        }
    }

    #endregion

    #region LoadingInterface Fundamental Methods

    public void UploadLoadingMessage()
    {
        if (loadingtextmessagetimer <= 0)
        {
            loadingtext.text = loadingmessegestext[counterofloops];
            counterofloops++;
            if(counterofloops >= loadingmessegestext.Length)
            {
                counterofloops = 0;
            }
            loadingtextmessagetimer = 50f;
        }
        else
        {
            loadingtextmessagetimer -= 1f;
        }
    }

    #endregion

    #endregion

}
