using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMoonStatue : MonoBehaviour
{
     public LightSwitch[] lockedLinkedSwitches;

    public bool isOpen = false;


    void Update()
    {
    	// if statements to help with processing/time
    	if (!isOpen && checkLinkedSwitches())
    	{
    		Open();
    	}
    	else if (isOpen && !checkLinkedSwitches())
    	{
    		Close();
    	}
    }

    bool checkLinkedSwitches()
    {
    	foreach(LightSwitch ls in lockedLinkedSwitches)
    	{
    		if(!ls.active)
    		{
    			return false;
    		}
    	}
    	return true;
    }

    private void Open()
    {
    	isOpen = true;
    	// change this to particle system stuff when you get to the waterfall
    	GetComponent<Renderer>().enabled = true;
    	GetComponent<Collider>().enabled = true;
    	GetComponent<LineRenderer>().enabled = true;

    }

    private void Close()
    {
    	isOpen = false;
    	// change this to particle system stuff when you get to the waterfall
 	   	GetComponent<Renderer>().enabled = false;
    	GetComponent<Collider>().enabled = false;
    	GetComponent<LineRenderer>().enabled = false;
    }
}
