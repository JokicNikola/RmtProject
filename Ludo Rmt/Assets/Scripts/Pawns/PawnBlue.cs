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
        else
        {
            position.koraci = index;
            position.index = index;
        }





        }
    private void OnMouseDown()
    {
        if (dc.click && boardC.isMyMove)
        {
            StartCoroutine("Move");
            
        }
        else Debug.Log("Nije bacena");
        Debug.Log(this.name + ":" + this.position.koraci + this.tag);
    }

    private void Move()
    {
       
        randomDiceSide1 = dc.randomDiceSide1;

        if (!position._out && (randomDiceSide1 + 1) == 6)
        {

            position._out = true;
            boardC.napolju++;
            boardC.listaNapolju.AddFirst(this.name);
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
            
        }
        else
        {

            
            if ((position.koraci + randomDiceSide1 + 1) < 76 && position._out)
            {
            
                dc.click = false;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1));
              
            }
            
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.tag + ":" + position.index+"->"+boardC.client.isMyMove);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index+"->" + boardC.client.isMyMove);

        if (this.tag != collision.tag && position.index == collision.GetComponent<Position>().index && boardC.client.whosMove.Equals(this.tag) && boardC.client.isMyMove)
        {
            
                boardC.client.Send("%" + collision.name);

            //collision.transform.position = collision.GetComponent<Position>().onStart;
            //collision.GetComponent<Position>()._out = false;        

        }
        
    }
}
