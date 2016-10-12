using UnityEngine;
using System.Collections;

public class AlbumMenu : MonoBehaviour {

    #region Attributes

    public GameObject gallerybox;
    public GameObject diarybox;
    public GameObject gameendsbox;

    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        Debug.Log("Album Active and Enable");
    }
    void OnDisable()
    {
        Debug.Log("Album Desactive");
    }

    #endregion

    #region Awake And Start

    void Start()
    {
        gallerybox.SetActive(false);
        diarybox.SetActive(false);
        gameendsbox.SetActive(false);
    }

    #endregion

    #region Buttons Methods
    public void GalleryButton()
    {
        if (!gallerybox.activeInHierarchy)
        {
            gameObject.SetActive(false);
            gameendsbox.SetActive(false);
            diarybox.SetActive(false);
            gallerybox.SetActive(true);
        }
    }
    public void DiaryButton()
    {
        if(!diarybox.activeInHierarchy)
        {
            gallerybox.SetActive(false);
            gameendsbox.SetActive(false);
            diarybox.SetActive(true);
        }
    }
    public void GameEndsButton()
    {
        if(!gameendsbox.activeInHierarchy)
        {
            gallerybox.SetActive(false);
            diarybox.SetActive(false);
            gameendsbox.SetActive(true);
        }
    }
    public void ReturnButton()
    {
        Application.LoadLevel(2);
    }

    #endregion

    #endregion
}
