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
        else
        {
            position.koraci = index;
            position.index = index;
        }
        
    }
    private void OnMouseDown()
    {
        Debug.Log(this.name + ":" + this.position.index);
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
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
            
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 86 && position._out)
            {
                          
                dc.click = false;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1));
                
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(this.tag + ":" + position.index + "->" + boardC.client.whosMove+"->"+randomDiceSide1);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index + "->" + boardC.client.whosMove);

        if (this.tag != collision.tag && position.index == collision.GetComponent<Position>().index && boardC.client.isMyMove)
        {
        
                boardC.client.Send("%" + collision.name);

            //collision.transform.position = collision.GetComponent<Position>().onStart;
            //collision.GetComponent<Position>()._out = false;        

        }
        
    }
}
