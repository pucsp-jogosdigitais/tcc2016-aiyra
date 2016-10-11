using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorCG : MonoBehaviour {

    #region Keys

    //public string cgstatesavekey = "CG" +

    #endregion

    #region Attributes
    public Image cgimage;

    public string cgpath;
    public string cgname;

    public int cgid;

    public bool isunlock;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("CG " + cgid + " Active and Enable");
    }
    void OnDisable()
    {
        Debug.Log("CG " + cgid + " Disable");
    }

    #endregion

    #region Awake and Start Methods

    void Awake()
    {
        if (cgimage == null)
            cgimage = GetComponentInChildren<Image>();
    }

    #endregion

    #region Update Methods

    void Update()
    {
        if (isunlock)
        {
            Debug.Log("CG " + cgid + "Unlock");
            cgimage.sprite = Resources.Load<Sprite>(cgpath + cgname);
        }
        else
        {
            Debug.Log("CG not unlocked");
        }
    }
    #endregion

    #region ActorCG Fundamental Methods

    #region Set Values Methods

    public void SetCGID(int NewCGID)
    {
        cgid = NewCGID;
    }
    public void SetCGName(string NewCGName)
    {
        cgname = NewCGName;
    }
    public void SetCGPath(string NewPath)
    {
        cgpath = NewPath;
    }

    #endregion

    #endregion

    #endregion

}
