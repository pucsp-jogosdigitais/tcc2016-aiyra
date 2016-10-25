using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorButton : MonoBehaviour {

    #region Attributes

    public Actor actor;
    public BoxCollider2D buttoncollider;
    public Image buttonframe;
    public Sprite[] buttonframestates;
    public Text actornamebuttontext;
    public Image actorimagebuttonimage;

    #endregion

    #region Methods

    #region Awake And Start Methods
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (buttoncollider == null)
            if(GetComponent<BoxCollider2D>() != null)
                buttoncollider = GetComponent<BoxCollider2D>();

        if (actor != null)
        {
            if (actornamebuttontext != null)
                actornamebuttontext.text = actor.actorname;
            if (actorimagebuttonimage != null)
                actorimagebuttonimage.sprite = actor.actorimage;
        }
    }

    void Start()
    {
        if (buttonframe != null && buttonframestates[0] != null)
            buttonframe.sprite = buttonframestates[0];
        if(buttoncollider != null)
            buttoncollider.size = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
    }
    #endregion

    #region OnMouseOver And OnMouseExit Methods
    //On Mouse Over the collider of the button make glow the frame else return frame to normal state
    void OnMouseOver()
    {
        buttonframe.sprite = buttonframestates[1];
    }
    void OnMouseExit()
    {
        buttonframe.sprite = buttonframestates[0];
    }

    #endregion

    #endregion

}
