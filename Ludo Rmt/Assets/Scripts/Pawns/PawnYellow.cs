using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYellow : MonoBehaviour
{
    private GameObject dice;
    private GameObject nextDice;
    private GameObject check;
    private GameObject board;

    private DiceYellow dc;
    private DiceGreen nextDc;

    private Controller boardC;

    int randomDiceSide1 = 0;
    int index = 28;
    private bool out_ = false;
    

    // Start is called before the first frame update
    void Start()
    { 
        dice = GameObject.Find("Side6 (1)");
        nextDice = GameObject.Find("Side6 (2)");

        dc = dice.GetComponent<DiceYellow>();
        nextDc = nextDice.GetComponent<DiceGreen>();

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        

            if (out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        
    }
    private void OnMouseDown()
    {
        if (dc.click && boardC.yellowTurn)
            StartCoroutine("Move");
        else Debug.Log("Nije bacena");
    }

    private IEnumerator Move()
    {
       
      
        randomDiceSide1 = dc.randomDiceSide1;
        
        
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            
            out_ = true;
            boardC.outYellow++;
            dc.click = false;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 96 && out_)
            {
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    
                    if (index == 52)
                    {
                        index = 0;
                    }
                    if (index == 26)
                    {
                        index = 89;
                    }
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                if (index == 95)
                {
                    boardC.outYellow--;
                }
                nextDc.click = false;
                boardC.yellowTurn = false;
                boardC.greenTurn = true;

            }
        }
    }
}
