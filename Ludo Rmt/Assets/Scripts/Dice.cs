using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    public GameObject[] waypoints= new GameObject[60];
    private GameObject pawn;
    private GameObject check;
    int index = 0;
    private Sprite[] diceSides;
    private SpriteRenderer rend;

	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        pawn = GameObject.Find("pawn");
        check = GameObject.Find("Waypoint (2)");
        pawn.transform.position = check.transform.position;
        
         
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        

        for (int i = 0; i <= 25; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }


        if (index + randomDiceSide + 1 < 52)
        {
            index = index + randomDiceSide + 1;
            check = GameObject.Find("Waypoint (" + index + ")");
            pawn.transform.position = check.transform.position;
        }

        
       
    }
}
