using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    public GameObject[] waypoints= new GameObject[60];
    private GameObject pawn;
    private GameObject check;
    int index = 2;
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public bool out_=false;

	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        pawn = GameObject.Find("pawn");
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
            Debug.Log(randomDiceSide);
        }

        if (!out_ && (randomDiceSide + 1) == 6)
        {
            check = GameObject.Find("Waypoint (2)");
            pawn.transform.position = check.transform.position;
            out_ = true;
        }
        else
        {


            if (index + randomDiceSide + 1 <= 52 && out_)
            {
                index = index + randomDiceSide + 1;
                check = GameObject.Find("Waypoint (" + index + ")");
                pawn.transform.position = check.transform.position;
            }

        }
       
    }
}
