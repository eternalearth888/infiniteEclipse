using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


// Original Template/Design started here
/*
 * Projectile reflection demonstration in Unity 3D
 * 
 * Demonstrates a projectile reflecting in 3D space a variable number of times.
 * Reflections are calculated using Raycast's and Vector3.Reflect
 * 
 * Developed on World of Zero: https://youtu.be/GttdLYKEJAM
 */

public class SunbeamLightEmitter : MonoBehaviour
{
    public int maxReflectionCount = 50;
    public float maxStepDistance = 20000;

	// Game world line/light
    private LineRenderer _lineRenderer;
    private List<Vector3> _lineVertices;
    private Ray _ray;
    private RaycastHit _rayCastHit;


    //lists of things
    public List<GameObject> _activeStatues;
    public GameObject _parentLightEmitter; // sunstone
    public bool _startSwitchOn;

    // called before the first frame update
    void Start()
    {
    	// tell the game that our light exists
    	_lineRenderer = GetComponent<LineRenderer>();
    	_lineVertices = new List<Vector3>(maxReflectionCount + 1);
    	_activeStatues = new List<GameObject>
    	{
    		this.gameObject
    	};
    }


    // update is called once per frame
    void Update()
    {
    	//just draw the light for now
    	DrawSunbeam();
    }


    // only shows in Scene not in game. this will allow you to see what is happening so you 
    // can debug and figure out what you wanna do
    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    // draw the light for gizmos
    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            return;
        }

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }

    // update drawing/rendering the line for the light in game
    void DrawSunbeam()
    {
    	//clear the lists
    	_activeStatues.Clear();
    	// delete any lines that exist
    	_lineVertices.Clear();
    	// re-draw/re-add lines to the map
    	_lineVertices.Add(this.transform.position);
    	_ray = new Ray(_lineVertices[0], this.transform.forward);

    	if(Physics.Raycast(_ray, out _rayCastHit, maxStepDistance))
    	{
    		ReflectLineRenderer(_lineVertices[0], this.transform.forward, maxReflectionCount);
    	} 
    	else 
    	{
    		_lineVertices.Add(this.transform.position + (this.transform.forward * maxStepDistance));
    	}

    	_lineRenderer.positionCount = _lineVertices.Count;
    	_lineRenderer.SetPositions(_lineVertices.ToArray());

    }

    // actually draw the Line Renderer
    void ReflectLineRenderer(Vector3 position, Vector3 direction, int reflectionsLeft)
    {
    	if(reflectionsLeft == 0)
    	{
    		return;
    	}

    	Ray ray = new Ray(position, direction);
    	RaycastHit hit;

    	if(Physics.Raycast(ray, out hit, maxStepDistance))
    	{
    		GameObject go = hit.collider.gameObject;

    		switch(go.tag)
    		{
    			case "isReflective":
    				direction = (hit.collider.transform.position - _lineVertices[_lineVertices.Count-1]).normalized; //Direction fixed to mirror center
	    			direction = Vector3.Reflect(direction, hit.normal);
	    			//direct hit anywhere on the mirror
	    			position = hit.point;
	    			_lineVertices.Add(position);
	    			// reflect line again
	    			ReflectLineRenderer(position, direction, reflectionsLeft - 1);
	    			break;
	    		case "isSwitchisReflective": 
	    			direction = (hit.collider.transform.position - _lineVertices[_lineVertices.Count-1]).normalized; //Direction fixed to mirror center
	    			ActivateSwitch(hit.collider.gameObject);
	    			direction = Vector3.Reflect(direction, hit.normal);
	    			position = hit.point;
	    			_lineVertices.Add(position);
					ReflectLineRenderer(hit.point, direction, reflectionsLeft - 1);
					if (hit.collider.gameObject.GetComponent<LightSwitch>().startSwitch) return;
                    break;
                case "isSwitchisNOTReflective": // make it so line goes through this
	    			ActivateSwitch(hit.collider.gameObject);
	    			position = hit.point;
	    			_lineVertices.Add(position);
					if (hit.collider.gameObject.GetComponent<LightSwitch>().startSwitch) return;
                    break;
          		default: // tag: "notReflective" : trees, walls, main character, etc.
	    			position = hit.point;
	    			_lineVertices.Add(position);
	    			return;
    		}
    	}
    	else
    	{
    		position += direction * maxStepDistance;
    		_lineVertices.Add(position);
    		return;
    	}
    }

    void ActivateSwitch(GameObject go){
    		LightSwitch lightSwitch;
    		lightSwitch = go.GetComponent<LightSwitch>();
    		lightSwitch.Activate();
    		lightSwitch.lightEmitter = this.gameObject;

    		if (!_activeStatues.Contains(go))
    		{
    			_activeStatues.Add(go);
    		}
    }
}