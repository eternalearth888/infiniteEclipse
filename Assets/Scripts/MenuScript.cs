using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
	{
		SceneManager.LoadScene("mountainSection0");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
