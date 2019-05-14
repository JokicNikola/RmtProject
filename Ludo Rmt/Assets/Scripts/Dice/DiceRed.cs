using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRed : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;
    
    public int randomDiceSide;
    public int randomDiceSide1 = 0;
    public bool click;


    private Pawn pawn1;

    private Pawn pawn2;

    private Pawn pawn3;

    private Pawn pawn4;

    private Controller boardC;

    private Dice nextDc;
    private GameObject nextDice;

    private GameObject beforeDice;
    private DiceGreen beforeDc;

    // Start is called before the first frame update
    void Start()
    {
       
        pawn1 = GameObject.Find("pawn").GetComponent<Pawn>();
        pawn2 = GameObject.Find("pawn (1)").GetComponent<Pawn>();
        pawn3 = GameObject.Find("pawn (2)").GetComponent<Pawn>();
        pawn4 = GameObject.Find("pawn (3)").GetComponent<Pawn>();

        click = false;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        boardC = GameObject.Find("board").GetComponent<Controller>();

        nextDice = GameObject.Find("Blue Dice");
        nextDc = nextDice.GetComponent<Dice>();

        beforeDice = GameObject.Find("Green Dice");
        beforeDc = beforeDice.GetComponent<DiceGreen>();
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
            UnityEngine.Debug.Log("TI SI CRVENI, NIJE TVOJ POTEZ!");
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
            click =false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;

        }
        if (((randomDiceSide1 + 1 + pawn1.position.index > 58) || !pawn1.position._out) && ((randomDiceSide1 + 1 + pawn2.position.index > 58) || !pawn2.position._out)
            && ((randomDiceSide1 + 1 + pawn3.position.index > 58) || !pawn3.position._out) && ((randomDiceSide1 + 1 + pawn4.position.index > 58) || !pawn4.position._out))
        {
            click = false;
            boardC.client.Send("Played");
            boardC.client.isMyMove = false;
            boardC.isMyMove = false;
        }
    }

    
}
