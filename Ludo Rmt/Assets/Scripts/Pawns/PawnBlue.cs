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

    private int randomDiceSide1;
    private int index;
    
    

    // Start is called before the first frame update
    void Start()
    {
        index = 15;
        randomDiceSide1 = -1;

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

    private void Move()
    {
       
        randomDiceSide1 = dc.randomDiceSide1;

        if (!position._out && (randomDiceSide1 + 1) == 6)
        {

            position._out = true;
            boardC.outBlue++;
            boardC.napolju++;
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
        }
        else
        {

            
            if ((position.koraci + randomDiceSide1 + 1) < 76 && position._out)
            {
            
                
               
                dc.click = false;

                if (position.koraci == 75)
                {
                    boardC.outBlue--;
                    boardC.endBlue++;
                }

                if((randomDiceSide1 + 1) == 6 || randomDiceSide1==-1)
                {
                 
                    boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1));
                } else {

                   
                                     
                    boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1)+"|Played");
                    boardC.client.isMyMove = false;
                    boardC.isMyMove = false;

                }
               
            }
            
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.tag + ":" + position.index+"->"+boardC.client.whosMove);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index+"->" + boardC.client.whosMove);

        if (this.tag != collision.tag && position.index==collision.GetComponent<Position>().index &&  
            (boardC.client.whosMove.Equals("Yellow") || randomDiceSide1 == 5))
        {
            boardC.client.Send("%" + collision.name);
            // Debug.Log(this.tag + ":" + position.index);
            // Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index);

            // UnityEngine.Debug.Log("Trigerovao se! plavi");
            //collision.transform.position = collision.GetComponent<Position>().onStart;
            //collision.GetComponent<Position>()._out = false;

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
