using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;



public class PositionController : MonoBehaviour
{
    public GameObject theCube;
    public GameObject theSphere;
    public int thestringlength;
    public float maxPos= 0f;
    public float minPos = 30f;
    

    private string createdRandomString;
    private static int palindromeLength;
    private Vector3 theNewPos;
    
    /*
        SPAWN FUNCTION
        This function generate collectibles (cubes) at random positions in the maze and their corresponding text element with clamped through sphere object.
        The string of 9 characters generated having alphanumerics x, m and 8 appeared in clamped text. 
     
     */
    
    public void spawn()
    {
        List<int> palindromeIndexes = new List<int>();              //  List of index values corresponds to palindrom strings
        int palindromeIndex;                                        //  palindron index variable

        Text randomString;                                          //  variable to store random strings generated 


        
        /* Algorithm to make random palindromic strings   */
        
        for (int i = 0; i < palindromeLength; i++)
        {
            palindromeIndex = Random.Range(0, 9);                    // 10 strings to be generated
            if (!palindromeIndexes.Contains(palindromeIndex) || palindromeIndexes.Count == 0)
            {
                palindromeIndexes.Add(palindromeIndex);
            }
            else
            {
                palindromeLength = palindromeLength + 1;
            }
        }

        int cubeNumber = 0;
        while (cubeNumber<10)                                                       //  for 10 cubes/collectibles
        {
            createdRandomString = "";
            float theXPosition = Random.Range(minPos, maxPos);                      //  To seek random positions on x-axis
            float theZPosition = Random.Range(minPos, maxPos);                      //  To seek random positions on z-axis
            theNewPos = new Vector3 (theXPosition,0.5f,theZPosition);               //  Store position in this variable. Notice y-axis will remain constant in 3D
            if (Physics.CheckSphere(theNewPos, 0.36f))                              //  It at this location there is player then do nothing
            {
                continue;
            }
            else
            {                                                                       //  Otherwise
                GameObject sphere = Instantiate(theSphere);                         //  generate an sphere game object that corresponds to clamp text for random string
                GameObject cube = Instantiate(theCube);                             //  generate an cube game object which is collectible
                sphere.name = "Sphere" + cubeNumber;                                //  append its index
                cube.name = "Cube" + cubeNumber;                                    //  append its index
                sphere.transform.position = new Vector3(theXPosition,1.1f,theZPosition);    //  set label position
                cube.transform.position = theNewPos;                                        //  set collectible cube position
                
                //  create random string of length 9-15 characters (x, m and 8) and display in the UI Text object

                randomString = GameObject.Find("Sphere"+cubeNumber+"/Canvas/Text").GetComponent<Text>();    
                string[] characters = new string[] { "x", "a", "4"};
                thestringlength = Random.Range(9, 15);                          //  strings having random length between 9 to 15 characters 
                if (palindromeIndexes.Contains(cubeNumber))
                {
                    for (int j = 0; j < thestringlength/2; j++)                 //  to generate some palindromic strings
                    {
                        createdRandomString = createdRandomString + characters[Random.Range(0, characters.Length)]; 
                    }
                    createdRandomString = createdRandomString + new string(createdRandomString.Reverse().ToArray());
                }
                else
                {
                    for (int j = 0; j < thestringlength; j++) 
                    {
                        createdRandomString = createdRandomString + characters[Random.Range(0, characters.Length)]; 
                    }   
                }

                randomString.text = createdRandomString;
                cubeNumber++;
            }
        }
    }

    public static int _totalPalindromes;
    public static int totalPalindromes
    {
        get
        {
            return _totalPalindromes;
        }
        set
        {
            _totalPalindromes = value;
        }

    }
    
    
    /*  START FUNCTION :  
        initially to generate 10 collectibles along with corresponding random strings, it is fixed that there should be minimum 3 palindroms in the strings generated 
        by the spa*/
    
    void Start()
    {
        palindromeLength = Random.Range(3, 10);                 //  generate random palindrom length between 3 to 10
        PositionController.totalPalindromes = palindromeLength; //  set the number of palindrom accroding to the length generated
        spawn();                                                //  call spawn function to generate cubes and corresponding strings clamped through sphere and text.
    }
}