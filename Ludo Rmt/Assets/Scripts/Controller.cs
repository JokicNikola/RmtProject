using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool blueTurn;
    public bool yellowTurn;
    public bool redTurn;
    public bool greenTurn;

    public int outBlue;
    public int outYellow;
    public int outRed;
    public int outGreen;

    // Start is called before the first frame update
    void Start()
    {
        blueTurn = true;
        yellowTurn = false;
        redTurn = false;
        greenTurn = false;

 

        outBlue = 0;
        outYellow = 0;
        outRed = 0;
        outGreen = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
