using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEndsMenu : MonoBehaviour {

    #region Attributes

    public GameObject albummenu;
    public GameObject endsmenu;
    public Text endstext;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("GameEnds Menu Active");
    }

    void OnDisable()
    {
        Debug.Log("GameEnds Menu Desactive");
    }

    #endregion

    #region Buttons Methods

    public void EndsButton(string EndsText)
    {
        endsmenu.SetActive(true);
        endstext.text = EndsText;
        gameObject.SetActive(false);
    }
    public void ReturnButton()
    {
        gameObject.SetActive(false);
        albummenu.SetActive(true);
    }

    #endregion

    #endregion

}
