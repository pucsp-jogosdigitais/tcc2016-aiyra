using UnityEngine;
using System.Collections;

public class CollectionData : MonoBehaviour {

    #region Keys

    public string currentgallerystatussavekey = "";

    public string currentendstatussavekey = "";

    #endregion

    #region Attributes

    public ActorCG actorcg;

    #endregion

    #region Methods

    #region Enable And Disable
    //methods that alert the developer if gameobject is active or not
    void OnEnable()
    {
        Debug.Log("Collection Data Active And Enable");
    }
    void OnDisable()
    {
        Debug.Log("Collection Data Desactive");
    }

    #endregion

    #region CollectionData Fundamental Methods

    #region SetValueMethod

    public void SetGalleryStatusSaveKey(string NewGalleryStatusSaveKeyValue)
    {
        currentgallerystatussavekey = NewGalleryStatusSaveKeyValue;
    }
    public void SetEndStatusSaveKey(string NewEndStatusSaveKeyValue)
    {
        currentendstatussavekey = NewEndStatusSaveKeyValue;
    }
    public void SetActorCG(ActorCG NewActorCG)
    {
        actorcg = NewActorCG;
    }

    #endregion

    #region Save Methods

    public void SaveSpecficActorCGStatus()
    {
        if (actorcg != null)
        {
            SetGalleryStatusSaveKey(actorcg.name);
            Debug.Log("Saving on save key: " + actorcg.name);

            if (PlayerPrefs.HasKey(actorcg.name) == true)
            {
                Debug.Log("CG: " + actorcg.name + " Status been overwriting as " + actorcg.isunlock.ToString() + " In save key " + actorcg.name);
            }
            else
            {
                Debug.Log(" Create save key with name " + actorcg.name + " and save cg: " + actorcg.name + " status" );
            }

            PlayerPrefs.SetString(currentgallerystatussavekey, actorcg.isunlock.ToString());
        }
        else
        {
            Debug.LogWarning("No CG variable cant generete save key name and save");
        }
    }
    public void SaveActorCGStatusWithCGName(string CGName,bool UnlockStatus)
    {
        if(CGName.Length > 0)
        {
            SetGalleryStatusSaveKey(CGName);
            Debug.Log("Saving on save key: " + CGName);

            if (PlayerPrefs.HasKey(CGName) == true)
            {
                Debug.Log("CG: " + CGName + " Status been overwriting as " + UnlockStatus.ToString() + " In save key " + CGName);
            }
            else
            {
                Debug.Log(" Create save key with name " + CGName + " and save cg: " + CGName + " status");
            }

            PlayerPrefs.SetString(currentgallerystatussavekey, UnlockStatus.ToString());
        }
        else
        {
            Debug.LogError("The CGName have not a single letter impossible to save");
        }
    }

    #endregion

    #region Load Methods

    public void LoadSpecficActorCGStatus()
    {
        if (actorcg != null)
        {
            SetGalleryStatusSaveKey(actorcg.name);
            Debug.Log("Loading from save key: " + actorcg.name);

            if (PlayerPrefs.HasKey(currentgallerystatussavekey) == true)
            {
                Debug.Log("Save Key Exist Loading save key");

                if (PlayerPrefs.GetString(currentgallerystatussavekey) == "True")
                {
                    actorcg.isunlock = true;
                }
                else
                {
                    actorcg.isunlock = false;
                }
            }
            else
            {
                Debug.LogWarning("For the CG: " + currentgallerystatussavekey + " Have no save key");
            }
        }
        else
        {
            Debug.LogError("No ActorCG specficed");
        }
    }
    public void LoadActorCGStatusWithCGName(string CGName)
    {
        if (CGName.Length > 0)
        {
            SetGalleryStatusSaveKey(CGName);
            Debug.Log("Loading from save key: " + CGName);

            if (PlayerPrefs.HasKey(currentgallerystatussavekey) == true)
            {
                Debug.Log("Save Key Exist Loading save key");

                if (PlayerPrefs.GetString(currentgallerystatussavekey) == "True")
                {
                    actorcg.isunlock = true;
                }
                else
                {
                    actorcg.isunlock = false;
                }
            }
            else
            {
                Debug.LogWarning("For the CG: " + currentgallerystatussavekey + " Have no save key");
            }
        }
        else
        {
            Debug.LogError("The CGName have not a single letter impossible to load");
        }
    }

    #endregion

    #endregion

    #endregion

}
