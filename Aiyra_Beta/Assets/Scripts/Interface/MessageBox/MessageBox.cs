using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class MessageBox : MonoBehaviour {

    #region Attributes

    public GameController gamecontroller;

    public Image messageimage;

    public string messagepath;
    public string messagename;

    public float messagetime;

    public bool hasbeendisplayed;
    public bool hasbeenloaded;

    #endregion

    #region Methods

    #region Enable And Disable

    void OnEnable()
    {
        Debug.Log("MessageBox Active");
    }
    void OnDisable()
    {
        Debug.Log("MessageBox Desactive");
    }

    #endregion

    #region Awake And Start Methods

    void Awake()
    {
        if (gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        if (messageimage == null)
            messageimage = GetComponent<Image>();
    }

    #endregion

    #region MessageBox Fundamentals Methods

    #region Set Message Values Methods

    public void SetMessagePath(string NewPath)
    {
        messagepath = NewPath;
        hasbeendisplayed = false;
    }
    public void SetMessageName(string NewMessageName)
    {
        messagename = NewMessageName;
        hasbeendisplayed = false;
    }
    public void SetMessageTime(float NewMessageTime)
    {
        messagetime = NewMessageTime;
    }

    #endregion

    #region Upload Message Methods

    public void UploadMessageBox()
    {
        if (!hasbeenloaded)
        {
            messageimage.sprite = Resources.Load<Sprite>(messagepath + messagename);
            hasbeenloaded = true;
        }
        if(messagetime <= 0)
        {
            hasbeendisplayed = true;
        }
        else
        {
            hasbeendisplayed = false;
        }
    }

    #endregion

    #endregion

    #endregion
}
