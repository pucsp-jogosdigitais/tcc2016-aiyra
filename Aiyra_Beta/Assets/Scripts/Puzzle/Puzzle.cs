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

    #region Enable And Disable Methods

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

    #endregion

    #region Awake And Start Methods

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

    #endregion

    #region Updates Methods

    void Update()
    {
        resolved = CheckPuzzleResolution();

        //ManipulatePicture();
    }

    #endregion

    #region MouseOver And Interaction Methods

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

    #endregion

    #region Puzzle Fundamental Methods

    #region Check Resolution

    bool CheckPuzzleResolution()
    {
        if (gameObject.name == "PuzzlePhoto")
            if (puzzletransform.rotation.z >= 0)
                return true;

        return false;
    }

    #endregion

    #region Puzzle Resolution Methods

    /*
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
    */

    #endregion

    #region Puzzle Reward Methods

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

    #endregion

    #region Puzzle End And Exit Methods

    void ExitPuzzle()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #endregion

    #endregion
}
