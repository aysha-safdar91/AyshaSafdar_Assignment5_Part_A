using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For setting camera which follow / focus the game object. By getting the position of that location and the offset value and assigning the transform.position.

public class CameraController : MonoBehaviour
{
    public GameObject player;   //  to focus on game object player

    private Vector3 offset;

    // Start is called before the first frame update
    void Start() 
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update() 
    {
        transform.position = offset + player.transform.position;
    }
}
