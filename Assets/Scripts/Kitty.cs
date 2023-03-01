using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitty : MonoBehaviour
{
    public int happinese;
    public int growProgress = 0;
    public int level = 0;

    public ParticleSystem growEffect;
    public ParticleSystem happyEffect;

    Animator anim;
    [SerializeField] private GameObject kitty;
    public Animator camAnim;

    public AudioSource meowSound;

    Vector2 destination;

    public GameObject speechBubble;
    public Sprite[] needs;
    public GameObject wanting;

    public bool isHungry, eaten;
    public bool isThirsty, drunk;
    public bool isSleepy, slept;
    public bool wantsToPlay, played;

    public Transform sleepPlace;

    public Sprite[] secretWords;
    public GameObject word;
    public GameObject wordBubble;

    // Start is called before the first frame update
    void Start()
    {
        anim = kitty.GetComponent<Animator>();
        anim.SetBool("isRunning", true);

        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        destination = new Vector2(posX, posY);

        wordBubble.SetActive(false);
        speechBubble.SetActive(false);

        isHungry = false;       eaten = false;
        isThirsty = false;      drunk = false;
        isSleepy = false;       slept = false;
        wantsToPlay = false;    played = false;

        happinese = 8;
    }

    int nextLevel = 1;
    float sayDeadline = 1.5f;
    float saidFor = 0;

    public float scale;
    // Update is called once per frame
    void Update()
    {
        wantingCheck();
        if (isSleepy != true)
        {
            walk();

            meow();

            play();

            wakeUp();
        }

        scale = transform.localScale.x / Mathf.Abs(transform.localScale.x);
        float xScale = word.transform.localScale.x;
        float yScale = word.transform.localScale.y;
        word.transform.localScale = new Vector3(Mathf.Abs(xScale) * (-scale), yScale, 1);
        if (isSleepy == true)
        {
            sleep();
        }

        if (level == nextLevel && level == 1)
        {
            //Say "I"
            wordBubble.SetActive(true);
            word.GetComponent<SpriteRenderer>().sprite = secretWords[0];
            if (saidFor >= sayDeadline)
            {
                wordBubble.SetActive(false);
                nextLevel++;
                saidFor = 0;
            }
            else
            {
                saidFor += Time.deltaTime;
            }
        }
        else if (level == nextLevel && level == 2)
        {
            //Say "Love"
            wordBubble.SetActive(true);
            word.GetComponent<SpriteRenderer>().sprite = secretWords[1];
            if (saidFor >= sayDeadline)
            {
                wordBubble.SetActive(false);
                nextLevel++;
                saidFor = 0;
            }
            else
            {
                saidFor += Time.deltaTime;
            }
        }
        else if(level == nextLevel && level == 3)
        {
            //Say "You"
            wordBubble.SetActive(true);
            word.GetComponent<SpriteRenderer>().sprite = secretWords[2];
            if (saidFor >= sayDeadline)
            {
                wordBubble.SetActive(false);
                nextLevel++;
                saidFor = 0;
            }
            else
            {
                saidFor += Time.deltaTime;
            }
        }
    }

    public void hungry()
    {
        if (FindObjectOfType<TimeManager>().hour == 1 || FindObjectOfType<TimeManager>().hour == 4 || FindObjectOfType<TimeManager>().hour == 7)
        {
            if(eaten != true)
            {
                isHungry = true;
            }
        }
        if (FindObjectOfType<TimeManager>().hour == 2 || FindObjectOfType<TimeManager>().hour == 5 || FindObjectOfType<TimeManager>().hour == 8)
        {
            if (eaten != false)
            {
                eaten = false;
            }
        }
    }
    public void eat()
    {
        if (isHungry != true) return;

        growProgress++;
        isHungry = false;
        eaten = true;

        if (transform.localScale.x != 1 && transform.localScale.x != -1)
        {
            FindObjectOfType<SoundManager>().playEatSound();
            happy();
        }
        if (growProgress >= 4)
        {
            grow();
            if (transform.localScale.x != 1 && transform.localScale.x != -1)
            {
                growProgress = 0;
            }
        }
    }
    public void grow()
    {
        if (transform.localScale.x == 1 || transform.localScale.x == -1)
        {
            Debug.Log("Grown full size!");
            return;
        }
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;
        if(xScale < 0)
        {
            transform.localScale = new Vector3(xScale - 0.2f, yScale + 0.2f, 1);
            FindObjectOfType<SoundManager>().playGrowSound();
            level++;
            growEffect.Play();
        }   
        else if(xScale > 0)
        {
            transform.localScale = new Vector3(xScale + 0.2f, yScale + 0.2f, 1);
            FindObjectOfType<SoundManager>().playGrowSound();
            level++;
            growEffect.Play();
        }
    }

    public void sleepy()
    {
        if (FindObjectOfType<TimeManager>().hour >= 9)
        {
            isSleepy = true;
        }
        else
        {
            isSleepy = false;
        }
    }
    public Sprite sleepingSprite;
    public Sprite awakeSprite;
    public void sleep()
    {
        if (transform.position.x < sleepPlace.position.x && lookingRight == false)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = true;
        }
        else if (transform.position.x > sleepPlace.position.x && lookingRight == true)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, sleepPlace.position, 5f * Time.deltaTime);
        anim.SetBool("isRunning", true);
        if (transform.position.x == sleepPlace.position.x)
        {
            anim.SetBool("isRunning", false);
            FindObjectOfType<SoundManager>().playSleepSound();
            kitty.GetComponent<SpriteRenderer>().sprite = sleepingSprite;
        }
    }
    public void wakeUp()
    {
        FindObjectOfType<SoundManager>().stopSleepSound();
        kitty.GetComponent<SpriteRenderer>().sprite = awakeSprite;
    }

    public void thirsty()
    {
        if (FindObjectOfType<TimeManager>().hour == 2 || FindObjectOfType<TimeManager>().hour == 4 || FindObjectOfType<TimeManager>().hour == 6 || FindObjectOfType<TimeManager>().hour == 8)
        {
            if (drunk != true)
            {
                isThirsty = true;
            }
        }
        if (FindObjectOfType<TimeManager>().hour == 3 || FindObjectOfType<TimeManager>().hour == 5 || FindObjectOfType<TimeManager>().hour == 7 || FindObjectOfType<TimeManager>().hour == 9)
        {
            if (drunk != false)
            {
                drunk = false;
            }
        }
    }
    public void drink()
    {
        if (isThirsty != true) return;

        FindObjectOfType<SoundManager>().playDrinkSound();
        isThirsty = false;
        drunk = true;
    }

    float minX = -10f;
    float maxX = 10f;
    float minY = -3.8f;
    float maxY = -0.5f;

    public float stayTime = 3f;
    public float stayedFor = 0;

    public bool lookingRight = false;
    public void walk()
    {
        if (isPlaying == true) return;

        if(transform.position.x < destination.x && lookingRight == false)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = true;
        }
        else if(transform.position.x > destination.x && lookingRight == true)
        {
            float xScale = transform.localScale.x;
            float yScale = transform.localScale.y;
            transform.localScale = new Vector3(-xScale, yScale, 1);
            lookingRight = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, 5f * Time.deltaTime);
        if (transform.position == (Vector3)destination)
        {
            anim.SetBool("isRunning", false);
            if (stayedFor >= stayTime)
            {
                float posX = Random.Range(minX, maxX);
                float posY = Random.Range(minY, maxY);
                destination = new Vector2(posX, posY);

                anim.SetBool("isRunning", true);

                stayTime = Random.Range(3, 6);
                stayedFor = 0;

            }
            else
            {
                stayedFor += Time.deltaTime;
            }
        }
        
    }

    public float ranNow = 2f;
    public float wait = 0;
    public int ran;
    public void meow()
    {
        if (wait >= ranNow)
        {
            ran = (int) Random.Range(1, 6);
            if (ran == 2)
            {
                float pitchRan = Random.Range(0.5f, 1.5f);
                meowSound.pitch = pitchRan;
                meowSound.Play();
            }
            wait = 0;
        }
        else
        {
            wait += Time.deltaTime;
        }
                
    }


    public void wantingCheck()
    {
        hungry();
        thirsty();
        sleepy();
        wantToPlay();
        if(isHungry == true || isThirsty == true || wantsToPlay == true || isSleepy == true)
        {
            speechBubble.SetActive(true);

            if (isThirsty == true)
            {
                wanting.GetComponent<SpriteRenderer>().sprite = needs[1];
                sadCountDown(2);
            }
            else if (isHungry == true)
            {
                wanting.GetComponent<SpriteRenderer>().sprite = needs[0];
                sadCountDown(1);
            }
            else if (wantsToPlay == true)
            {
                wanting.GetComponent<SpriteRenderer>().sprite = needs[2];
            }

            if (isSleepy == true)
            {
                wanting.GetComponent<SpriteRenderer>().sprite = needs[3];
            }
        }
        else
        {
            speechBubble.SetActive(false);
            resetSadCountDown();
        }
    }

    public void wantToPlay()
    {
        if (FindObjectOfType<TimeManager>().hour == 5)
        {
            if (played != true)
            {
                wantsToPlay = true;
            }
        }
        else
        {
            playedFor = 0;
            wantsToPlay = false;
            isPlaying = false;
            isPressed = false;
        }
    }
    public bool isPressed;
    public LayerMask theKitty;
    float playTime = 3f;
    float playedFor = 0;

    bool isPlaying;
    public void play()
    {
        if (wantsToPlay != true)
        {
            FindObjectOfType<SoundManager>().stopSleepSound();
            return;
        }

            if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }

        if(isPressed == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Physics2D.OverlapPoint(mousePos, theKitty))
            {
                isPlaying = true;
                Debug.Log("Mouse is holding in the cat!");

                anim.SetBool("isRunning", false);
                FindObjectOfType<SoundManager>().playSleepSound();

                if (playedFor >= playTime)
                {
                    happy();
                    happy();
                    happyEffect.Play();

                    wantsToPlay = false;
                    played = true;
                    isPlaying = false;
                    playedFor = 0;
                }
                else 
                {
                    playedFor += Time.deltaTime;
                }
            }
        }
        if(isPressed == false)
        {
            isPlaying = false;
            playedFor = 0;
        }
    }

    public void happy()
    {
        if (happinese < 12)
        {
            happinese++;
            if(happinese == 5 || happinese == 9 || happinese == 11)
            {
                happyEffect.Play();
            }
        }
    }

    public void sad(int n)
    {
        if (happinese > 0)
        {
            happinese -= n;
        }
    }
    float sadDeadline = 5f;
    public float waited = 0;
    public void sadCountDown(int n)
    {
        if(waited >= sadDeadline)
        {
            sad(n);
            Debug.LogError("Sad: " + happinese);
            waited = 0;
        }
        else
        {
            waited += Time.deltaTime;
        }
    }
    public void resetSadCountDown()
    {
        waited = 0;
    }

    public AudioSource catHurtSound;
    public AudioSource catHurtSound2;
    public void hurt()
    {
        happinese--;
        camAnim.SetTrigger("shake");
        //catHurtSound2.Play();
        catHurtSound.Play();
    }
}
