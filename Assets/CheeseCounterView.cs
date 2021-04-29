using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCounterView : MonoBehaviour
{
    public Player Player;

    public Text Text;

    int count = 0;
    void Update()
    {
        if (Player)
        {
            count = Player.ateCheeses;
        }
        Text.text = "x" + count;
    }
}