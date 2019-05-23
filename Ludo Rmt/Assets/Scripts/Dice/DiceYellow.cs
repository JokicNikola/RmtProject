using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceYellow : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;

    
    public int randomDiceSide1 = 0;
    public bool click;


    private PawnYellow pawn1;

    private PawnYellow pawn2;

    private PawnYellow pawn3;

    private PawnYellow pawn4;

    private Controller boardC;

    private DiceGreen nextDc;
    private GameObject nextDice;

    private GameObject beforeDice;
    private Dice beforeDc;

    // Start is called before the first frame update
    void Start()
    {
        
        pawn1 = GameObject.Find("pawn (9)").GetComponent<PawnYellow>();
        pawn2 = GameObject.Find("pawn (10)").GetComponent<PawnYellow>();
        pawn3 = GameObject.Find("pawn (11)").GetComponent<PawnYellow>();
        pawn4 = GameObject.Find("pawn (12)").GetComponent<PawnYellow>();
        //click = true;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        
        boardC = GameObject.Find("board").GetComponent<Controller>();

        nextDice = GameObject.Find("Green Dice");
        nextDc = nextDice.GetComponent<DiceGreen>();

        beforeDice = GameObject.Find("Dice");
        beforeDc = beforeDice.GetComponent<Dice>();
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
            UnityEngine.Debug.Log("TI SI ŽUTI, NIJE TVOJ POTEZ!");
        }
    }

    private IEnumerator RollTheDice()
    {
       
        randomDiceSide1 = -1;

        boardC.client.Send("Roll|Yellow");

        yield return new WaitForSeconds(2f);



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

            for (int i = 0; i < boardC.listaNapolju.Count; i++)
            {
                Position pijun = GameObject.Find(boardC.listaNapolju.ElementAt(i)).GetComponent<Position>();
                if ((pijun.koraci + randomDiceSide1 + 1) <= 95)
                {
                    canPlay = true;
                    break;
                }
            }

            Debug.Log("canPlay: " + canPlay + " napolju :" + boardC.napolju + "unutra: " + boardC.unutra + " kockica " + randomDiceSide1);

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
