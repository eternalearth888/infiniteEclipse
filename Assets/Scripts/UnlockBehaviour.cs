using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBehaviour : MonoBehaviour
{
    public GameObject lockedLinkObject;

    private LightSwitch _lightSwitch;
    public bool isOpen = false;


    void Start()
    {
    	_lightSwitch = lockedLinkObject.GetComponent<LightSwitch>();
    }

    void Update()
    {
    	if (!isOpen && _lightSwitch.active)
    	{
    		Open();
    	}
    	else if (isOpen &&  !_lightSwitch.active)
    	{
    		Close();
    	}
    }

    private void Open()
    {
    	isOpen = true;
    	GetComponent<MeshRenderer>().enabled = false;
    	GetComponent<MeshCollider>().enabled = false;

    }

    private void Close()
    {
    	isOpen = false;
 	   	GetComponent<MeshRenderer>().enabled = true;
    	GetComponent<MeshCollider>().enabled = true;

    }
}
