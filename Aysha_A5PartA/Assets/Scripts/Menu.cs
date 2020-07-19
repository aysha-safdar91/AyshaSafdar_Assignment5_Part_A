using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // function click for the button
    public void button_click()
    {
        SceneManager.LoadScene(1);

    }
}
