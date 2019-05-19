using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    private PawnBlue pawn1;
    private PawnBlue pawn2;
    private PawnBlue pawn3;
    private PawnBlue pawn4;


    private GameObject board;
    private Controller boardC;

    private DiceYellow nextDc;
    private GameObject nextDice;

    private GameObject beforeDice;
    private DiceRed beforeDc;




    private void Start() {


        pawn1 = GameObject.Find("pawn (4)").GetComponent<PawnBlue>();
        pawn2 = GameObject.Find("pawn (5)").GetComponent<PawnBlue>();
        pawn3 = GameObject.Find("pawn (6)").GetComponent<PawnBlue>();
        pawn4 = GameObject.Find("pawn (7)").GetComponent<PawnBlue>();

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
        randomDiceSide1 = Random.Range(0, 5);
        rend.sprite = diceSides[randomDiceSide1];

        if ((randomDiceSide1 + 1) != 6 && boardC.napolju == 0)
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;
        }

        if (boardC.napolju != 0)
        {
            bool canPlay = false;

            for(int i = 0; i < boardC.listaNapolju.Count; i++)
            {
                Position pijun = GameObject.Find(boardC.listaNapolju.ElementAt(i)).GetComponent<Position>();
                if((pijun.koraci + randomDiceSide1 + 1) <= 75)
                {
                    canPlay = true;
                    break;
                }              
            }

            Debug.Log("canPlay: " + canPlay + " napolju :"+boardC.napolju+ "unutra: " + boardC.unutra + " kockica " + randomDiceSide1);

            if (!canPlay)
            {
                if (boardC.napolju + boardC.unutra == 4 || (boardC.napolju + boardC.unutra < 4 && randomDiceSide1 < 5))
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
