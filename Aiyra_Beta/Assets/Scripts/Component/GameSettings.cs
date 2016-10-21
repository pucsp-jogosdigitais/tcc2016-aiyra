using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSettings : MonoBehaviour {

    #region Attributes

    private const string musicvolumesavekey = "MUSICVOLUME";
    private const string effectsvolumesavekey = "EFFECTSVOLUME";

    public Slider[] volumesliders;

    public float musicvolume;
    public float effectsvolume;
    public float maxvolume;

    #endregion

    #region Methods

    #region Awake And Start

    void Start()
    {
        LoadAudioSettings();

        if (volumesliders.Length > 0)
        {
            volumesliders[0].maxValue = maxvolume;
            volumesliders[1].maxValue = maxvolume;

            DisplayCurrentVolume();
        }
    }

    #endregion

    #region Game Settings Fundamental Methods

    public void DisplayCurrentVolume()
    {
        volumesliders[0].value = musicvolume;
        volumesliders[1].value = effectsvolume;
    }
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(musicvolumesavekey, musicvolume);
        PlayerPrefs.SetFloat(effectsvolumesavekey, effectsvolume);
    }
    public void LoadAudioSettings()
    {
        musicvolume = PlayerPrefs.GetFloat(musicvolumesavekey);
        effectsvolume = PlayerPrefs.GetFloat(effectsvolumesavekey);
    }

    #endregion

    #endregion

}
