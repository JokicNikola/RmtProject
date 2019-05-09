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
            boardC.outYellow++;
            boardC.napolju++;
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 96 && position._out)
            {
                position.index = position.koraci + randomDiceSide1 + 1;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1+1));
                dc.click = false;
               
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
                   
                    dc.click = false;
                    Debug.Log("Cekam!");
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

        Debug.Log(this.tag + ":" + position.index);
        Debug.Log(collision.tag + ":" + collision.GetComponent<Position>().index);

            collision.transform.position = collision.GetComponent<Position>().onStart;
            collision.GetComponent<Position>()._out = false;

            if (collision.gameObject.tag == "BLUE")
            {
                boardC.outBlue--;
            }
            if (collision.gameObject.tag == "GREEN")
            {
                boardC.outGreen--;
            }
            if (collision.gameObject.tag == "RED")
            {
                boardC.outRed--;
            }
        
    }

    
}
