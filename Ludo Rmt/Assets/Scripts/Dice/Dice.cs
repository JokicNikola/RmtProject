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

    private GameObject board;
    private Controller boardC;

    private DiceYellow nextDc;
    private GameObject nextDice;

    private GameObject beforeDice;
    private DiceRed beforeDc;

    


    private void Start () {
        
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
        randomDiceSide1 = Random.Range(5, 6);
        rend.sprite = diceSides[randomDiceSide1];

        if((randomDiceSide1 + 1)!=6 && boardC.outBlue == 0)
        {
            click = true;
            
           
        }

    }

   
}
