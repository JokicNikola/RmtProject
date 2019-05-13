using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceYellow : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;

    public int randomDiceSide;
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
        if ((randomDiceSide1 + 1 + pawn1.position.index > 95) && (randomDiceSide1 + 1 + pawn2.position.index > 95)
            && (randomDiceSide1 + 1 + pawn3.position.index > 95) && (randomDiceSide1 + 1 + pawn4.position.index > 95))
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;
        }
    }

   
}
