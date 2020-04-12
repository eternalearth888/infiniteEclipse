using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLightBehaviour : MonoBehaviour
{
    
    // change the character behaviour when mousing over a statue
    // so that the character interaction is with the statue
    // and not the first person controller with walking/looking around
    private void OnMouseOver()
    {
        if (CharacterBehaviour.characterSingleton.mouseOverObject != gameObject)
        {
            CharacterBehaviour.characterSingleton.mouseOverObject = gameObject;
            
        }
    }

    private void OnMouseExit()
    {
        if (CharacterBehaviour.characterSingleton.mouseOverObject == gameObject)
        {
            CharacterBehaviour.characterSingleton.mouseOverObject = null;
        }

    }
}
