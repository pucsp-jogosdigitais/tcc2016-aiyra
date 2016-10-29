using UnityEngine;
using System;
using System.Collections;

public class CrossHeir : MonoBehaviour {

    #region attributes

    public Camera playercamera;
    public GameObject crossheir;

    #endregion

    #region methods

    //Methods that will work when the gameobject is enable or disable
    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Player CrossHeir Active");
    }
    void OnDisable()
    {
        Debug.Log("Player CrossHeir Disactive");
    }

    #endregion

    #region Awake And Start
     //Method that will run on the begining of the inicialize of the script and will get any component that is is missing
    void Awake()
    {
        if(playercamera == null)
            playercamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (crossheir == null)
            crossheir = GameObject.Find("CrossHeirImage").GetComponent<GameObject>();
    }
    //Method start that will hide the mouse default cursor and show game costum mouse cursor
    void Start()
    {
        Cursor.visible = false;
    }

    #endregion

    #region Update Methods
    void Update () {
        FollowMouse();
	}
    #endregion

    #region CrossHeir Fundamental Methods

    void FollowMouse()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Vector2 pos = playercamera.ScreenToWorldPoint(Input.mousePosition);
            crossheir.transform.position = pos;
        }
    }

    #endregion

    #endregion
}
