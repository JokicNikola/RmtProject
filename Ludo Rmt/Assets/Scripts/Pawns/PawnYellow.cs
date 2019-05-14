using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYellow : MonoBehaviour
{
      
    private GameObject check;
   
    private DiceYellow dc;
    private DiceGreen nextDc;

    public Position position;

    private Controller boardC;

    private int randomDiceSide1 = 0;
    private int index = 28;
    


    

    // Start is called before the first frame update
    void Start()
    {

        index = 28;
        randomDiceSide1 = 0;

        dc = GameObject.Find("Yellow Dice").GetComponent<DiceYellow>();
        nextDc = GameObject.Find("Green Dice").GetComponent<DiceGreen>();
    
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
        if (dc.click && boardC.isMyMove)
        {
            StartCoroutine("Move");
            
        }
        else Debug.Log("Nije bacena");
        Debug.Log(this.name + ":" + this.position.index);
    }

    private void Move()
    {
  
        randomDiceSide1 = dc.randomDiceSide1;
      
        if (!position._out && (randomDiceSide1 + 1) == 6)
        {

            position._out = true;
            boardC.outYellow++;
            boardC.napolju++;
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
           
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 96 && position._out)
            {

                dc.click = false;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1 + 1));
                   


            }        
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(this.tag + ":" + position.index + "->" + boardC.client.whosMove + "->" + randomDiceSide1);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index + "->" + boardC.client.whosMove);

        if (this.tag != collision.tag && position.index == collision.GetComponent<Position>().index && boardC.client.isMyMove)
        {
            
                boardC.client.Send("%" + collision.name);

            //collision.transform.position = collision.GetComponent<Position>().onStart;
            //collision.GetComponent<Position>()._out = false;        

        }
        
    }

    
}
