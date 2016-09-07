using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorButton : MonoBehaviour {

    public Actor actor;
    public Text actornamebuttontext;
    public Image actorimagebuttonimage;

    void Start()
    {
        if (actornamebuttontext != null)
            actornamebuttontext.text = actor.actorname;
        if (actorimagebuttonimage != null)
            actorimagebuttonimage.sprite = actor.actorimage;
    }
}
