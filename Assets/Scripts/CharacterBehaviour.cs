using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CharacterBehaviour : MonoBehaviour
{

	public FirstPersonController fpc;

	// make public for editor/debugging purposes
	public GameObject editStatue; // the current statue that is being manipulated
	public Quaternion previousStatueRotation; // the current statues previous state of rotation

	//singleton logic for statuelightbehaviour
	public static CharacterBehaviour characterSingleton;

	//mouse related stuff
	public GameObject mouseOverObject;

	private void Awake(){
		characterSingleton = this;
	}

    // Start is called before the first frame update
    void Start()
    {
    	fpc = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
    	 if (fpc.isEditingStatue) //while the player is editing/manipulating the statue
        {
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 currentRotation = editStatue.transform.rotation.eulerAngles;
            Vector3 newRotation = currentRotation;
            if (editStatue.tag == "isReflective" || editStatue.tag == "isSwitchisReflective")
           // if (editStatue.tag == "isReflective" || editStatue.tag == "isSwitch" || editStatue.tag == "isSwitchisReflective") //edit/debug all mirrors
            {
                newRotation = new Vector3(currentRotation.x + vertical, currentRotation.y + horizontal, currentRotation.z);
            }
            
            editStatue.transform.rotation = Quaternion.Euler(newRotation);
            
            if (Input.GetMouseButtonDown(0)) //save the edits on a left click
            {
                fpc.isEditingStatue = false;
                editStatue = null;
            }
            else if (Input.GetMouseButtonDown(1)) //cancel  on a right click
            {
                fpc.isEditingStatue = false;
                editStatue.transform.rotation = previousStatueRotation;
                editStatue = null;
            }
        }
        else
        {
		  if (Input.GetMouseButtonDown(0))
	      {
	        if (mouseOverObject != null)
	        {
                // add tags to me to make them moveable
	            switch (mouseOverObject.tag)
	            {
	                case "isReflective":
	                case "isSwitchisReflective":    
                        fpc.isEditingStatue = true;
	                    editStatue = mouseOverObject;
	                    previousStatueRotation = editStatue.transform.rotation;
	                    break;
	            }
	        }
	      }
	    }
    }
}
