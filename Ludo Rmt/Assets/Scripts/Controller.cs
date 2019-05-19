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
    public int napolju;

    public Client client;
    public LinkedList<string> listaNapolju;
    

    // Start is called before the first frame update
    void Start()
    {
       
        isMyMove = false;
        client = FindObjectOfType<Client>();

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
        listaNapolju = new LinkedList<string>();
        

}

    IEnumerator move(string number, string tag)
    {
        

        pawn.index += int.Parse(number);

        if (pawn.index > 52)
            pawn.index = pawn.index - 52;

        switch (tag)
        {
            
            case "Blue":
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
                if (pawn.koraci == 75 && pawn.tag == client.clientColor)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            case "Red":
                for (int i = 0; i < int.Parse(number); i++)
                {
                    
                    pawn.koraci++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
                if (pawn.koraci == 58 && pawn.tag == client.clientColor)
                {
                    unutra++;
                    napolju--;
                };
              
                break;
            case "Yellow":
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

                if (pawn.koraci == 95 && pawn.tag == client.clientColor)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            case "Green":
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
                if (pawn.koraci == 85 && pawn.tag==client.clientColor)
                {
                    unutra++;
                    napolju--;
                };
                
                break;
            default: break;

              
        }

        foreach (string s in listaNapolju)
        {
            Debug.Log("Ovaj je u listi "+s);
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
            client.Send("END|" + client.clientColor);
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
                listaNapolju.Remove(pawnName);
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
                {
                    pawn._out = true;      
                }
                    
                else
                {
                    
                    StartCoroutine(move(split[1], GameObject.Find(split[0]).tag));

                }

                client.readData = "";

            }
        }       
    }
}
