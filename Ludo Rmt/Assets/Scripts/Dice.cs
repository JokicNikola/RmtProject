using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {


    private Sprite[] diceSides;
    private SpriteRenderer rend;

	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        int finalSide = 0;

        for (int i = 0; i <= 25; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        Debug.Log(finalSide);
    }
}
