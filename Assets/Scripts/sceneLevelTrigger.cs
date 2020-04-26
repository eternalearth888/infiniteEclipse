using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLevelTrigger : MonoBehaviour
{
	// current scene
	public Scene currentScene;
	//next scene
	public int currentSceneIndex;

	void OnTriggerEnter(Collider player)  {
		Debug.Log("Trigger Activated");
    	// Load the next scene in the build index
    	if (player.gameObject.tag == "player")
        {
        	currentScene = SceneManager.GetActiveScene();
        	currentSceneIndex =  currentScene.buildIndex;

    		SceneManager.LoadScene(currentSceneIndex+1);
        }
    }
}
