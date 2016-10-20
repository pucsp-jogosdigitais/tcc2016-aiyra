using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour {

    #region Animator Keys Reference

    public string motionreference = "Motion";

    #endregion

    #region Attributes

    public SimpleModel actormodel;

    public Sprite actorimage;

    public Animator actoranimator;

    public string actorname;

    public string actorbio;

    public int affinitypoints;
    public int[] dialoglines;

    public bool hasdialog;

    #endregion

    #region Methods

    #region Enable and Disable

    void OnEnable()
    {
        Debug.Log(actorname + " has enter in scene for dialog ");
    }
    void OnDisable()
    {
        Debug.Log(actorname + " has left the scene ");
    }

    #endregion

    #endregion

}
