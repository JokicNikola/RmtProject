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

    int randomDiceSide1 = 0;
    public int index = 28;
    


    
    

    // Start is called before the first frame update
    void Start()
    { 

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
            dc.click = false;
            boardC.client.Send("$" + this.name + "|out");
        }
        else
        {

            if ((position.koraci + randomDiceSide1 + 1) < 96 && position._out)
            {
                position.index = position.koraci + randomDiceSide1 + 1;
                boardC.client.Send("$" + this.name + "|" + (randomDiceSide1+1));

               /* for (int i = 0; i < randomDiceSide1 + 1; i++)
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
                */
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

        if (boardC.isMyMove && position.index == collision.GetComponent<Position>().index && collision.gameObject.tag != "YELLOW")
        {
            UnityEngine.Debug.Log("Trigerovao se! zuti");


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

    
}
