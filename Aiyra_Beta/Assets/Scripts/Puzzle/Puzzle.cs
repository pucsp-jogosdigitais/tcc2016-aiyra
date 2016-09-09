using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class Puzzle : MonoBehaviour {

    #region attributes
    public GameController gamecontroller;
    public Scene scene;
    public RectTransform puzzletransform;
    public AudioSource puzzlesound;
    public Image puzzleimage;
    public BoxCollider2D puzzlecollider;

    public bool resolved;
    #endregion

    #region Methods
    void OnEnable()
    {
        if(scene != null)
        scene.scenestate = Scene.state.puzzle;

        gamecontroller.canprogress = false;
    }
    void OnDisable()
    {
        if(scene != null)
        scene.scenestate = Scene.state.interaction;

        gamecontroller.canprogress = true;
    }
    void Awake()
    {
        if (gamecontroller == null)
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        if (puzzlesound == null)
            puzzlesound = GetComponent<AudioSource>();
        if (puzzleimage == null)
            puzzleimage = GetComponent<Image>();
        if (puzzlecollider == null)
            puzzlecollider = GetComponent<BoxCollider2D>();
        if (puzzletransform == null)
            puzzletransform = GetComponent<RectTransform>();

        puzzlecollider.size = new Vector2(puzzletransform.sizeDelta.x, puzzletransform.sizeDelta.y);
    }
    void Update()
    {
        resolved = CheckPuzzleResolution();
        ManipulatePicture();
    }
    void OnMouseOver()
    {
        if (Input.GetButtonDown("Confirm"))
        {
            if (resolved)
            { 
                RewardPlayer();
                ExitPuzzle();
            }
        }
    }
    bool CheckPuzzleResolution()
    {
        if (gameObject.name == "PuzzlePhoto")
            if (puzzletransform.rotation.z >= 0)
                return true;

        return false;
    }
    void ManipulatePicture()
    {
        Component blur = GameObject.Find("Main Camera").GetComponent("BlurOptimized");

        if (scene.scenestate == Scene.state.puzzle)
        {
            if(gameObject.name == "PuzzlePhoto")
                if (!resolved)
                {
                    if(Input.GetButton("Horizontal"))
                    {
                        puzzletransform.Rotate(0, 0, Input.GetAxis("Horizontal"));
                    }
                }
        }
        
    }
    void RewardPlayer()
    {
        /*
        if (gameObject.name == "PuzzlePhoto")
        {
            GameObject.Find("MainRoomPicture").GetComponent<Object>().isavailable = false;
            Debug.Log("Voc� Constou a foto" + gameObject.name);
        }
        */
    }
    void ExitPuzzle()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
