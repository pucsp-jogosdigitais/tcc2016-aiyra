using UnityEngine;
using System.Collections;

public class CrossHeir : MonoBehaviour {

    #region attributes
    public GameObject crossheir;
    #endregion

    #region methods
    void Update () {
        FollowMouse();
	}
    void FollowMouse()
    {
        Vector2 pos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        crossheir.transform.position = pos;
    }
    #endregion
}
