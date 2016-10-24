using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    #region Attributes
    public enum MusicPlayerBehaviour { random, inorder, justone}
    public MusicPlayerBehaviour musicplayerbehaviour;

    public AudioSource music;

    public int currentmusicclip;
    public int endatmusicclip;

    public bool isreplayable;
    #endregion

    #region Methods

    #region Enable and Disable Methods

    void OnEnable()
    {
        Debug.Log("Music Player active");
    }
    void OnDisable()
    {
        Debug.Log("Music Player desactive");
    }

    #endregion

    #region Awake and Start Methods
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (music == null)
            music = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (music != null)
        {
            if (musicplayerbehaviour == MusicPlayerBehaviour.justone)
            {
                Restart();
                PlayMusic();
            }
            if (musicplayerbehaviour == MusicPlayerBehaviour.inorder)
            {
                Restart();
                PlayMusic();
            }
        }

    }

    #endregion

    #region MusicPlayer Fundamental Methods

    public void Restart()
    {
        currentmusicclip = 0;
    }
    public void NextMusic()
    {
        if (musicplayerbehaviour == MusicPlayerBehaviour.inorder)
        {
            if (currentmusicclip < endatmusicclip)
            {
                if (!music.isPlaying)
                {
                    currentmusicclip++;
                    PlayMusic();
                }
            }
            else
            {
                if(isreplayable)
                    if (!music.isPlaying)
                    {
                        Restart();
                    }
            }
        }
        if(musicplayerbehaviour == MusicPlayerBehaviour.justone)
        {
            if (!music.isPlaying)
            {
                Restart();
                if (isreplayable)
                {
                    PlayMusic();
                }
                else
                {
                    StopMusic();
                }
            }
        }
        if(musicplayerbehaviour == MusicPlayerBehaviour.random)
        {
            if(!music.isPlaying)
            {
                currentmusicclip = Random.Range(0, endatmusicclip);
                PlayMusic();
            }
        }
    }
    public void PlayMusic()
    {
        if (gameObject.activeInHierarchy && !music.isPlaying)
        {
            music.Play();
        }
    }
    public void StopMusic()
    {
        if(gameObject.activeInHierarchy && music.isPlaying)
        {
            music.Stop();
        }
    }
    public void LimitMusicLengh(int limit)
    {
        endatmusicclip = limit;
    }

    #endregion

    #endregion

}
