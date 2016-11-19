using UnityEngine;
using System.Collections;
using System;

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
                if (Input.GetKey(KeyCode.W))
                {
                    if (transform.rotation.eulerAngles.z >= 180)
                        transform.Rotate(0, 0, 1f);
                    else { transform.Rotate(0, 0, -1f); }
                }
                if(Input.GetKey(KeyCode.S))
                {
                    if (transform.rotation.eulerAngles.z < 180)
                        transform.Rotate(0, 0, 1f);
                    else { transform.Rotate(0, 0, -1f); }
                }
                if (Input.GetKey(KeyCode.A))
                    transform.Rotate(0, 0, 1f);
                if (Input.GetKey(KeyCode.D))
                    transform.Rotate(0, 0, -1f);

                puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.rotation.z;

                if (transform.rotation.eulerAngles.z >= 350 || transform.rotation.eulerAngles.z <= 10)
                    puzzle.resolved = true;
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
