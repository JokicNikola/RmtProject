using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBlue : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    public Position position;

    private Dice dc;
    private DiceYellow nextDc;
    
    private Controller boardC;

    int randomDiceSide1 = 0;
    int index = 15;
    
    

    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Blue Dice");
        dc = dice.GetComponent<Dice>();

        nextDice = GameObject.Find("Yellow Dice");
        nextDc = nextDice.GetComponent<DiceYellow>();

        position = GetComponent<Position>();
        position.index = index;
        position.koraci = index;


        boardC = GameObject.Find("board").GetComponent<Controller>();

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
        Debug.Log(position.index);
    }

    private IEnumerator Move()
    {
       
        randomDiceSide1 = dc.randomDiceSide1;

        if (!position._out && (randomDiceSide1 + 1) == 6)
        {

            position._out = true;
            boardC.outBlue++;
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
        }
        else
        {

            
            if ((position.koraci + randomDiceSide1 + 1) < 76 && position._out)
            {
               // position.index = position.koraci + randomDiceSide1 + 1;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1+1));


                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (position.koraci == 52)
                    {
                        position.koraci = 0;
                        position.index = 0;
                    }
                    if (position.koraci == 13)
                    {
                        position.koraci = 69;
                        position.index = 69;
                    }
                    position.koraci++;
                   yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                

                if(position.koraci == 75)
                {
                    boardC.outBlue--;
                    boardC.endBlue++;
                }

                if((randomDiceSide1 + 1) == 6 || randomDiceSide1==-1)
                {
                    dc.click = false;

                }else {

                    dc.click = false;
                    boardC.client.Send("Played");
                    boardC.client.isMyMove = false;
                    boardC.isMyMove = false;

                }
               
            }
            else
            {
                dc.click = false;
                boardC.client.Send("Played");
                boardC.client.isMyMove = false;
                boardC.isMyMove = false;

                //dc.rend.sprite = dc.diceSides[5];
            }
        }
        //dc.rend.sprite = dc.diceSides[5];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (boardC.isMyMove && position.index==collision.GetComponent<Position>().index && collision.gameObject.tag != "BLUE")
        {

            UnityEngine.Debug.Log("Trigerovao se! plavi");
            collision.transform.position = collision.GetComponent<Position>().onStart;
            collision.GetComponent<Position>()._out = false;

            if (collision.gameObject.tag == "RED")
            {
                boardC.outRed--;
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
