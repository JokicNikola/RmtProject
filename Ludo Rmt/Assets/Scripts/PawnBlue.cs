using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBlue : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    private Dice dc;
    private Dice nextDc;
    
    private Controller boardC;

    int randomDiceSide1 = 0;
    int index = 15;
    private bool out_ =false;
    

    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Side6");
        dc = dice.GetComponent<Dice>();

        nextDice = GameObject.Find("Side6 (1)");
        nextDc = nextDice.GetComponent<Dice>();

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (15)");
            transform.position = check.transform.position;
        }
        else
        {

            if (out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        }
    }
    private void OnMouseDown()
    {
        if (dc.click && boardC.blueTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
    }

    private IEnumerator Move()
    {
       
        randomDiceSide1 = dice.GetComponent<Dice>().randomDiceSide1;

        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (15)");
            transform.position = check.transform.position;
            out_ = true;
            dc.click = false;
        }
        else
        {

            
            if ((index + randomDiceSide1 + 1) < 76 && out_)
            {
                
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (index == 52)
                    {
                        index = 0;
                    }
                    if (index == 13)
                    {
                        index = 69;
                    }
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                nextDc.click = false;
                boardC.blueTurn = false;
                boardC.yellowTurn = true;
            }
            
            
        }

    }
}
