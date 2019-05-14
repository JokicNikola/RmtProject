using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool isMyMove;
    

    private Position pawn;
    private GameObject pawn1;
    
    private DiceYellow yellow;
    private DiceRed red;
    private DiceGreen green;
    private Dice blue;
    private BoxCollider2D collide;

    public int unutra;
    private int rand;

    public int napolju;

    public Client client;


    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 4);
        isMyMove = false;
      
        
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
        napolju = 0;
        unutra = 0;

    }

    IEnumerator move(string number, string tag)
    {
        

        pawn.index += int.Parse(number);

        if (pawn.index > 52)
            pawn.index = pawn.index - 52;

        switch (tag)
        {
            
            case "BLUE":
                for (int i = 0; i < int.Parse(number); i++)
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
                if (pawn.index == 75)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            case "RED":
                for (int i = 0; i < int.Parse(number); i++)
                {
                    
                    pawn.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                if (pawn.index == 58)
                {
                    unutra++;
                    napolju--;
                };
              
                break;
            case "YELLOW":
                for (int i = 0; i < int.Parse(number); i++)
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
                if (pawn.index == 95)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            case "GREEN":
                for (int i = 0; i < int.Parse(number); i++)
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
                if (pawn.index == 85)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            default: break;
        }

       // yield return new WaitForSeconds(58f * Time.deltaTime);
       // Debug.Log(client.clientColor+": Usao sam opet u korutinu");

        if (isMyMove)
        {
            if (int.Parse(number) != 6)
            {
                Debug.Log(client.clientColor + " je poslao Played");
                client.isMyMove = false;
                isMyMove = false;
                yield return new WaitForSeconds(58f * Time.deltaTime);
                client.Send("Played");          
            }
        }
        StopAllCoroutines();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (client.isMyMove) 
            isMyMove = true;

        if (unutra == 4)
        {
            client.Send("END!");
        }

        if (client.readData.StartsWith("%"))
        {
            string pawnName = client.readData.Substring(1);           
            pawn1 = GameObject.Find(pawnName);
            pawn = pawn1.GetComponent<Position>();
            pawn1.transform.position = pawn.onStart;
            pawn._out = false;
            if(pawn1.tag == client.clientColor)
            {
                napolju--;
            }
            
            client.readData = "";

        }

        if (client.readData.StartsWith("$"))
        {
            string readData = client.readData.Substring(1);
            if(readData.Contains("-"))
                readData = readData.Substring(0, readData.IndexOf('-'));
           
            string[] split =readData.Split('|');
            pawn = GameObject.Find(split[0]).GetComponent<Position>();
           
            if (pawn != null)
            {
               
                if (split[1].Equals("out"))
                    pawn._out = true;
                else
                {
                    
                    StartCoroutine(move(split[1], pawn.tag));

                }

                client.readData = "";

            }
        }       
    }
}
