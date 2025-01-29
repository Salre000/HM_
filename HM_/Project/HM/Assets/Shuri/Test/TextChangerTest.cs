using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static InputManager;

public class TextChangerTest : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] TextMeshProUGUI[] text;

    [SerializeField] Image[] lamps = new Image[(int)InputKeys.Max];

    private void Start()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            text[i].text = inputManager.keys[i].keyName;
        }
        //text[0].text = inputManager.config.a.keyName;
        //text[1].text = inputManager.config.b.keyName;
        //text[2].text = inputManager.config.x.keyName;
        //text[3].text = inputManager.config.y.keyName;
        //text[4].text = inputManager.config.rt.keyName;
        //text[5].text = inputManager.config.lt.keyName;
        //text[6].text = inputManager.config.rb.keyName;
        //text[7].text = inputManager.config.lb.keyName;
        //text[8].text = inputManager.config.right.keyName;
        //text[9].text = inputManager.config.left.keyName;
        //text[10].text = inputManager.config.up.keyName;
        //text[11].text = inputManager.config.down.keyName;

    }

    private void Update()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            lamps[i].color = Color.white;
            if (inputManager.keys[i].type == KeyType.None) { text[i].text = ""; continue; }
            text[i].text = inputManager.keys[i].keyName.ToString();
        }
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (inputManager.keys[i].keyName == null || inputManager.keys[i].keyName == "") continue;
            if (inputManager.IsOnButton(inputManager.keys[i])) lamps[i].color = Color.red;
        }
        //    if (inputManager.IsOnButton(inputManager.config.a)) lamps[(int)Button.A].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.b)) lamps[(int)Button.B].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.x)) lamps[(int)Button.X].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.y)) lamps[(int)Button.Y].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.rt)) lamps[(int)Button.RT].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.lt)) lamps[(int)Button.LT].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.rb)) lamps[(int)Button.RB].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.lb)) lamps[(int)Button.LB].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.right)) lamps[(int)Button.Right].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.left)) lamps[(int)Button.Left].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.up)) lamps[(int)Button.Up].color = Color.red;

        //if (inputManager.IsOnButton(inputManager.config.down)) lamps[(int)Button.Down].color = Color.red;

    }
}
