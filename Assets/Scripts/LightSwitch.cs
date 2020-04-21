using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

	// all switches start inactive
	public bool active = false;
	public bool startSwitch = false;

	public GameObject lightEmitter;
	public GameObject startLight;

	public Color inactiveColor;
	public Color activeColor;

	private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
   		if(startSwitch)
   		{
   			startLight = GameObject.Find("SunBeamStone");
   		}

   		_renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	// if no light is hitting the switch, deactive it
        if(lightEmitter != null && (!lightEmitter.GetComponent<SunbeamLightEmitter>()._activeStatues.Contains(this.gameObject) || (!lightEmitter.activeInHierarchy)))
        {
			Deactivate();        	
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    	GameObject go = collision.gameObject;
    }

    public void Activate()
    {
    	active = true;

    	if (startSwitch && startLight) {
    		startLight.GetComponent<SunbeamLightEmitter>()._startSwitchOn = true;
    	}

    	_renderer.material.SetColor("_Color", activeColor);
    }

    public void Deactivate()
    {
    	active = false;
    	lightEmitter = null;
    	_renderer.material.SetColor("_Color", inactiveColor);
    }

}
