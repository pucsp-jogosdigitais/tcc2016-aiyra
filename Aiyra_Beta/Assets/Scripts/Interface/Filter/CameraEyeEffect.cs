using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraEyeEffect : MonoBehaviour {

    #region Attributes

    public PlayerEyes playereyes;

    public enum effectstate { Open, Blink, Close };
    public effectstate state;

    public int[] moments;
    public bool hasaction;
    public bool doaction;

    #endregion

    #region Methods

    #region Enable And Disable

    void OnEnable()
    {
        Debug.Log("Player Eye Filter Active");
    }

    void OnDisable()
    {
        Debug.Log("Player Eye Filter Desactive");
    }

    #endregion

    #region Awake and Start
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (playereyes == null)
            GameObject.Find("PlayerEyesEffect").GetComponent<PlayerEyes>();
    }

    #endregion

    #region Camera Eyes Effect Update Methods

    public void CameraEyesUpdate()
    {
        if (playereyes.animator.isActiveAndEnabled)
        {
            if (hasaction || doaction)
            {
                playereyes.animator.SetBool("NoFilter", false);
                playereyes.animator.SetBool("Open", false);
                playereyes.animator.SetBool("Blink", false);
                playereyes.animator.SetBool("Close", false);
                playereyes.animator.SetBool(state.ToString(), true);
            }
            else
            {
                playereyes.animator.SetBool("Open", false);
                playereyes.animator.SetBool("Blink", false);
                playereyes.animator.SetBool("Close", false);
                playereyes.animator.SetBool("NoFilter", true);
            }
        } 
    }

    #endregion

    #endregion

}
