using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGreen : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;
    
    public int randomDiceSide;
    public int randomDiceSide1 = 0;
    public bool click;

    private GameObject myPawn1;
    private PawnGreen pawn1;
    private GameObject myPawn2;
    private PawnGreen pawn2;
    private GameObject myPawn3;
    private PawnGreen pawn3;
    private GameObject myPawn4;
    private PawnGreen pawn4;

    private GameObject board;
    private Controller boardC;

    private DiceRed nextDc;
   

   
    private DiceYellow beforeDc;

    // Start is called before the first frame update
    void Start()
    {
        myPawn1 = GameObject.Find("pawn (8)");
        pawn1 = myPawn1.GetComponent<PawnGreen>();
        myPawn2 = GameObject.Find("pawn (13)");
        pawn2 = myPawn1.GetComponent<PawnGreen>();
        myPawn3 = GameObject.Find("pawn (14)");
        pawn3 = myPawn1.GetComponent<PawnGreen>();
        myPawn4 = GameObject.Find("pawn (15)");
        pawn4 = myPawn1.GetComponent<PawnGreen>();
        //click = true;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

        
        nextDc = GameObject.Find("Green Dice").GetComponent<DiceRed>();

        
        beforeDc = GameObject.Find("Yellow Dice").GetComponent<DiceYellow>();
    }

    private void OnMouseDown()
    {
        if (boardC.isMyMove && !click)
        {
            click = true;
            StartCoroutine("RollTheDice");
            
        }
        else
        {
            UnityEngine.Debug.Log("TI SI ZELENI, NIJE TVOJ POTEZ!");
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

        randomDiceSide1 = Random.Range(0, 6);
        rend.sprite = diceSides[randomDiceSide1];

        if ((randomDiceSide1 + 1) != 6 && boardC.napolju == 0)
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;

        }
        if (randomDiceSide1 + 1 + pawn1.position.koraci > 75)
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
