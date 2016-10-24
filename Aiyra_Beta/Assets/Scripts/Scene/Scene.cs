using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SceneBehaviour))]
public class Scene : MonoBehaviour {

    #region Attributes

    public enum state { dialog, interaction, puzzle };
    public state scenestate;
    public MovieTexture[] livebackground;
    public Sprite[] backgrounds;
    public AudioClip[] musics;
    public TextAsset[] dialogs;
    public TextAsset[] answers;
    public GameObject[] objects;
    public Puzzle[] puzzles;

    public string scenename;
    public int sceneid;

    #endregion
}
