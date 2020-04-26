using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLevelTrigger : MonoBehaviour
{
	public string nextScene;

	void onTriggerEnter(Collider Other)  {
    	// Load the next scene in the build index
    	SceneManager.LoadScene(nextScene);
    }
}
