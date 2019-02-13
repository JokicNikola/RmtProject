using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

   
    private GameObject pawn;
    private GameObject check;
    int index = 2;
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public bool out_=false;
    public int randomDiceSide;
    public int randomDiceSide1=0;


    private void Start () {
        rend = GetComponent<SpriteRenderer>();
        //pawn = GameObject.Find("pawn");
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

   /* private void Update()
    {
       
        if (!out_ && (randomDiceSide1 + 1) == 6){
            check = GameObject.Find("Waypoint (2)");
            pawn.transform.position = check.transform.position;
        }
        else
        {

            if ( out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                pawn.transform.position = Vector3.MoveTowards(pawn.transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        }
    }*/

    private IEnumerator RollTheDice()
    {
        randomDiceSide = 0;
        randomDiceSide1 = 0;



        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
            
        }
        randomDiceSide1 = Random.Range(0, 6);
        rend.sprite = diceSides[randomDiceSide1];
        

 /*       if (!out_ && (randomDiceSide1 + 1) == 6)
       {
            check = GameObject.Find("Waypoint (2)");
            pawn.transform.position = check.transform.position;
            out_ = true;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 53 && out_)
            {
                for(int i=0;i< randomDiceSide1 + 1; i++)
                {
                    index++;
                    yield return new WaitForSeconds(12f*Time.deltaTime);
                } 
            }
        }
        */
    }
}
