using UnityEngine;
using System.Collections;

public class PuzzlePicture : MonoBehaviour
{

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

    public void UpdatePicuteStatus()
    {
        if (puzzle != null)
        {
            if (!puzzle.resolved && puzzle.active)
            {
                if (Input.GetKey(KeyCode.A))
                    transform.Rotate(0, 0, 1);
                if (Input.GetKey(KeyCode.D))
                    transform.Rotate(0, 0, -1);
                if (Input.GetKey(KeyCode.LeftArrow))
                    transform.Translate(-1, 0, 0);
                if (Input.GetKey(KeyCode.RightArrow))
                    transform.Translate(1, 0, 0);
                
                if (puzzle.gamecontroller.pausemenu.gameObject.activeInHierarchy)
                {
                    puzzle.gamecontroller.effectscamerablurfilter.blurAmount = 0;
                }
                else
                {
                    if (transform.rotation.z > 0)
                    {
                        puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.rotation.z;
                    }
                    else
                    {
                        puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.rotation.z * -1;
                    }
                    if (transform.rotation.z < 3 && transform.rotation.z > 357)
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
    }
    #endregion

    #endregion
}
