using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]

public class PlayerEyes : MonoBehaviour {
    
    #region Attributes

    public Image filter;
    public Animator animator;

    #endregion

    #region Methods

    #region Enable And Disable
    void OnEnable()
    {
        Debug.Log("Player Eye active and Enable");
    }

    void OnDisable()
    {
        Debug.Log("Player eye Desactive");
    }

    #endregion

    #region Awake And Start
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (filter == null)
            filter = GetComponent<Image>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    #endregion

    #endregion
}
