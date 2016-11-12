using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectIInterface : MonoBehaviour {

    #region Attributes

    public GameController gamecontroller;

    public RectTransform objectiinterfacetransform;
    public Image objectiinterface;
    public BoxCollider2D objectiinterfacecollider;

    #endregion

    #region Methods

    #region Enable and Disable Methods

    void OnEnable()
    {
        Debug.Log("Object Interface " + gameObject.name + " Active ");
    }
    void OnDisable()
    {
        Debug.Log("Object Interface " + gameObject.name + " Desactive ");
    }

    #endregion

    #region Awake And Start Methods

    void Awake()
    {
        if (gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        if (objectiinterfacetransform == null)
            objectiinterfacetransform = GetComponent<RectTransform>();
        if (objectiinterface == null)
            objectiinterface = GetComponent<Image>();
        if (objectiinterfacecollider == null)
            objectiinterfacecollider = GetComponent<BoxCollider2D>();

        objectiinterfacecollider.size = new Vector2(objectiinterfacetransform.sizeDelta.x, objectiinterfacetransform.sizeDelta.y);
    }

    #endregion

    #region ObjectI Interface Fundamental Methods

    #region Mouse Over Methods
    //If object active on mouse over and click the gameobject close
    void OnMouseOver()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                gamecontroller.canprogress = true;
            }
        }
    }

    #endregion

    #endregion

    #endregion
}
