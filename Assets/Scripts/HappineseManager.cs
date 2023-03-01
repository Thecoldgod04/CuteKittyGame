using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappineseManager : MonoBehaviour
{
    public GameObject kitty;

    public Slider happineseBar;

    public GameObject fill;

    public Sprite[] emotionStates;

    public GameObject currentStateObject;
    public Image currentState;

    // Start is called before the first frame update
    void Start()
    {
        happineseBar.maxValue = 12;
        happineseBar.value = kitty.GetComponent<Kitty>().happinese;
        currentState = currentStateObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        happineseBar.value = kitty.GetComponent<Kitty>().happinese;
        if(happineseBar.value <= 4)
        {
            //Red
            fill.GetComponent<Image>().color = new Color(0.85f, 0.36f, 0.32f);
            currentState.sprite = emotionStates[0];
        }
        else if(happineseBar.value <= 8)
        {
            //Green
            fill.GetComponent<Image>().color = new Color(0.43f, 0.76f, 0.46f);
            currentState.sprite = emotionStates[1];
        }
        else if(happineseBar.value <= 12)
        {
            //Blue
            fill.GetComponent<Image>().color = new Color(0.27f, 0.74f, 0.77f);
            currentState.sprite = emotionStates[2];
        }
    }
}
