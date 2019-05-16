using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;

    

    private DiceRed dc;
    private Dice nextDc;

    private Controller boardC;

    private int randomDiceSide1;
    private int index;

    public Position position;

    // Start is called before the first frame update
    void Start()
    {
        
        index = 2;
        randomDiceSide1 = 0;

        nextDice = GameObject.Find("Blue Dice");

        dc = GameObject.Find("Red Dice").GetComponent<DiceRed>();
        nextDc = nextDice.GetComponent<Dice>();

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
        else
        {
            position.koraci = index;
            position.index = index;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(this.name+":"+this.position.index+this.tag);
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
            boardC.napolju++;
            boardC.listaNapolju.AddFirst(this.name);
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
            
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 59 && position._out)
            {

                dc.click = false;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1));
         
            }
        } 
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(this.tag + ":" + position.index + "->" + boardC.client.isMyMove);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index + "->" + boardC.client.isMyMove);

        if (this.tag != collision.tag && position.index == collision.GetComponent<Position>().index && boardC.client.whosMove.Equals(this.tag) && boardC.client.isMyMove)
        {
              
            boardC.client.Send("%"+collision.name);
                
                //collision.transform.position = collision.GetComponent<Position>().onStart;
                //collision.GetComponent<Position>()._out = false;        
            
        }
        
    }
    
}
