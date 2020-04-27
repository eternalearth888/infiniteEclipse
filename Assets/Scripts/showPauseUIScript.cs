using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPauseUIScript : MonoBehaviour
{
    public GameObject uiObject;
    
    public static bool GameIsPaused = false;

    void Update()
    {

    	// player hits escape is not currently paused
   		if(Input.GetKeyDown(KeyCode.Escape)) 
	    {     
	    	Debug.Log("Player hit escape");

	    	// if the player is paused and they  hit escape, unpause the game
		    if (GameIsPaused)
		    {
		    	Resume();
		    } 
		    else if (GameIsPaused && Input.GetKeyDown(KeyCode.Q) == true) //keycodes need verbose booleans
		    {
		    	// if the player is paused and they press the Q button, quit the game
		    	Debug.Log("Quit");
		    	Application.Quit();
		    }
		    else
		    {
		    	// if they player is playing and they hit escape, pause the game
		    	Pause();
		    }
	    }
    }   

    void Resume()
    {
    	Debug.Log("Resume Function");
        uiObject.SetActive(false);
        //unfreeze time when paused
        Time.timeScale = 1f;
		GameIsPaused = false;
    }

    void Pause()
    {
    	Debug.Log("Pause Function");
    	// start the game with the pause screen
    	// we start the game paused for instructions
        uiObject.SetActive(true);
        //freeze time when paused
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
