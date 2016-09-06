using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSlot : MonoBehaviour {

    //Keys
    public string saveslotchaptersavekey;
    public string saveslotactorsavekey;
    public string saveslotdatasavekey;
    public string saveslottimesavekey;
    //

    public int saveslotid;
    public Text saveslotchapter;
    public Image saveslotactor;
    public Text saveslotdata;
    public Text saveslottime;
    public Button saveslotbutton;

    public int saveslotcurrentscene;
    public int saveslotcurrentdialogtextfile;
    public int saveslotcurrentdialogline;

    public void SaveData()
    {
        PlayerPrefs.SetString(saveslotchaptersavekey, saveslotchapter.text);
        PlayerPrefs.SetString(saveslotactorsavekey, saveslotactor.name);
        PlayerPrefs.SetString(saveslotdatasavekey, saveslotdata.text);
        PlayerPrefs.SetString(saveslottimesavekey, saveslottime.text);
    }
	public void LoadData()
    {
        saveslotchapter.text = PlayerPrefs.GetString(saveslotchaptersavekey);
        //saveslotactor.sprite = PlayerPrefs.GetString(saveslotactorsavekey);
        saveslotdata.text = PlayerPrefs.GetString(saveslotdatasavekey);
        saveslottime.text = PlayerPrefs.GetString(saveslottimesavekey);
    }
}
