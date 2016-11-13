using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Request the components  Audiosource, Image, BoxCollider2D
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]

public class ObjectI : MonoBehaviour {

    #region Keys
    public string objectinventarystatussavekey;
    #endregion

    #region Attributes

    // Create the variables that hold the important information 
    public GameController gamecontroller;
    public Scene scene;
    public RectTransform objecttransform;
    public AudioSource objectsound;
    public Image objectimage;
    public BoxCollider2D objectcollider;
    public Sprite objectnormal;
    public Sprite objecthighlighted;
    public Sprite objectclicked;
    public GameObject conectedobject;


    public string objectname;
    public int isininventary;

    public bool isavailable;
    public bool hasbeenloaded;

    #endregion

    #region methods

    #region Enable and Disable Methods
    void OnEnable()
    {
        Debug.Log("Object " + objectname + " Active");
        LoadObjectStatus();
    }

    void OnDisable()
    {
        Debug.Log("Object " + objectname + " Desactive");
    }
    #endregion

    #region Awake And Start Methods
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if(isininventary >= 0)
            isavailable = true;

        if (gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        if (objecttransform == null)
            objecttransform = GetComponent<RectTransform>();
        if (objectsound == null)
            objectsound = GetComponent<AudioSource>();
        if (objectimage == null)
            objectimage = GetComponent<Image>();
        if (objectcollider == null)
            objectcollider = GetComponent<BoxCollider2D>();

        objectcollider.size = new Vector2(objecttransform.sizeDelta.x, objecttransform.sizeDelta.y);

        if (objectnormal == null)
            Debug.Log("Please put the object normal sprite in the variable objectnormal");

        if (objectname == "" || objectname.Length <= 0)
            objectname = gameObject.name;

        UploadObjectISaveKey();
    }
    void Start()
    {
        LoadObjectStatus();
    }
    #endregion

    #region Mouse Interaction Methods

