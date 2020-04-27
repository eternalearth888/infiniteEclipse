using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTextDialogue : MonoBehaviour
{
    public GameObject uiObject;

    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "player")
        {
        	//Debug.Log("OnTriggerEnter Activated");
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    // Destroy the gameobject so they don't walk over it multiple times
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        Destroy(uiObject);
        Destroy(gameObject);
    }
}
