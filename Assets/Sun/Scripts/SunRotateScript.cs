using UnityEngine;
using System;


public class SunRotateScript:MonoBehaviour
{
    public int speed =1 ;
    public void Update() {
      
        transform.Rotate(Vector3.up * Time.deltaTime*speed);
    }
}