﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRed : MonoBehaviour
{
    private GameObject check;
    public Sprite[] diceSides;
    public SpriteRenderer rend;
    
   
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
        
        randomDiceSide1 = -1;

        boardC.client.Send("Roll|Red");

        yield return new WaitForSeconds(2f);


        if ((randomDiceSide1 + 1) != 6 && boardC.napolju == 0)
        {
            click =false;
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
                if ((pijun.koraci + randomDiceSide1 + 1) <= 58)
                {
                    canPlay = true;
                    break;   
                }
            }

            Debug.Log("canPlay: " + canPlay + " napolju :" + boardC.napolju + "unutra: " + boardC.unutra + " kockica " + randomDiceSide1);

            if (!canPlay )
            {
                if (boardC.napolju + boardC.unutra == 4 || (boardC.napolju + boardC.unutra <4 && randomDiceSide1<5))
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

    

