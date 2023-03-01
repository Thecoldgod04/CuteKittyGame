using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float winTime = 2f;
    float winTimer = 0;

    float loseTime = 0.5f;
    float loseTimer = 0;
    void Update()
    {
        if(FindObjectOfType<Kitty>().level == 3)
        {
            if (winTimer >= winTime)
            {
                SceneManager.LoadScene("GameWin");
            }
            else
            {
                winTimer += Time.deltaTime;
            }
        }
        
        if(FindObjectOfType<Kitty>().happinese == 0)
        {
            if (loseTimer >= loseTime)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                loseTimer += Time.deltaTime;
            }
        }
    }
}
