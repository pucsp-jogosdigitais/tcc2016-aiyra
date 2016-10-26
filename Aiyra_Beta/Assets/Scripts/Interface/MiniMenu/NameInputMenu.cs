using UnityEngine;
using UnityEngine.UI;
using System;
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
    public float currentletter;

    public float buttontimer;

    #endregion

    #region Methods

    #region Awake And Start Methods
    //Method that define the namelimit based on the array of the name and load the gamedata data
    void Start()
    {
        namelimit = nameletters.Length - 1;

        gamedata.LoadAllPlayerData();
        gamedata.LoadAllGameData();
    }

    #endregion

    #region Methods for buttons

    #region Main Buttons Methods
    //Method that is associed with button of input letter to the name of the player this method receve a parameter of type string and when clicked give to the  current letter space of the complete name it parameter as values
    public void ButtonLetter(string letter)
    {

        if (buttontimer <= 0)
        {
            if (currentletter < namelimit)
            {
                nameletters[Convert.ToInt32(currentletter)] = letter;
                Debug.Log(nameletters[Convert.ToInt32(currentletter)]);
                namecomplete = namecomplete + nameletters[Convert.ToInt32(currentletter)];
                nametext.text = namecomplete;
                if (currentletter == 0)
                    currentletter += 1;
                else { currentletter += 0.5f; }
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.1f; Debug.Log("Timer to libere button function: " + buttontimer); }
    }
    //Method that delete all name write until now
    public void ButtonDelete()
    {
        if (buttontimer <= 0)
        {
            if (currentletter > 0)
            {
                for (int i = nameletters.Length - 1; i > -1; i--)
                {
                    nameletters[Convert.ToInt32(currentletter)] = " ";
                    currentletter = i;
                }
                namecomplete = " ";
                nametext.text = namecomplete;
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.1f; Debug.Log("Timer to libere button function: " + buttontimer); }
    }
    //Method that accept the name that player has write and pass it to the confirm box
    public void ButtonAccept()
    {
        if (buttontimer <= 0)
        {
            if (namecomplete.Length > 0)
            {
                confirmtext.text = nametext.text;
                confirmbox.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Write a letter for have a name");
            }
            buttontimer = 0.1f;
        }
        else { buttontimer -= 0.1f; Debug.Log("Timer to libere button function: " + buttontimer); }
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
