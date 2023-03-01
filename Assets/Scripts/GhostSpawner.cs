using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    //Spawn each 3 secs
    // 1:6 lv1
    // 1:5 lv2
    // 1:3 lv3

    public GameObject ghostPrefab;

    float time = 3f;
    float count = 0;
    void Update()
    {
        if(FindObjectOfType<TimeManager>().hour >= 9 && FindObjectOfType<TimeManager>().hour <= 12)
        {
            if (count >= time)
            {
                if (FindObjectOfType<Kitty>().level == 0)
                {
                    randomSpawn(8);
                }
                else if (FindObjectOfType<Kitty>().level == 1)
                {
                    randomSpawn(7);
                }
                else if (FindObjectOfType<Kitty>().level == 2)
                {
                    randomSpawn(5);
                }
                count = 0;
            }
            else
            {
                count += Time.deltaTime;
            }
        }   
    }

    void randomSpawn(int range)
    {
        int ran = Random.Range(1, range);
        if(ran == 1)
        {
            Instantiate(ghostPrefab, transform.position, Quaternion.identity);
            float pitchRan = Random.Range(1f, 1.5f);
            FindObjectOfType<SoundManager>().ghostSound.pitch = pitchRan;
            FindObjectOfType<SoundManager>().playGhostSound();
        }
    }
}
