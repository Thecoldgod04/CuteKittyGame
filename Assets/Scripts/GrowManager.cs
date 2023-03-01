using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowManager : MonoBehaviour
{
    public GameObject kitty;

    public Slider growBar;
    // Start is called before the first frame update
    void Start()
    {
        growBar.maxValue = 4;
        growBar.value = kitty.GetComponent<Kitty>().growProgress;
    }

    // Update is called once per frame
    void Update()
    {
        growBar.value = kitty.GetComponent<Kitty>().growProgress;
    }
}
