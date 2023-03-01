using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Transform target;
    bool lookingRight = false;
    float speed;

    //public LayerMask theCat;

    private void Start()
    {
        target = FindObjectOfType<Kitty>().sleepPlace;
        speed = Random.Range(4f, 6f);
    }

    void Update()
    {
        if(FindObjectOfType<TimeManager>().hour == 0)
        {
            die();
        }
        if (transform.position.x < target.position.x && lookingRight == false)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = true;
        }
        else if (transform.position.x > target.position.x && lookingRight == true)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    float time = 1f;
    float timer = 0;
    int ran;
    void moan()
    {
        if(timer >= time)
        {
            ran = (int)Random.Range(1, 3);
            if (ran == 1)
            {
                float pitchRan = Random.Range(1f, 1.5f);
                FindObjectOfType<SoundManager>().ghostSound.pitch = pitchRan;
                FindObjectOfType<SoundManager>().playGhostSound();
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "the cat")
        {
            FindObjectOfType<Kitty>().hurt();
            die();
        }
    }

    void die()
    {
        FindObjectOfType<SoundManager>().playGhostDieSound();
        FindObjectOfType<Kitty>().catHurtSound2.Play();
        FindObjectOfType<DieParticleManager>().spawnParticle(transform.position);
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        die();
    }
}
