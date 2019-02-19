using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public int index;
    public int index1;
    public Vector3 onStart;
    public bool _out;

   void Start()
    {
        index = 13;
        index1 = 13;
        _out = false;
       onStart = transform.position;
        Debug.Log(onStart);
    }
}
