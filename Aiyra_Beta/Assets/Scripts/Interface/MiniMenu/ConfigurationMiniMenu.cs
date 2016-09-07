using UnityEngine;
using System.Collections;

public class ConfigurationMiniMenu : MonoBehaviour {

	public void GoToURL(string URL)
    {
        Application.OpenURL(URL);
    }
    public void ReturnButton()
    {
        gameObject.SetActive(false);
    }
}
