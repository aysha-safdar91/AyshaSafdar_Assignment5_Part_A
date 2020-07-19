using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Applies a rotation to the 'Collectibe' whic is a cube object in 3d i. all the three axis.
*/
public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        /* pass values for all the three angles in degrees. Delta time This property provides the time between the current 
           and previous frame, helps to continue. 
        */
    }
}
