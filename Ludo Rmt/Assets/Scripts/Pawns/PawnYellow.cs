using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYellow : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    private DiceYellow dc;
    private DiceGreen nextDc;

    public Position position;

    private Controller boardC;

    int randomDiceSide1 = 0;
    public int index = 28;
    


    
    

    // Start is called before the first frame update
    void Start()
    { 
        dice = GameObject.Find("Side6 (1)");
        nextDice = GameObject.Find("Side6 (2)");

        dc = dice.GetComponent<DiceYellow>();
        nextDc = nextDice.GetComponent<DiceGreen>();

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
        if (dc.click && boardC.yellowTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
        Debug.Log(position.index);
    }

    private IEnumerator Move()
    {
       
      
        randomDiceSide1 = dc.randomDiceSide1;
        
        
        if (!position._out && (randomDiceSide1 + 1) == 6)
        {

            position._out = true;
            boardC.outYellow++;
            dc.click = false;
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 96 && position._out)
            {
                position.index = position.koraci + randomDiceSide1 + 1;

                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (position.koraci == 52)
                    {
                        position.koraci = 0;
                        position.index = 0;
                    }
                    if (position.koraci == 26)
                    {
                        position.koraci = 89;
                        position.index = 89;
                    }
                    position.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                if (position.koraci == 95)
                {
                    boardC.outYellow--;
                    boardC.endYellow++;
                }

                if ((randomDiceSide1 + 1) == 6 || randomDiceSide1 == -1)
                {
                    dc.click = false;

                }
                else
                {
                    nextDc.click = false;
                    boardC.yellowTurn = false;
                    boardC.greenTurn = true;
                }

            }
            else
            {
                nextDc.click = false;
                boardC.yellowTurn = false;
                boardC.greenTurn = true;
                dc.rend.sprite = dc.diceSides[5];
            }
        }
        dc.rend.sprite = dc.diceSides[5];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (boardC.yellowTurn && position.index == collision.GetComponent<Position>().index)
        {
            UnityEngine.Debug.Log("Trigerovao se! zuti");


            collision.transform.position = collision.GetComponent<Position>().onStart;
            collision.GetComponent<Position>()._out = false;
        }
    }

    
}
