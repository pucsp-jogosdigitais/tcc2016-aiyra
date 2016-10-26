using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogProcedIcon : MonoBehaviour {

    #region Attributes

    public Image procedicon;

    public Sprite[] procedstatesicon;

    public Animator procediconanimator;

    public int currentstate;

    #endregion

    #region Methods

    #region Enable And Disable

    void OnEnable()
    {
        Debug.Log("DialogBoxProcedIcon active");
    }
    void OnDisable()
    {
        Debug.Log("DialogBoxProcedIcon disable");
    }

    #endregion

    #region Awake And Start

    void Awake()
    {
        if (procedicon == null)
            procedicon = GetComponent<Image>();
        if (procedstatesicon.Length <= 0)
            procedstatesicon = new Sprite[2] { Resources.Load<Sprite>("Sprites/Buttons/AlbumMenu/Seta"), Resources.Load<Sprite>("Sprites/Buttons/AlbumMenu/Seta_Click") };
        if (procediconanimator == null)
            procediconanimator = GetComponent<Animator>();
    }
    void Start ()
    {
        UploadDialogProcedIcon();
	}

    #endregion

    #region DialogProcedIcon Fundamental Methods

    #region Set Value Methods

    public void SetIconCurrentState(int NewCurrentState)
    {
        currentstate = NewCurrentState;
    }

    #endregion

    #region Upload Methods

    public void UploadDialogProcedIcon()
    {
        procedicon.sprite = procedstatesicon[currentstate];
    }

    #endregion

    #endregion

    #endregion
}
