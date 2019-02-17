using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnGreen : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    private DiceGreen dc;
    private DiceRed nextDc;

    private Controller boardC;

    int randomDiceSide1 = 0;
    int index = 41;
    private bool out_ = false;
    

    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Side6 (2)");
        nextDice = GameObject.Find("Side6 (3)");

        dc = dice.GetComponent<DiceGreen>();
        nextDc = nextDice.GetComponent<DiceRed>();

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        

            if (out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        
    }
    private void OnMouseDown()
    {
        if (dc.click && boardC.greenTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
    }

    private IEnumerator Move()
    {
       
        randomDiceSide1 = dc.randomDiceSide1;
      
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
           
            out_ = true;
            boardC.outGreen++;
            dc.click = false;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 86 && out_)
            {
                
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (index == 52)
                    {
                        index = 0;
                    }
                    if (index == 39)
                    {
                        index = 79;
                    }
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }

                if (index == 85)
                {
                    boardC.outGreen--;
                    boardC.endGreen++;
                }

                if ((randomDiceSide1 + 1) == 6 || randomDiceSide1 == -1)
                {
                    dc.click = false;

                }
                else
                {
                    nextDc.click = false;
                    boardC.greenTurn = false;
                    boardC.redTurn = true;
                }
            }else
            {
                nextDc.click = false;
                boardC.greenTurn = false;
                boardC.redTurn = true;
            }
        }
        dc.rend.sprite = dc.diceSides[5];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Trigerovao se!");
    }
}
