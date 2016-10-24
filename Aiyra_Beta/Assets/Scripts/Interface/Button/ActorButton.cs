using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorButton : MonoBehaviour {

    #region Attributes

    public Actor actor;
    public Text actornamebuttontext;
    public Image actorimagebuttonimage;

    #endregion

    #region Methods

    #region Awake And Start Methods
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (actor != null)
        {
            if (actornamebuttontext != null)
                actornamebuttontext.text = actor.actorname;
            if (actorimagebuttonimage != null)
                actorimagebuttonimage.sprite = actor.actorimage;
        }
    }

    #endregion

    #endregion

}
