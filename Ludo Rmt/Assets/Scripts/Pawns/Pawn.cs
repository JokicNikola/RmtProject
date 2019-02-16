﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    private DiceRed dc;
    private Dice nextDc;

    private Controller boardC;

    int randomDiceSide1=0;
    int index = 2;
    private bool out_ = false;


    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Side6 (3)");
        nextDice = GameObject.Find("Side6");

        dc = dice.GetComponent<DiceRed>();
        nextDc = nextDice.GetComponent<Dice>();

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (!out_ && (randomDiceSide1 + 1) == 6)
       // {
       //     check = GameObject.Find("Waypoint (2)");
       //     transform.position = check.transform.position;
       // }
       // else
        //{

            if (out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        //}

    }

    private void OnMouseDown()
    {
        if(dc.click && boardC.redTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
    }

    private IEnumerator Move()
    {
        //click = dice.GetComponent<Dice>().click;
        
        randomDiceSide1 = dc.randomDiceSide1;
      
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
           // check = GameObject.Find("Waypoint (2)");
           // transform.position = check.transform.position;
            out_ = true;
            boardC.outRed++;
            dc.click = false;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 59 && out_)
            {
                
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }

                if (index == 58)
                {
                    boardC.outRed--;
                }

                if ((randomDiceSide1 + 1) == 6)
                {
                    dc.click = false;

                }
                else
                {
                    nextDc.click = false;
                    boardC.redTurn = false;
                    boardC.blueTurn = true;
                }
            }
        }
        
    }


}
