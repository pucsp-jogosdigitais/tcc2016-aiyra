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
    public GameObject conectedobject;


    public string objectname;
    public int isininventary;

    public bool isavailable;

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

        if (objectname == "" || objectname.Length <= 0)
            objectname = gameObject.name;

        if (objectinventarystatussavekey == "" || objectinventarystatussavekey.Length <= 0)
            objectinventarystatussavekey = "OBJECT" + objectname + "SAVEKEY";
    }
    void Start()
    {
        LoadObjectStatus();
    }
    #endregion

    #region Mouse Interaction Methods

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Interaction"))
        {

            if (scene.scenestate == Scene.state.interaction)
            {
                #region Doors

                if (gamecontroller.canprogress)
                {
                    if (isavailable)
                    {
                        if (gameObject.name == "BedroomDoor")
                        {
                            PlayInteractionSound();
                            gamecontroller.currentscene = 2;
                            gamecontroller.dialogbox.StartDialog(0);
                        }
                        if (gameObject.name == "HouseDoor")
                        {
                            PlayInteractionSound();
                            gamecontroller.currentscene = 3;
                            gamecontroller.dialogbox.StartDialog(0);
                        }
                    }
                }

                #endregion

                #region Pickable Objetcs

                if (isavailable)
                {
                    if (gameObject.name == "MainRoomPicture")
                    {
                        OnClickPickUpObjectAndDisplay();
                    }
                }

                #endregion

                #region Objects
   
                #endregion

                Debug.Log("Vocé clicou no objeto " + gameObject.name);
            }
            else { Debug.Log("Scene not in interaction"); }
        }
    }

    #endregion

    #region ObjectI Fundamental Methods

    #region Feedback

    public void ChangeBackgroundColor(Color NewBackgroundColor)
    {
        if (NewBackgroundColor != GameObject.Find("Background").GetComponent<Image>().color)
        {
            GameObject.Find("Background").GetComponent<Image>().color = NewBackgroundColor;
            gamecontroller.canprogress = false;
        }
        else
        {
            GameObject.Find("Background").GetComponent<Image>().color = Color.white;
            gamecontroller.canprogress = true;
        }
    }
    // metodo que serve para mostrar o objeto conectado com o objeto
    public void OnClickPickUpObjectAndDisplay()
    {
        if (!conectedobject.activeInHierarchy)
        {
            conectedobject.SetActive(true);
            gamecontroller.canprogress = false;
        }
        else
        {
            conectedobject.SetActive(false);
            gamecontroller.canprogress = true;
        }
    }
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
        }
    }
    // metodo que serve para mostra caixa de texto e o texto sobre o objeto
    public void DisplayTextAboutObject()
    {
    }

    #endregion

    #region Object Status Save Methods

    public void SaveObjectStatus()
    {
        PlayerPrefs.SetInt(objectinventarystatussavekey, isininventary);
    }

    #endregion

    #region Object Status Load Methods

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
