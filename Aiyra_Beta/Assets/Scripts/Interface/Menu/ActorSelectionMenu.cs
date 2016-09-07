using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorSelectionMenu : MonoBehaviour {

    public GameData gamedata;
    public GameObject actordescriptionbox;
    public Text actorname;
    public Image actorimage;
    public Text actordescription;
    public ActorButton[] actorsbuttons;

    public void DisplayActorDescription(ActorButton ActorToDisplay)
    {
        if (!actordescriptionbox.activeInHierarchy)
        {
            actordescriptionbox.SetActive(true);
            actorname.text = ActorToDisplay.actor.actorname;
            actorimage.sprite = ActorToDisplay.actor.actorimage;
            actordescription.text = ActorToDisplay.actor.actorbio;
        }
    }
    public void AcceptButton()
    {
        gamedata.SetPlayerCurrentActor(actorname.text);
        gamedata.SaveAllPlayerData();
        gamedata.SetSaveRequest(7);
        gamedata.SaveSaveRequest();
        Application.LoadLevel(7);
    }
    public void ReturnButton()
    {
        actordescriptionbox.SetActive(false);
    }
}
