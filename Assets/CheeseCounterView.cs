using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCounterView : MonoBehaviour
{
    public UIController UIController;

    public Text Text;

    int count = 0;
    void Update()
    {        
        Text.text = "x" + UIController.AteCheeses;
    }
}