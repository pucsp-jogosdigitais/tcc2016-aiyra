using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameInputMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public Text nametext;
    public GameObject confirmbox;
    public Text confirmtext;

    public string[] nameletters;
    public string namecomplete;
    public int namelimit;
    public int currentletter;

    #endregion

    #region Methods

    #region Awake And Start Methods

    void Start()
    {
        namelimit = nameletters.Length - 1;

        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();
    }

    #endregion

    #region Methods for buttons

    #region Main Buttons Methods

    public void ButtonLetter(string letter)
    {
        if (currentletter < namelimit)
        {
            nameletters[currentletter] = letter;
            namecomplete = namecomplete + nameletters[currentletter];
            nametext.text = namecomplete;
            currentletter++;
        }
    }
    public void ButtonDelete()
    {
        if (currentletter > 0)
        {
            for (int i = nameletters.Length - 1; i > -1; i--)
            {
                nameletters[currentletter] = " ";
                currentletter = i;
            }
            namecomplete = " ";
            nametext.text = namecomplete;
        }
    }
    public void ButtonAccept()
    {
        if (namecomplete.Length > 0)
        {
            confirmtext.text = nametext.text;
            confirmbox.SetActive(true);
        }
    }

    #endregion

    #region ConfirmBox Buttons

    public void ButtonConfirm(int Level)
    {
        gameObject.SetActive(false);
        confirmbox.SetActive(false);

        gamedata.SetPlayerName(nametext.text);
        gamedata.SavePlayerName();

        Application.LoadLevel(Level);
    }
    public void ButtonCancel()
    {
        confirmbox.SetActive(false);
    }

    #endregion

    #endregion

    #endregion
}
