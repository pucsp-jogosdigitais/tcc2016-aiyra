using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActorSelectionMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public GameObject actordescriptionbox;
    public Text actorname;
    public Image actorimage;
    public Text actordescription;
    public ActorButton[] actorsbuttons;

    #endregion

    #region Methods

    #region Awake And Start

    void Start()
    {
        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();
    }

    #endregion

    #region Buttons Methods
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
        gamedata.ResetAffinitys();
        gamedata.SaveAllPlayerData();
        gamedata.SetSaveRequest(7);
        gamedata.SaveSaveRequest();
        Application.LoadLevel(7);
    }
    public void ReturnButton()
    {
        actordescriptionbox.SetActive(false);
    }

    #endregion

    #endregion

}
