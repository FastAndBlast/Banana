using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    public bool fadingOut = true;

    public float fade = 5;


    public static float audioLength = 1.698f;

    public float startTime;

    Image image;

    public static TransitionEffect instance;

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        image = GameObject.Find("WhiteFlash").GetComponent<Image>();
        audioSource = GameObject.Find("TransitionSoundEffect").GetComponent<AudioSource>();
        //image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!fadingOut)
        {
            return;
        }

        fade = (Time.time - startTime) / audioLength;

        fade = fade * fade * (3 - 2 * fade);

        //fade += Time.deltaTime * GameManager.timeScale;

        if (fade < 1f/3)
        {
            image.color = new Color(1, 1, 1, fade * 3f);
        }
        else
        {
            image.color = new Color(1, 1, 1, (1 - fade) * 3f);
        }

        if (Time.time - startTime > audioLength)
        {
            fadingOut = false;
        }

        /*
        if (fade < audioLength / 3)
        {
            image.color = new Color(1, 1, 1, fade / (audioLength / 3));
        }
        else
        {
            image.color = new Color(1, 1, 1, 1 - (fade - (audioLength / 3)) / (audioLength * 2 / 3));
        }
        */
    }

    public void Flash()
    {
        audioSource.Play();
        startTime = Time.time;
        fadingOut = true;
        //fade = 0;
    }

}
