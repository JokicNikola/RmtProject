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

    private GameObject board;
    private Controller boardC;

    private Dice nextDc;
    private GameObject nextDice;

    // Start is called before the first frame update
    void Start()
    {
       // click = true;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        board = GameObject.Find("board");
        boardC = board.GetComponent<Controller>();

        nextDice = GameObject.Find("Side6");
        nextDc = nextDice.GetComponent<Dice>();
    }

    private void OnMouseDown()
    {
        if (!click && boardC.redTurn)
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
        randomDiceSide1 = Random.Range(0, 5);
        rend.sprite = diceSides[randomDiceSide1];

        if ((randomDiceSide1 + 1) != 6 && boardC.outRed == 0)
        {
            click = true;
            boardC.redTurn = false;
            boardC.blueTurn = true;
            nextDc.click = false;
        }
    }
}
