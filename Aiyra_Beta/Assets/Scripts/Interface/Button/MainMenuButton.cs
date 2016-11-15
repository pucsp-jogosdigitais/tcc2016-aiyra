using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class MainMenuButton : MonoBehaviour {

    #region Attributes

    public BoxCollider2D buttoncollider;
    public AudioClip buttonsoundeffect;
    public AudioSource buttonsound;

    public bool over;
    public bool hasbeenplayed;

    #endregion

    #region Methods

    #region Enable and Disable

    void OnEnable()
    {
        Debug.Log("Button " + gameObject.name + " Active");
    }
    void OnDisable()
    {
        Debug.Log("Button " + gameObject.name + " Desable");
    }

    #endregion

    #region Awake and Start Methods

    void Awake()
    {
        if (buttoncollider == null)
            buttoncollider = GetComponent<BoxCollider2D>();
        if (buttonsoundeffect == null)
            buttonsoundeffect = Resources.Load<AudioClip>("Sounds/Button/som OK 3 (online-audio-converter.com)");
        if (buttonsound == null)
            buttonsound = GetComponent<AudioSource>();
        if(buttonsound.clip == null)
            buttonsound.clip = buttonsoundeffect;

        buttoncollider.size = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
    }

    #endregion

    #region Update

    void Update()
    {
        if (over)
        {
            if (!buttonsound.isPlaying && !hasbeenplayed)
            {
                buttonsound.Play();
                hasbeenplayed = true;
            }
        }
        else
        {
            if (buttonsound.isPlaying)
            {
                buttonsound.Stop();
                hasbeenplayed = false;
            }
        }
    }

    #endregion

    #region MainMenuButton Fundamental Methods

    #region Mouse over Methods

    void OnMouseOver()
    {
        over = true;
    }
    void OnMouseExit()
    {
        over = false;
    }

    #endregion

    #endregion

    #endregion
}
