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
    //Method that on work display the actor on the selected partner menu update it values and change the frame state
    public void DisplayActorDescription(ActorButton ActorToDisplay)
    {
        if (!actordescriptionbox.activeInHierarchy)
        {
            if (ActorToDisplay.buttonframestates[2] != null && ActorToDisplay.buttonframe != null)
                ActorToDisplay.buttonframe.sprite = ActorToDisplay.buttonframestates[2];

            actordescriptionbox.SetActive(true);
            actorname.text = ActorToDisplay.actor.actorname;
            actorimage.sprite = ActorToDisplay.actor.actorimage;
            actordescription.text = ActorToDisplay.actor.actorbio;
        }
    }
    //Method that if in work save the current actor selected and load game scene
    public void AcceptButton()
    {
        gamedata.SetPlayerCurrentActor(actorname.text);
        gamedata.ResetAffinitys();
        gamedata.SaveAllPlayerData();
        gamedata.SetSaveRequest(7);
        gamedata.SaveSaveRequest();
        Application.LoadLevel(7);
    }
    //A simple return method that desactive the selected actor menu
    public void ReturnButton()
    {
        actordescriptionbox.SetActive(false);
    }

    #endregion

    #endregion

}
