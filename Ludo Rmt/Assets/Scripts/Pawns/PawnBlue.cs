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
        dice = GameObject.Find("Side6");
        dc = dice.GetComponent<Dice>();

        nextDice = GameObject.Find("Side6 (1)");
        nextDc = nextDice.GetComponent<DiceYellow>();

        position = GetComponent<Position>();
        position.index = index;
        position.index1 = index;

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

    }

    // Update is called once per frame
    void Update()
    {


        if (position._out)
        {
            check = GameObject.Find("Waypoint (" + position.index1 + ")");
            transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
        }
        else position.index1 = index;





    }
    private void OnMouseDown()
    {
        if (dc.click && boardC.blueTurn)
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
            boardC.outBlue++;
            dc.click = false;
        }
        else
        {

            
            if ((position.index1 + randomDiceSide1 + 1) < 76 && position._out)
            {
                position.index = position.index1 + randomDiceSide1 + 1;


                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (position.index1 == 52)
                    {
                        position.index1 = 0;
                        position.index = 0;
                    }
                    if (position.index1 == 13)
                    {
                        position.index1 = 69;
                        position.index = 69;
                    }
                    position.index1++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }

                if(position.index1 == 75)
                {
                    boardC.outBlue--;
                    boardC.endBlue++;
                }

                if((randomDiceSide1 + 1) == 6 || randomDiceSide1==-1)
                {
                    dc.click = false;

                }else {

                    nextDc.click = false;
                    boardC.blueTurn = false;
                    boardC.yellowTurn = true;
                }
               
            }
            else
            {
                nextDc.click = false;
                boardC.blueTurn = false;
                boardC.yellowTurn = true;
                dc.rend.sprite = dc.diceSides[5];
            }
        }
        dc.rend.sprite = dc.diceSides[5];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Trigerovao se! plavi");
        if (boardC.blueTurn && position.index==collision.GetComponent<Position>().index)
        {
            
            //Destroy(collision.gameObject);
            collision.transform.position = collision.GetComponent<Position>().onStart;
            //collision.GetComponent<Position>().index1 = index;
            collision.GetComponent<Position>()._out = false;
        }
    }
}
