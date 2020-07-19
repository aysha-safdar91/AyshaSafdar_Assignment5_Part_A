using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  This Method is used for UI Text placed static when the camera move with the ball. It Transforms position from worldspace to screen space. 
    Screenspace is defined in pixels with dimensions as left-bottom (0,0) the right-top (pixelWidth, pixelHeight).
*/
public class ClampName : MonoBehaviour
{
    public Text nameLabel;      //  to get UI text value
    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;
    }
}
