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
    void OnEnable()
    {
        Debug.Log("Music Player active");
    }
    void OnDisable()
    {
        Debug.Log("Music Player desactive");
    }
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
                StopMusic();
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

}
