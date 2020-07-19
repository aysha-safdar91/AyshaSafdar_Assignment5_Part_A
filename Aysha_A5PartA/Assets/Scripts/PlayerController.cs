using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : PositionController
{
    /*  In this Player Movement class, first we code for the movement of player game object with arrow keys. Then it pick up collectible cubes 
        with collider component once it hit the cube, however, for picking or destroying a cube there is a validation rule, i.e. it only destroy it
        if its corresponding string is a 'PALINDROM' otherwise it bypass it. Once all the palindromic string collectibles are picked/ destroyed, a message 
        appears that shows how many palindromic strings were generated during instantiation of cube objects (collectibles). Note that such strings ranges from 
        3 to 10 alphanumeric strings of length 9-15.
    */

    // Variables declaration

    public int speed;           //  to set movement of player
    public GameObject sphere;   //  player
    public AudioSource ticksource; //Audio

    private Rigidbody _rigidbody;
    private string theCubeIndex;
    private int palindromeCount = 0;
    private static int totalPalindromes;

    /*  In start method Component is actually an instance of a class so the first step is to get a reference to the Component instance you want to work 
        with. 
    */

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();  //assign the Component object to a variable to the values of its properties like in Inspector done
        ticksource = GetComponent<AudioSource>(); //Adding  audio
    }

    
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        /*  Returns the value of the virtual axis identified by Horizontal to local variable moveHorizontal. Similarly,Returns the value of the virtual axis 
        identified by Vertical to local variable moveVertical*/
        Vector3 movement = new Vector3(moveHorizontal , 0.2f , moveVertical);
        /*  Calculate a position between the points specified by current and target, moving no farther than the distance specified by move vertical value.
             The force applied used for controling the speed of ball as Force can only be applied to an active Rigidbody.
             If a GameObject is inactive, AddForce has no effect. Also, the Rigidbody cannot be kinematic.
         */
        _rigidbody.AddForce(movement * speed);      
    }

  
    /*   As OnTriggerEnter method is called the collider component of game object is passed as paramter whose tag isvarified whether it is 'Collectible',
         notice that the tags of cube and sphere prefabs are assigned as 'Collectibles' using Inspecter pallate then match regex 
    */
    void OnTriggerEnter(Collider other)      
    {
        Text checkIsPalindrome, collectedPalindrome;            //  to manipulate palindrom check and keep the list
        if (other.gameObject.CompareTag ("Collectibles"))       //  compare tag
        {
            /*  Whether the regular expression finds a match in the input string (already defined in PositionController class). Once matches its value captured 
                and sphere variable contain the finding gameobject using parameter thecubeindex. In fact, checkIsPalindrome check the string that generated 
                having pallindrome or not using UI text. Using for loop for decrement that reverse the string of pallindrome and check the condition which
                randomly genearted pallindrome if the sequence according the pallindrome the set active variable change the false otherwise not. 
                 if the palindromeCount is equal to the totalPalindromes then using UItext display the message the collected pallindrome. 
            */
            theCubeIndex = Regex.Match(other.gameObject.name, @"\d+").Value;    //  capture match text of collectible
            sphere = GameObject.Find("Sphere"+Int32.Parse(theCubeIndex));       //  transform it to corresponding sphere with clamped text
            checkIsPalindrome = GameObject.Find("Sphere"+Int32.Parse(theCubeIndex)+"/Canvas/Text").GetComponent<Text>();    //  to check whether it is palindrom
            
            string revs = "";                                   //  to store revere of the string
            for (int i = checkIsPalindrome.text.Length - 1; i >= 0; i--)  
            {
                revs += checkIsPalindrome.text[i].ToString();
            }
            if (revs == checkIsPalindrome.text)                 // if reverse matches the actual text           
            {
                ticksource.Play(); // Play when collectible is palindrome
                other.gameObject.SetActive(false);              //  destroy collectible
                sphere.gameObject.SetActive(false);             //  destroy clamping sphere

                palindromeCount += 1;                           //  count this collectible
                if (palindromeCount == PositionController.totalPalindromes)  //   if count approaches to the palindrom length. that means there will beno further destruction
                {
                    
                    collectedPalindrome = GameObject.Find("Canvas/Text").GetComponent<Text>();
                    collectedPalindrome.text = "You Won!!! You collected all palindromes. Total No. of collected palindromes= " + palindromeCount + " Palindromes";     //  show how many collectibles picked.
                }
            }
            
        }
    }
}
