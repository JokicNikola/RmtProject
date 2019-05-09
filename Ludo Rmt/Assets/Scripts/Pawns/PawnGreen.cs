using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnGreen : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    
    private DiceGreen dc;
    private DiceRed nextDc;

    private Controller boardC;

    private int randomDiceSide1;
    private int index;

    public Position position;


    // Start is called before the first frame update
    void Start()
    {
        index = 41;
        randomDiceSide1 = 0;

        dice = GameObject.Find("Green Dice");
        nextDice = GameObject.Find("Red Dice");

        dc = dice.GetComponent<DiceGreen>();
        nextDc = nextDice.GetComponent<DiceRed>();

        boardC = GameObject.Find("board").GetComponent<Controller>();

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
        if (dc.click && boardC.isMyMove)
        {
            StartCoroutine("Move");
            
        }
        else Debug.Log("Nije bacena");
    }

    private void Move()
    {
       
        randomDiceSide1 = dc.randomDiceSide1;
      
        if (!position._out && (randomDiceSide1 + 1) == 6)
        {
           
            position._out = true;
            boardC.outGreen++;
            boardC.napolju++;
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 86 && position._out)
            {
                position.index = position.koraci + randomDiceSide1 + 1;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1+1));
                dc.click = false;
               
                if (position.koraci == 85)
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
                    dc.click = false;
                    System.Threading.Thread.Sleep(100);
                    
                    boardC.client.Send("Played");
                    boardC.client.isMyMove = false;
                    boardC.isMyMove = false;

                }
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boardC.isMyMove && position.index == collision.GetComponent<Position>().index && collision.gameObject.tag != "GREEN")
        {
            UnityEngine.Debug.Log("Trigerovao se! zeleni");


            collision.transform.position = collision.GetComponent<Position>().onStart;
            collision.GetComponent<Position>()._out = false;

            if (collision.gameObject.tag == "BLUE")
            {
                boardC.outBlue--;
            }
            if (collision.gameObject.tag == "RED")
            {
                boardC.outRed--;
            }
            if (collision.gameObject.tag == "YELLOW")
            {
                boardC.outYellow--;
            }
        }
    }
}
