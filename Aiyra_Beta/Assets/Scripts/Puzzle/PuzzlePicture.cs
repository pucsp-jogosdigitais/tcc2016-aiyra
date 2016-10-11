using UnityEngine;
using System.Collections;

public class PuzzlePicture : MonoBehaviour {

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
            if (!puzzle.resolved)
            {
                if (Input.GetKey(KeyCode.A))
                    transform.Rotate(0, 0, 1);
                if (Input.GetKey(KeyCode.D))
                    transform.Rotate(0, 0, -1);
                if (Input.GetKey(KeyCode.LeftArrow))
                    transform.Translate(-1, 0, 0);
                if (Input.GetKey(KeyCode.RightArrow))
                    transform.Translate(1, 0, 0);

                if (transform.rotation.z < 3 && transform.rotation.z > 357)
                    {
                        puzzle.gamecontroller.effectscamerablurfilter.blurAmount = 0;
                        puzzle.resolved = true;
                    }
                    else
                    {
                        puzzle.gamecontroller.effectscamerablurfilter.blurAmount = transform.rotation.z / 10;
                    }
            }
        }
        else
        {
            Debug.Log("No puzzle script associated with picture puzzle");
        }

        #endregion

        #endregion
    }

}
