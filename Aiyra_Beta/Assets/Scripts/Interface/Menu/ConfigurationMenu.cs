using UnityEngine;
using System.Collections;

public class ConfigurationMenu : MonoBehaviour {

    public GameData gamedata;
    public GameSettings gamesettings;
    public GameObject configwindowsbox;
    public GameObject howtoplaybox;
    public GameObject speakwithusbox;

    void Start()
    {
        gamedata.LoadLoadRequest();
        gamedata.LoadSaveRequest();
    }
    public void UpdateVolumeSettings()
    {
        gamesettings.musicvolume = gamesettings.volumesliders[0].value;
        gamesettings.effectsvolume = gamesettings.volumesliders[1].value;

        gamesettings.SaveAudioSettings();
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
}
