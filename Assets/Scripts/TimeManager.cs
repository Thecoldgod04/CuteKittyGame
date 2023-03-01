using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float time;
    public float timer;

    public int hour;

    public bool dayTime;

    public GameObject[] day;
    public GameObject[] night;

    public GameObject sun;
    public GameObject moon;
    public Transform moveDownPoint;
    public Transform moveUpPoint;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        time = 10f;
        timer = 0;
        hour = 0;
        dayTime = true;
        cam = Camera.main;

        moon.transform.position = moveDownPoint.position;
        sun.transform.position = moveUpPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= time)
        {
            hour++;
            Debug.Log(hour + " now");
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        timeUpdate();
        switchTime();

    }

    public void timeUpdate()
    {
        if (hour <= 6)
        {
            dayTime = true;
        }
        else if (hour <= 12)
        {
            dayTime = false;
        }
        else
        {
            hour = 0;
        }
    }

    public void switchTime()
    {
        if (dayTime == false)
        {
            sun.transform.position = Vector2.MoveTowards(sun.transform.position, moveDownPoint.position, 10f * Time.deltaTime);
            if(sun.transform.position == moveDownPoint.position)
            {
                foreach (GameObject dayGO in day)
                {
                    dayGO.SetActive(false);
                }
                foreach (GameObject nightGO in night)
                {
                    nightGO.SetActive(true);
                }
                moon.transform.position = Vector2.MoveTowards(moon.transform.position, moveUpPoint.position, 10f * Time.deltaTime);
                cam.backgroundColor = new Color(0, 0.2f, 0.3f);
            }
        }
        else
        {
            moon.transform.position = Vector2.MoveTowards(moon.transform.position, moveDownPoint.position, 10f * Time.deltaTime);
            if (moon.transform.position == moveDownPoint.position)
            {
                foreach (GameObject nightGO in night)
                {
                    nightGO.SetActive(false);
                }
                foreach (GameObject dayGO in day)
                {
                    dayGO.SetActive(true);
                }
                sun.transform.position = Vector2.MoveTowards(sun.transform.position, moveUpPoint.position, 10f * Time.deltaTime);
                cam.backgroundColor = new Color(0.4f, 0.7f, 0.8f);
            }
        }
    }
}
