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

    IEnumerator jkj(string s)
    {
        Debug.Log("usao");
        for (int i = 0; i < int.Parse(s); i++)
        {
            pawn.koraci++;
            yield return new WaitForSeconds(12f * Time.deltaTime);
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
            Debug.Log(readData);
            string[] split =readData.Split('|');
            Debug.Log(split[0]);
            pawn = GameObject.Find(split[0]).GetComponent<Position>();
            Debug.Log(pawn.koraci);

            if (pawn != null)
            {
                Debug.Log(pawn.koraci);
                if (split[1].Equals("out"))
                    pawn._out = true;
                else
                {
                    StartCoroutine(jkj(split[1]));
                    

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
