using UnityEngine;
using System.Collections;

public class PuzzlePortalFall : MonoBehaviour {

    #region Attributes

    public Puzzle puzzle;

    #endregion

    #region Methods

    #region Enable and Disable

    void OnEnable()
    {
        Debug.Log("PicturePuzzle " + gameObject.name + " Active");
    }
    void OnDisable()
    {
        Debug.Log("PicturePuzzle " + gameObject.name + " Desactive");
    }

    #endregion

    #region Awake and Start
    //Method is run only one and when the gameobject associed with the script awake
    void Awake()
    {
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
    }

    #endregion

    #region Fundamental Methods

    public void UpdateStunStatus()
    {
        if (puzzle != null)
        {
            if (!puzzle.resolved && puzzle.active)
            {
                if (Input.GetKey(KeyCode.A))
                    transform.Translate(-1, 0, 0);
                if (Input.GetKey(KeyCode.D))
                    transform.Translate(1, 0, 0);

                if (transform.position.x > 1)
                {
                    puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.position.x;
                }
                else
                {
                    puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.position.x * -1;
                }

                if(transform.position.x < 15 && transform.position.x > -15)
                {
                    puzzle.resolved = true;
                }
            }
        }
        else
        {
            Debug.Log("No puzzle script associated with picture puzzle");
        }
    }
    #endregion
    //&& (transform.rotation.z < 3 && transform.rotation.z > 357)
    #endregion
}
