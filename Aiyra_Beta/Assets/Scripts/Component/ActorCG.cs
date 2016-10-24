using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorCG : MonoBehaviour {

    #region Attributes
    public Image cgimage;

    public string cgpath;
    public string cgname;

    public int cgid;

    public bool isunlock;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    //methods that check if the gameobject is active or not and answer the developer the case
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
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (cgimage == null)
            cgimage = GetComponentInChildren<Image>();
    }

    #endregion

    #region Update Methods

    public void UpdateCG()
    {
        if (isunlock)
        {
            Debug.Log("CG: " + cgid + "Unlock Load CG");
            cgimage.sprite = Resources.Load<Sprite>(cgpath + cgname);
        }
        else
        {
            Debug.Log("CG: " + cgid + "not unlocked cant load");
            cgimage.sprite = Resources.Load<Sprite>("Sprites/Buttons/AlbumMenu/CG_Locked");
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
