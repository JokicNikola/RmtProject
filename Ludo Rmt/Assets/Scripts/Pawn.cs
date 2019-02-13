using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    private GameObject dice;
    private GameObject check;
    int randomDiceSide1=0;
    int index = 2;
    private bool out_ = false;

    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.Find("Side6");
    }

    // Update is called once per frame
    void Update()
    {
        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (2)");
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

        randomDiceSide1 = dice.GetComponent<Dice>().randomDiceSide1;

        if (!out_ && (randomDiceSide1 + 1) == 6)
        {
            check = GameObject.Find("Waypoint (2)");
            transform.position = check.transform.position;
            out_ = true;
        }
        else
        {

            if ((index + randomDiceSide1 + 1) < 53 && out_)
            {
                for (int i = 0; i < randomDiceSide1 + 1; i++)
                {
                    index++;
                    yield return new WaitForSeconds(12f * Time.deltaTime);
                }
            }
        }
        
    }


    }
