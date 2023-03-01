using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource eatSound;
    public GameObject sleepSound;
    public AudioSource growSound;
    public AudioSource drinkSound;
    public AudioSource ghostSound;
    public AudioSource ghostDieSound;

    public void playEatSound()
    {
        eatSound.Play();
    }

    public void playGrowSound()
    {
        growSound.Play();
    }

    public void playDrinkSound()
    {
        drinkSound.Play();
    }

    public void playGhostSound()
    {
        ghostSound.Play();
    }
    public void playGhostDieSound()
    {
        ghostDieSound.Play();
    }

    public void playSleepSound()
    {
        sleepSound.GetComponent<AudioSource>().volume = 0.2f;
    }
    public void stopSleepSound()
    {
        sleepSound.GetComponent<AudioSource>().volume = 0;
    }
}
