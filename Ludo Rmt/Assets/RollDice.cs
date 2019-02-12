using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int turn = 1;
    private bool coroutineAllowed = true;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("Objects/Dice/");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
