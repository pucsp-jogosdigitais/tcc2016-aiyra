using UnityEngine;
using System.Collections;

public class ConfigurationMenu : MonoBehaviour {

    #region Attributes

    public GameData gamedata;
    public GameSettings gamesettings;
    public GameObject configaudiobox;
    public GameObject configwindowsbox;
    public GameObject howtoplaybox;
    public GameObject speakwithusbox;

    #endregion

    #region Methods

    #region Awake And Start

    void Start()
    {
        configaudiobox.SetActive(false);
        configwindowsbox.SetActive(false);
        howtoplaybox.SetActive(false);
        speakwithusbox.SetActive(false);

        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();
    }

    #endregion

    #region Update Volume Settings

    public void UpdateVolumeSettings()
    {
        gamesettings.musicvolume = gamesettings.volumesliders[0].value;
        gamesettings.effectsvolume = gamesettings.volumesliders[1].value;

        gamesettings.SaveAudioSettings();
    }

    #endregion

    #region Buttons Methods

    public void DisplayConfigAudio()
    {
        if (!configaudiobox.activeInHierarchy)
            configaudiobox.SetActive(true);
    }
    public void DisplayConfigWindowsText()
    {
        if (!configwindowsbox.activeInHierarchy)
            configwindowsbox.SetActive(true);
    }
    public void ReturnToDefault()
    {
        gamesettings.volumesliders[0].value = 0.6f;
        gamesettings.volumesliders[1].value = 0.8f;
    }
    public void DisplayHowToPlay()
    {
        if (!howtoplaybox.activeInHierarchy)
            howtoplaybox.SetActive(true);
    }
    public void DisplaySpeakWithUsBox()
    {
        if (!speakwithusbox.activeInHierarchy)
            speakwithusbox.SetActive(true);
    }
    public void ReturnButton()
    {
        if (gamedata.loadrequest > 0 || gamedata.saverequest > 0)
            Application.LoadLevel(7);
        else
        {
            gamedata.SetLoadRequest(-1);
            gamedata.SaveLoadRequest();
            gamedata.SetSaveRequest(-1);
            gamedata.SaveSaveRequest();
            Application.LoadLevel(2);
        }
    }

    #endregion

    #endregion

}
