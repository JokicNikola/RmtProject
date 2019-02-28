using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool blueTurn;
    public bool yellowTurn;
    public bool redTurn;
    public bool greenTurn;

    private DiceYellow yellow;
    private DiceRed red;
    private DiceGreen green;
    private Dice blue;

    public int endBlue;
    public int endGreen;
    public int endYellow;
    public int endRed;


    public int outBlue;
    public int outYellow;
    public int outRed;
    public int outGreen;
    private int rand;


    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 4);
        blueTurn = false;
        yellowTurn = false;
        redTurn = false;
        greenTurn = false;

        yellow = GameObject.Find("Side6 (1)").GetComponent<DiceYellow>();
        red = GameObject.Find("Side6 (3)").GetComponent<DiceRed>();
        blue = GameObject.Find("Side6").GetComponent<Dice>();
        green = GameObject.Find("Side6 (2)").GetComponent<DiceGreen>();

        endBlue=0;
        endGreen=0;
        endYellow=0;
        endRed=0;

        outBlue = 0;
        outYellow = 0;
        outRed = 0;
        outGreen = 0;



        if (rand == 0)
        {
            blueTurn = true;
            blue.click = false;
            UnityEngine.Debug.Log("Plavi igra");
        }
        if (rand ==1)
        {
            yellowTurn = true;
            yellow.click = false;
            UnityEngine.Debug.Log("Zuti igra");
        }
        if (rand == 2)
        {
            redTurn = true;
            red.click = false;
            UnityEngine.Debug.Log("Crveni igra");
        }
        if (rand == 3)
        {
            greenTurn = true;
            green.click = false;
            UnityEngine.Debug.Log("Zeleni igra");
        }  
        
    }

    

    // Update is called once per frame
    void Update()
    {

        if (endBlue == 4) {
            UnityEngine.Debug.Log("Plavi je pobedio!");
            
        }

        if (endGreen == 4) {
            UnityEngine.Debug.Log("Zeleni je pobedio!");
            
        }

        if (endRed == 4) {
            UnityEngine.Debug.Log("Crveni je pobedio!");
            
        }

        if (endYellow == 4) {
            UnityEngine.Debug.Log("Žuti je pobedio!");
            
        }
    }
}
