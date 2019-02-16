using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private GameObject check;
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public bool out_ = false;
    public int randomDiceSide;
    public int randomDiceSide1 = 0;
    public bool click;

    private GameObject board;
    private Controller boardC;

    private DiceYellow nextDc;
    private GameObject nextDice;
    


    private void Start () {
        click = false;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

        nextDice = GameObject.Find("Side6 (1)");
        nextDc = nextDice.GetComponent<DiceYellow>();
    }

    

    private void OnMouseDown()
    {
        if (!click && boardC.blueTurn)
        {
            click = true;
            StartCoroutine("RollTheDice");
        } else
        {
            UnityEngine.Debug.Log("TI SI PLAVI, NIJE TVOJ POTEZ!");
        }
    }


    private IEnumerator RollTheDice()
    {
        randomDiceSide = 0;
        randomDiceSide1 = 0;



        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
            
        }
        randomDiceSide1 = Random.Range(0, 6);
        rend.sprite = diceSides[randomDiceSide1];

        if((randomDiceSide1 + 1)!=6 && boardC.blueTurn == true && boardC.outBlue == 0)
        {
            click = true;
            boardC.blueTurn = false;
            boardC.yellowTurn = true;
            nextDc.click = false;
        }


    }
}
