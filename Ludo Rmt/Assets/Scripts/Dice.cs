using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

   
    private GameObject check;
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public bool out_=false;
    public int randomDiceSide;
    public int randomDiceSide1=0;
    public  bool click;


    public void urad()
    {

    }


    private void Start () {
        click = false;
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
       
    }

    

    private void OnMouseDown()
    {
        if (!click)
        {
            click = true;
            StartCoroutine("RollTheDice");
        } else
        {
           // click = false;
        }
       

    }


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
        randomDiceSide1 = Random.Range(5, 6);
        rend.sprite = diceSides[randomDiceSide1];
        




    }
}
