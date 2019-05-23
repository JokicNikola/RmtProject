using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public GameObject redInd;
    public GameObject blueInd;
    public GameObject yellowInd;
    public GameObject greenInd;

    private bool coroutineRuns;

    private GameObject redTimer;
    private GameObject blueTimer;
    private GameObject yellowTimer;
    private GameObject greenTimer;

    private Sprite[] timerSides;
    private SpriteRenderer render;

    private Coroutine b;

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

      
        timerSides = Resources.LoadAll<Sprite>("TimerSides/");

        isMyMove = false;


        client = FindObjectOfType<Client>();

        redTimer = GameObject.Find("RedTimer");
        blueTimer = GameObject.Find("BlueTimer");
        greenTimer = GameObject.Find("GreenTimer");
        yellowTimer = GameObject.Find("YellowTimer");

        redTimer.SetActive(false);
        blueTimer.SetActive(false);
        greenTimer.SetActive(false);
        yellowTimer.SetActive(false);

        red = GameObject.Find("Red Dice").GetComponent<DiceRed>();
        blue = GameObject.Find("Blue Dice").GetComponent<Dice>();
        green = GameObject.Find("Green Dice").GetComponent<DiceGreen>();
        yellow = GameObject.Find("Yellow Dice").GetComponent<DiceYellow>();

        redInd = GameObject.Find("redIndicator");
        blueInd = GameObject.Find("blueIndicator");
        greenInd = GameObject.Find("greenIndicator");
        yellowInd = GameObject.Find("yellowIndicator");

        redInd.SetActive(false);
        blueInd.SetActive(false);
        greenInd.SetActive(false);
        yellowInd.SetActive(false);

        switch (client.clientColor)
        {

            case "Red":

                collide = GameObject.Find("Red Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                isMyMove = true;
                client.isMyMove = true;
                redInd.SetActive(true);
                render = redTimer.GetComponent<SpriteRenderer>();
                
                
                break;

            case "Blue":

                collide = GameObject.Find("Blue Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                render = blueTimer.GetComponent<SpriteRenderer>();
                break;

            case "Green":

                collide = GameObject.Find("Green Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                render = greenTimer.GetComponent<SpriteRenderer>();
                break;

            case "Yellow":

                collide = GameObject.Find("Yellow Dice").GetComponent<BoxCollider2D>();
                collide.enabled = true;
                render = yellowTimer.GetComponent<SpriteRenderer>();
                break;

            default: break;

        }
        napolju = 0;
        unutra = 0;
        listaNapolju = new LinkedList<string>();


    }

    private IEnumerator Timer()
    {
        
        coroutineRuns = true;
       
        for(int i = 0; i < 8; i++)
        {
            render.sprite = timerSides[i];
            yield return new WaitForSeconds(1.5f);
        }
        coroutineRuns = false;
        

    }

    IEnumerator move(string number, string tag)
    {


        pawn.index += int.Parse(number);
        if (coroutineRuns) {
            StopCoroutine(b);
            coroutineRuns = false;
        }
        

        if (isMyMove && int.Parse(number)==6)
        {
            b = StartCoroutine(Timer());
        }

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
                    if (pawn.koraci == 26)
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
                if (pawn.koraci == 85 && pawn.tag == client.clientColor)
                {
                    unutra++;
                    napolju--;
                };

                break;
            default: break;


        }


        if (isMyMove)
        {
            if (int.Parse(number) != 6)
            {

                client.isMyMove = false;
                isMyMove = false;
                yield return new WaitForSeconds(58f * Time.deltaTime);

                client.Send("Played");

            }
        }
        
    }

    private IEnumerator RollTheDice(string colorDice)
    {
        switch (colorDice)
        {

            case "Red":
                for (int i = 0; i <= 20; i++)
                {
                    int randomDiceSide = Random.Range(0, 6);
                    red.rend.sprite = red.diceSides[randomDiceSide];
                    yield return new WaitForSeconds(0.05f);

                }

                break;

            case "Blue":

                for (int i = 0; i <= 20; i++)
                {
                    int randomDiceSide = Random.Range(0, 6);
                    blue.rend.sprite = red.diceSides[randomDiceSide];
                    yield return new WaitForSeconds(0.05f);

                }

                break;

            case "Green":

                for (int i = 0; i <= 20; i++)
                {
                    int randomDiceSide = Random.Range(0, 6);
                    green.rend.sprite = red.diceSides[randomDiceSide];
                    yield return new WaitForSeconds(0.05f);

                }

                break;

            case "Yellow":

                for (int i = 0; i <= 20; i++)
                {
                    int randomDiceSide = Random.Range(0, 6);
                    yellow.rend.sprite = red.diceSides[randomDiceSide];
                    yield return new WaitForSeconds(0.05f);

                }


                break;

            default:
                break;
        }

        
    }



        // Update is called once per frame
        void Update()
    {
        if (client.isMyMove)
        {
            isMyMove = true;
            switch (client.clientColor)
            {
                case "Red":
                    redInd.SetActive(true);
                    redTimer.SetActive(true);

                    if (!coroutineRuns)
                    {
                        b = StartCoroutine(Timer());
                        
                    }
                    break;
                case "Blue":

                    blueInd.SetActive(true);
                    blueTimer.SetActive(true);

                    if (!coroutineRuns)
                    {
                        b = StartCoroutine(Timer());
                       
                    }
                    break;
                case "Yellow":

                    yellowInd.SetActive(true);
                    yellowTimer.SetActive(true);

                    if (!coroutineRuns)
                    {
                        b = StartCoroutine(Timer());
                        
                    }
                    break;

                case "Green":

                    greenInd.SetActive(true);
                    greenTimer.SetActive(true);

                    if (!coroutineRuns)
                    {
                        b = StartCoroutine(Timer());
                        
                    }
                    break;
            }
        }
        else
        {
            switch (client.clientColor)
            {
                case "Red":
                    redInd.SetActive(false);
                    redTimer.SetActive(false);
                    red.click = false;

                    if (b != null && coroutineRuns)
                    {
                        StopCoroutine(b);
                        coroutineRuns = false;
                    }
                    break;
                case "Blue":

                    blueInd.SetActive(false);
                    blueTimer.SetActive(false);
                    blue.click = false;

                    if (b != null && coroutineRuns)
                    {
                        StopCoroutine(b);
                        coroutineRuns = false;
                    }
                    break;
                case "Yellow":
                    yellow.click = false;
                    yellowInd.SetActive(false);
                    yellowTimer.SetActive(false);

                    if (b != null && coroutineRuns)
                    {
                        StopCoroutine(b);
                        coroutineRuns = false;
                    }
                    break;
                case "Green":
                    green.click = false;
                    greenInd.SetActive(false);
                    greenTimer.SetActive(false);

                    if (b != null && coroutineRuns)
                    {
                        StopCoroutine(b);
                        coroutineRuns = false;
                    }
                    break;
            }
            isMyMove = false;
        }

        if(unutra == 4)
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

            if (pawn1.tag == client.clientColor)
            {
                napolju--;
                listaNapolju.Remove(pawnName);
            }

            client.readData = "";

        }

        if (client.readData.StartsWith("StartRoll")) {
            string colorDice = client.readData.Substring(client.readData.IndexOf('|') + 1);
            StartCoroutine(RollTheDice(colorDice));
            
            client.readData = "";
        }

        if (client.readData.StartsWith("Roll"))
        {

            string[] data = client.readData.Split('|');

            switch (data[2])
            {
                case "Blue":

                    blue.randomDiceSide1 = int.Parse(data[1]);
                    blue.rend.sprite = blue.diceSides[int.Parse(data[1])];
                    break;

                case "Red":

                    red.randomDiceSide1 = int.Parse(data[1]);
                    red.rend.sprite = red.diceSides[int.Parse(data[1])];
                    break;

                case "Yellow":

                    yellow.randomDiceSide1 = int.Parse(data[1]);
                    yellow.rend.sprite = yellow.diceSides[int.Parse(data[1])];
                    break;
                case "Green":

                    green.randomDiceSide1 = int.Parse(data[1]);
                    green.rend.sprite = green.diceSides[int.Parse(data[1])];
                    break;
                default: break;

            }

            client.readData = "";


        }

        if (client.readData.StartsWith("$"))
        {
            string readData = client.readData.Substring(1);
            if (readData.Contains("-"))
                readData = readData.Substring(0, readData.IndexOf('-'));

            string[] split = readData.Split('|');
            pawn = GameObject.Find(split[0]).GetComponent<Position>();


            if (pawn != null)
            {

                if (split[1].Equals("out"))
                {
                    pawn._out = true;
                    if (b != null)
                        StopCoroutine(b);
                    if (isMyMove)
                        b = StartCoroutine(Timer());

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
