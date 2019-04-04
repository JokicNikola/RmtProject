using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;
    public bool out_ = false;
    public int randomDiceSide;
    public int randomDiceSide1 = 0;
    public bool click;

    private GameObject myPawn1;
    private PawnBlue pawn1;
    private GameObject myPawn2;
    private PawnBlue pawn2;
    private GameObject myPawn3;
    private PawnBlue pawn3;
    private GameObject myPawn4;
    private PawnBlue pawn4;


    private GameObject board;
    private Controller boardC;

    private DiceYellow nextDc;
    private GameObject nextDice;

    private GameObject beforeDice;
    private DiceRed beforeDc;

    


    private void Start () {

        myPawn1 = GameObject.Find("pawn (4)");
        pawn1 = myPawn1.GetComponent<PawnBlue>();
        myPawn2 = GameObject.Find("pawn (5)");
        pawn2 = myPawn1.GetComponent<PawnBlue>();
        myPawn3 = GameObject.Find("pawn (6)");
        pawn3 = myPawn1.GetComponent<PawnBlue>();
        myPawn4 = GameObject.Find("pawn (7)");
        pawn4 = myPawn1.GetComponent<PawnBlue>();

        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

        nextDice = GameObject.Find("Yellow Dice");
        nextDc = nextDice.GetComponent<DiceYellow>();

        beforeDice = GameObject.Find("Red Dice");
        beforeDc = beforeDice.GetComponent<DiceRed>();
    }

    

    private void OnMouseDown()
    {
        if (boardC.isMyMove && !click)
        {
            click = true;
            StartCoroutine("RollTheDice");
            //beforeDc.rend.sprite = beforeDc.diceSides[5];
        } else
        {
            UnityEngine.Debug.Log("TI SI PLAVI, NIJE TVOJ POTEZ!");
        }
    }


    private IEnumerator RollTheDice()
    {
        randomDiceSide = 0;
        randomDiceSide1 = -1;



        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);

        }
        randomDiceSide1 = Random.Range(3, 6);
        rend.sprite = diceSides[randomDiceSide1];

        if ((randomDiceSide1 + 1) != 6 && boardC.napolju == 0)
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;
        }

        /*if(((randomDiceSide1 + 1 + pawn1.position.koraci) > 95) && ((randomDiceSide1+1+pawn2.position.koraci) > 95)
            ((randomDiceSide1 + 1 + pawn3.position.koraci) > 95) && ((randomDiceSide1 + 1 + pawn4.position.koraci) > 95))
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;
        }*/
      
        if(randomDiceSide1 + 1 + pawn1.position.koraci > 75)
        {
            if (randomDiceSide1 + 1 + pawn2.position.koraci > 75)
            {
                if (randomDiceSide1 + 1 + pawn3.position.koraci > 75)
                {
                    if (randomDiceSide1 + 1 + pawn4.position.koraci > 75)
                    {
                        click = false;
                        boardC.client.Send("Played");
                        boardC.client.isMyMove = false;
                        boardC.isMyMove = false;
                    }
                }
            }
        }

    }

   
}