    void OnMouseOver()
    {
        if (!gamecontroller.pausemenu.gameObject.activeInHierarchy)
        {
            if(scene != null)
                if (scene.scenestate == Scene.state.interaction)
                        if (objecthighlighted != null)
                            objectimage.sprite = objecthighlighted;
        }

        if (Input.GetButtonDown("Interaction"))
        {
            if (scene.scenestate == Scene.state.interaction)
            {
                if (!gamecontroller.pausemenu.gameObject.activeInHierarchy)
                {

                    #region Doors

                    if (gamecontroller.canprogress)
                    {
                        if (isavailable)
                        {
                            if (gameObject.name == "BedroomDoor")
                            {
                                PlayInteractionSound();
                                if (gamecontroller.player.inventary[0] == "Broche")
                                {
                                    gamecontroller.currentscene = 2;
                                    gamecontroller.dialogbox.StartDialog(0);
                                }
                                else
                                {
                                    //testing
                                    gamecontroller.messagebox.gameObject.SetActive(true);
                                }
                            }
                            if (gameObject.name == "HouseDoor")
                            {
                                PlayInteractionSound();
                                if (gamecontroller.player.inventary[1] == "LivingroomDiaryBox")
                                {
                                    gamecontroller.currentscene = 3;
                                    gamecontroller.dialogbox.StartDialog(0);
                                }
                                else
                                {
                                    //testing
                                    gamecontroller.messagebox.gameObject.SetActive(true);
                                }
                            }
                            if (gameObject.name == "NormalClassroomDoor")
                            {
                                PlayInteractionSound();
                                gamecontroller.currentscene = 5;
                                gamecontroller.dialogbox.StartDialog(0);
                            }
                        }
                    }

                    #endregion

                    #region Pickable Objetcs

                    if (isavailable)
                    {
                        if (gameObject.name == "Broche")
                        {
                            PlayInteractionSound();
                            OnClickRewardPlayerWithObject();
                        }
                        else if (gameObject.name == "LivingroomDiaryBox")
                        {
                            PlayInteractionSound();
                            OnClickPickUpObjectAndDisplay();
                            OnClickRewardPlayerWithObject();
                            GoToDialog(5, 0);
                        }
                        /*
                        if (gameObject.name == "MainRoomPicture")
                        {
                            OnClickPickUpObjectAndDisplay();
                        }
                        if (gameObject.name == "")
                        {
                            OnClickPickUpObjectAndDisplay();
                        }
                        */
                    }

                    #endregion

                    #region Objects

                    if (gamecontroller.canprogress)
                    {
                        if (isavailable)
                        {
                            #region Bedroom Objects

                            if (gameObject.name == "BedroomLightSwitch")
                            {
                                PlayInteractionSound();
                                ChangeBackground(1, 0);
                            }
                            else if (gameObject.name == "BedroomAlarm")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomBox")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomDoorpicture")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomManga")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomMythologyBook")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomComputer")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomPoster1")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomPoster2")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomPoster3")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomPoster4")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "BedroomPoster5")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }

                        }

                        #endregion

                            #region Livingroom Objects

                            else if (gameObject.name == "LivingroomLawsBooks")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "LivingroomNursingBooks")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }
                            else if (gameObject.name == "LivingroomDoorPicture")
                            {
                                PlayInteractionSound();
                                OnClickPickUpObjectAndDisplay();
                            }

                            #endregion
                    }

                    #endregion

                    if (objectclicked != null)
                        objectimage.sprite = objectclicked;

                    #region Especial Cases
                    //Need to check if the player has the broche on it inventory to the game can processes
                    #endregion

                    Debug.Log("Vocé clicou no objeto " + gameObject.name);
                }
            }
            else { Debug.Log("Scene not in interaction"); }
        }
    }
    void OnMouseExit()
    {
        objectimage.sprite = objectnormal;
    }

    #endregion

    #region ObjectI Fundamental Methods

    #region Update ObjectI Status And Values

    public void UploadObjectISaveKey()
    {
        objectinventarystatussavekey = "OBJECT" + gameObject.name + "SAVEKEY";
    }

    #endregion

    #region Feedback

    public void Changebackground(Color NewBackgroundColor)
    {
        Debug.Log("Background with new color");
    }
    //Method that set the background
    public void ChangeBackground(int NewBackground,int NormalStateBackground)
    {
        if(gamecontroller.background.currentbackground == NormalStateBackground)
        {
            gamecontroller.background.currentbackground = NewBackground;
            //gamecontroller.canprogress = false;
            foreach (GameObject objecti in gamecontroller.scenes[gamecontroller.currentscene].objects)
            {
                if (objecti.GetComponent<ObjectI>().objectimage != null)
                {
                    objecti.GetComponent<ObjectI>().objectimage.color = new Color(0.1f, 0.1f, 0.1f, 1f); /*new Color(0.6f, 0.6f, 0.6f);*/
                }
            }
        }

        if(gamecontroller.background.currentbackground == NewBackground)
        {
            gamecontroller.background.currentbackground = NormalStateBackground;
            //gamecontroller.canprogress = true;
            foreach (GameObject objecti in gamecontroller.scenes[gamecontroller.currentscene].objects)
            {
                if (objecti.GetComponent<ObjectI>().objectimage != null)
                {
                    objecti.GetComponent<ObjectI>().objectimage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                }
            }
        }
    }
    // metodo que serve para mostrar o objeto conectado com o objeto
    public void OnClickPickUpObjectAndDisplay()
    {
        if (conectedobject != null)
        {
            if (!conectedobject.activeInHierarchy)
            {
                conectedobject.SetActive(true);
                gamecontroller.canprogress = false;
            }
        }
    }
    //Method that reward player with the gameobject that this script is associeted
    public void OnClickRewardPlayerWithObject()
    {
        if (isininventary >= 0)
        {
            if (gameObject.name == "Broche")
            {
                if (gamecontroller.player.inventary[0].Length <= 0)
                    gamecontroller.player.inventary[0] = gameObject.name;

                isininventary = -1;
                SaveObjectStatus();
                //destroy but need test Destroy(gameObject);
            }
            if(gameObject.name == "LivingroomDiaryBox")
            {
                if (gamecontroller.player.inventary[1].Length <= 0)
                    gamecontroller.player.inventary[1] = gameObject.name;

                isininventary = -1;
                SaveObjectStatus();
                //Destroy(gameObject);
            }
        }
    }
    //Method that force the player to go back to dialog
    public void GoToDialog(int DialogToGo,int DialogLineToGo)
    {
        gamecontroller.scenes[gamecontroller.currentscene].scenestate = Scene.state.dialog;
        gamecontroller.dialogbox.currentdialog = DialogToGo;
        gamecontroller.dialogbox.dialog.currentdialogline = DialogLineToGo;
    }
    //Method that play the object interaction sound
    public void PlayInteractionSound()
    {
        if (!objectsound.isPlaying)
        {
            objectsound.Play();
        }
        if (objectsound.isPlaying)
        {
            objectsound.Stop();
            gamecontroller.canprogress = true;
        };
    }
    // method that serves to give a message to the player
    public void DisplayTextAboutObject()
    {
    }

    #endregion

    #region Object Status Save Methods
    //Method that save the object status
    public void SaveObjectStatus()
    {
        PlayerPrefs.SetInt(objectinventarystatussavekey, isininventary);
    }

    #endregion

    #region Object Status Load Methods

    //Method that load the object status
    public void LoadObjectStatus()
    {
        isininventary = PlayerPrefs.GetInt(objectinventarystatussavekey);
        if (isininventary < 0)
            isavailable = false;
        else { isavailable = true; }
    }

    #endregion

    #endregion

    #endregion

}
