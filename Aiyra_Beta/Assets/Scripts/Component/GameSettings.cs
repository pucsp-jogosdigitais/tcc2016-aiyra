using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSettings : MonoBehaviour {

    private const string  musicvolumesavekey = "MUSICVOLUME";
    private const string  effectsvolumesavekey = "EFFECTSVOLUME";

    public Slider[] volumesliders; 

    public float musicvolume;
    public float effectsvolume;
    public float maxvolume;
    void Start()
    {
        LoadAudioSettings();

        volumesliders[0].maxValue = maxvolume;
        volumesliders[1].maxValue = maxvolume;

        DisplayCurrentVolume();  
    }
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
}
