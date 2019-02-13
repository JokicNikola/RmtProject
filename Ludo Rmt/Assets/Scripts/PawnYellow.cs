using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYellow : MonoBehaviour
{
    private GameObject dice;
    private GameObject check;
    int randomDiceSide1 = 0;
    int index = 28;
    private bool out_ = false;
    bool click;

    // Start is called before the first frame update
    void Start()
    { 
        dice = GameObject.Find("Side6 (1)");
    }

    // Update is called once per frame
    void Update()
    {
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (28)");
            transform.position = check.transform.position;
        }
        else
        {

            if (out_)
            {
                check = GameObject.Find("Waypoint (" + index + ")");
                transform.position = Vector3.MoveTowards(transform.position, check.transform.position, 3f * Time.deltaTime);
            }

        }
    }
    private void OnMouseDown()
    {
        StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        click = dice.GetComponent<Dice>().click;
        
        Dice dc = dice.GetComponent<Dice>();
        






        if (click)
        {
            randomDiceSide1 = dice.GetComponent<Dice>().randomDiceSide1;
        }
        else
        {
            randomDiceSide1 = -1;
        }


        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (28)");
            transform.position = check.transform.position;
            out_ = true;
            dc.click = false;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 96 && out_)
            {
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                    if (index == 52)
                    {
                        index = 0;
                    }
                    if (index == 26)
                    {
                        index = 89;
                    }
                }
                dc.click = false;
            }
        }
    }
}
