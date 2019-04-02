using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{


    public bool isMyMove;
    public string readData;
    private Position pawn;


    private DiceYellow yellow;
    private DiceRed red;
    private DiceGreen green;
    private Dice blue;
    private BoxCollider2D collide;

    public int endBlue;
    public int endGreen;
    public int endYellow;
    public int endRed;


    public int outBlue;
    public int outYellow;
    public int outRed;
    public int outGreen;
    private int rand;

    public Client client;


    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 4);
        isMyMove = false;
        readData = "";
        client = FindObjectOfType<Client>();
        //client.Send(client.clientColor+" ovo je iz table!!!");

        switch (client.clientColor)
        {

            case "Red":
                red = GameObject.Find("Red Dice").GetComponent<DiceRed>();
                collide = GameObject.Find("Red Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                isMyMove = true;
                break;
            case "Blue":
                blue = GameObject.Find("Blue Dice").GetComponent<Dice>();
                collide = GameObject.Find("Blue Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                break;
            case "Green":
                green = GameObject.Find("Green Dice").GetComponent<DiceGreen>();
                collide = GameObject.Find("Green Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                break;
            case "Yellow":
                yellow = GameObject.Find("Yellow Dice").GetComponent<DiceYellow>();
                collide = GameObject.Find("Yellow Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                break;
            default: break;

        }

        //Debug.Log("board"+client.clientColor);

        
        //Destroy(yellow);
        //yellow = GameObject.Find("Side6 (1)").GetComponent<BoxCollider2D>();
        //yellow.enabled = false;
        
        
        endBlue=0;
        endGreen=0;
        endYellow=0;
        endRed=0;

        outBlue = 0;
        outYellow = 0;
        outRed = 0;
        outGreen = 0;
   
    }

    IEnumerator jkj(string s, string tag)
    {
        Debug.Log("usao");

        switch (tag)
        {
            case "BLUE":
                
                    for (int i = 0; i < int.Parse(s); i++)
                    {
                        if (pawn.koraci == 52)
                        {
                            pawn.koraci = 0;
                        }
                        if (pawn.koraci == 13)
                        {
                            pawn.koraci = 69;
                        }
                        pawn.koraci++;
                        yield return new WaitForSeconds(12f * Time.deltaTime);
                    }
                break;
            case "RED":
                for (int i = 0; i < int.Parse(s); i++)
                {
                    
                    pawn.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                break;
            case "YELLOW":
                for (int i = 0; i < int.Parse(s); i++)
                {
                    if (pawn.koraci == 52)
                    {
                        pawn.koraci = 0;
                    }
                    if (pawn.koraci ==26)
                    {
                        pawn.koraci = 89;
                    }
                    pawn.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                break;
            case "GREEN":
                for (int i = 0; i < int.Parse(s); i++)
                {
                    if (pawn.koraci == 52)
                    {
                        pawn.koraci = 0;
                    }
                    if (pawn.koraci == 39)
                    {
                        pawn.koraci = 79;
                    }
                    pawn.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                break;
            default: break;


        }

        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (client.isMyMove)
            isMyMove = true;

        

        if(client.readData.StartsWith("$"))
        {
            string readData = client.readData.Substring(1);
            string[] split =readData.Split('|');
            pawn = GameObject.Find(split[0]).GetComponent<Position>();
            Debug.Log(pawn.tag);


            if (pawn != null)
            {
               
                if (split[1].Equals("out"))
                    pawn._out = true;
                else
                {
                    StartCoroutine(jkj(split[1], pawn.tag));
                    

                    // pawn.index = int.Parse(split[1]);
                    /*   for(int i=0;i< int.Parse(split[1]); i++)
                       {
                           pawn.koraci++;
                           new WaitForSeconds(12f * Time.deltaTime);
                       }
                       */
                }

                client.readData = "";

            }


        }

        
    }
}
