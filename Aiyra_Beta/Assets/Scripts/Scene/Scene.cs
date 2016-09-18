using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    public enum state { dialog, interaction, puzzle };
    public state scenestate;
    public Sprite[] backgrounds;
    public AudioClip[] musics;
    public TextAsset[] dialogs;
    public TextAsset[] answers;
    public GameObject[] objects;
    public Puzzle[] puzzles;

    public string scenename;
    public int sceneid;
}
