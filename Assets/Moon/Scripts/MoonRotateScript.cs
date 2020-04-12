using UnityEngine;
using System;


public class MoonRotateScript:MonoBehaviour
{
    public int speed =1 ;
    public void Update() {
      
        transform.Rotate(Vector3.up * Time.deltaTime*speed);
    }
}