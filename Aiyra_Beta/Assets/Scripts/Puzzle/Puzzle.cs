using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class Puzzle : MonoBehaviour {

    #region Keys
    public string puzzlestatussavekey;
    #endregion

    #region attributes
    public GameController gamecontroller;
    public Scene scene;
    public RectTransform puzzletransform;
    public AudioSource puzzlesound;
    public Image puzzleimage;
    public BoxCollider2D puzzlecollider;

    public bool active;
    public bool resolved;
    #endregion

    #region Methods

    #region Enable And Disable Methods

    void OnEnable()
    {
        //if(scene != null)
        //scene.scenestate = Scene.state.puzzle;

        //gamecontroller.canprogress = false;
    }
    void OnDisable()
    {
        //if(scene != null)
        //scene.scenestate = Scene.state.interaction;

        //gamecontroller.canprogress = true;
    }

    #endregion

    #region Awake And Start Methods
    //Method is run only one and when the gameobject associed with the script awake
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

    #region MouseOver And Interaction Methods

    //Method that check if the mouse is over the trigger of the puzzle and if the player has interacted with it
    void OnMouseOver()
    {
        if (Input.GetButtonDown("Confirm"))
        {
            if(!active)
            {
                Debug.Log("Player start puzzle" + gameObject.name);
                gamecontroller.canprogress = false;
                active = true;
                RewardPlayerWithObject();
            }
            /*
            else
            {
                Debug.Log("Player end puzzle" + gameObject.name);
                gamecontroller.canprogress = true;
                RewardPlayerWithObject();
                ExitPuzzle();
            }*/
        }
    }

    #endregion

    #region Puzzle Fundamental Methods

    #region Update CG Status And Values

    public void UploadPuzzleSaveKey()
    {
        puzzlestatussavekey = "PUZZLE" + gameObject + "SAVEKEY";
    }

    #endregion

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

    void RewardPlayerWithObject()
    {
        if (GetComponent<PuzzlePicture>() != null)
        {
            if (gamecontroller.player.inventary.Length > 0)
            {
                string[] temp = gamecontroller.player.inventary;
                gamecontroller.player.inventary = new string[gamecontroller.player.inventary.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    gamecontroller.player.inventary[i] = temp[i];
                }
                gamecontroller.player.inventary[gamecontroller.player.inventary.Length] = gameObject.name;
            }
            else
            {
                gamecontroller.player.inventary = new string[1] { gameObject.name };
            }
        }
    }

    #endregion

    #region Puzzle End And Exit Methods

    void ExitPuzzle()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Save And Load Methods

    #region Save Method

    public void SavePuzzleStatus()
    {
        PlayerPrefs.SetString(puzzlestatussavekey, resolved.ToString());
    }

    #endregion

    #region Load Method

    public void LoadPuzzleStatus()
    {
        if (PlayerPrefs.GetString(puzzlestatussavekey) == "TRUE")
            resolved = true;
        else
        {
            resolved = false;
        }
    }

    #endregion

    #endregion

    #endregion

    #endregion
}
