using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceGreen : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;
    
    public int randomDiceSide;
    public int randomDiceSide1 = 0;
    public bool click;


    private PawnGreen pawn1;

    private PawnGreen pawn2;

    private PawnGreen pawn3;

    private PawnGreen pawn4;

    private GameObject board;
    private Controller boardC;

    private DiceRed nextDc;
   

   
    private DiceYellow beforeDc;

    // Start is called before the first frame update
    void Start()
    {
       
        pawn1 = GameObject.Find("pawn (8)").GetComponent<PawnGreen>();
       
        pawn2 = GameObject.Find("pawn (13)").GetComponent<PawnGreen>();
     
        pawn3 = GameObject.Find("pawn (14)").GetComponent<PawnGreen>();
       
        pawn4 = GameObject.Find("pawn (15)").GetComponent<PawnGreen>();
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

        if (boardC.napolju != 0)
        {
            bool canPlay = false;

            for (int i = 0; i < boardC.listaNapolju.Count; i++)
            {
                Position pijun = GameObject.Find(boardC.listaNapolju.ElementAt(i)).GetComponent<Position>();
                if ((pijun.koraci + randomDiceSide1 + 1) <= 85)
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
