using UnityEngine;
using System.Collections;

public class ConfigurationMiniMenu : MonoBehaviour {

    #region Buttons Methods
    public void GoToURL(string URL)
    {
        Application.OpenURL(URL);
    }
    public void ReturnButton()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
