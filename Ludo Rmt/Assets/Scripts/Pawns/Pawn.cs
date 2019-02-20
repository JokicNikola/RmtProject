using System.Collections;
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

    public Position position;

    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Side6 (3)");
        nextDice = GameObject.Find("Side6");

        dc = dice.GetComponent<DiceRed>();
        nextDc = nextDice.GetComponent<Dice>();

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

        position = GetComponent<Position>();
        position.index = index;
        position.koraci = index;
    }

    // Update is called once per frame
    void Update()
    {
              if (position._out)
            {
                check = GameObject.Find("Waypoint (" + position.koraci + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }
            else position.koraci = index;
    }

    private void OnMouseDown()
    {
        if(dc.click && boardC.redTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
    }

    private IEnumerator Move()
    {
        
        randomDiceSide1 = dc.randomDiceSide1;
      
        if (!position._out && (randomDiceSide1 + 1) == 6)
        {
            position._out = true;
            boardC.outRed++;
            dc.click = false;
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 59 && position._out)
            {
                position.index = position.koraci + randomDiceSide1 + 1;
                
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    position.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }

                if (position.koraci == 58)
                {
                    boardC.outRed--;
                    boardC.endRed++;
                }

                if ((randomDiceSide1 + 1) == 6 || randomDiceSide1 == -1)
                {
                    dc.click = false;

                }
                else
                {
                    nextDc.click = false;
                    boardC.redTurn = false;
                    boardC.blueTurn = true;
                }
            }else
            {
                nextDc.click = false;
                boardC.redTurn = false;
                boardC.blueTurn = true;
            }
        }
        //dc.rend.sprite = dc.diceSides[5];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boardC.redTurn && position.index == collision.GetComponent<Position>().index && collision.gameObject.tag != "RED")
        {
            UnityEngine.Debug.Log("Trigerovao se! crveni");


            collision.transform.position = collision.GetComponent<Position>().onStart;
            collision.GetComponent<Position>()._out = false;

            if(collision.gameObject.tag == "BLUE")
            {
                boardC.outBlue--;
            }
            if (collision.gameObject.tag == "GREEN")
            {
                boardC.outGreen--;
            }
            if (collision.gameObject.tag == "YELLOW")
            {
                boardC.outYellow--;
            }
        }
    }
    
}
