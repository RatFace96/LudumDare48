using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCounterView : MonoBehaviour
{
    public Player Player;

    public Text Text;
    void Update()
    {
        if (Player)
        {
            Text.text = "x" + Player.ateCheeses;
        }
    }
}
