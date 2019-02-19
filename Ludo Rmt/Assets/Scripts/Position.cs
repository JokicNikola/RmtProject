using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int index;
    public int koraci;
    public Vector3 onStart;
    public bool _out;

   void Start()
    {
        
        _out = false;
       onStart = transform.position;
        Debug.Log(onStart);
    }
}
