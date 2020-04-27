using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPauseUIScript : MonoBehaviour
{
    public GameObject uiObject;
    
    public bool GameIsPaused = false;

    void Update()
    {

    	// player hits escape is not currently paused
   		if(Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused) 
   		{
   			Debug.Log("Pause");
   			Pause();
   		}
		else  if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused) // if the player is paused and they  hit escape, unpause the game
	    {
	    	Debug.Log("Resume");
	    	Resume();
	    } 
		else if (GameIsPaused && Input.GetKeyDown(KeyCode.Q))    // if the player is paused and they press the Q button, quit the game
	    {
	    	Debug.Log("Quit");
	    	Application.Quit();
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
