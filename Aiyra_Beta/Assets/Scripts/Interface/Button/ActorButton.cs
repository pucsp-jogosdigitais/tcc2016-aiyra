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

    void Awake()
    {
        if (actornamebuttontext != null)
            actornamebuttontext.text = actor.actorname;
        if (actorimagebuttonimage != null)
            actorimagebuttonimage.sprite = actor.actorimage;
    }

    #endregion

    #endregion

}
