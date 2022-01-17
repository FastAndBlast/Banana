using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { TOREAL, STABLE, TOFAKE};

public class MusicManager : MonoBehaviour
{
    public ReverbAudioSource fakeMusic;

    public List<ReverbAudioSource> realMusic = new List<ReverbAudioSource>();

    public State transitionState = State.STABLE;

    public float transitionTime;

    public static MusicManager instance;

    public ReverbAudioSource actualFakeMusic;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
//fakeMusic.time = fakeMusic.clip.length - 4;

        foreach (GameObject musicSource in GameObject.FindGameObjectsWithTag("Music"))
        {
            realMusic.Add(musicSource.GetComponent<ReverbAudioSource>());
            //musicSource.GetComponent<AudioSource>().time = musicSource.GetComponent<AudioSource>().clip.length - 4;
        }

        //SetNearEnd();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetNearEnd();
        }

        if (transitionState == State.STABLE)
        {
            return;
        }

        if (transitionTime < TransitionEffect.audioLength)
        {
            transitionTime += Time.deltaTime * GameManager.timeScale;

            if (transitionTime > TransitionEffect.audioLength)
            {
                transitionTime = TransitionEffect.audioLength;
            }
        }
        else
        {
            if (transitionState == State.TOFAKE)
            {
                GameManager.instance.realCamera.GetComponent<AudioListener>().enabled = false;
                GameManager.instance.fakeCamera.GetComponent<AudioListener>().enabled = true;
            }

            transitionState = State.STABLE;
            transitionTime = 0;
            return;
        }

        float fadingOut = 1 - Mathf.Exp(1.5f * (3 * transitionTime / 1.698f - 1)) * 3 * transitionTime / 1.698f;
        float fadingIn = 1 - Mathf.Exp(-1.5f * (3 * transitionTime / (2 * 1.698f) - 0.5f)) * (1.5f - 3 * transitionTime / (2 * 1.698f));

        if (transitionState == State.TOREAL)
        {
            fakeMusic.volume = fadingOut;
            foreach (ReverbAudioSource source in realMusic)
            {
                source.volume = fadingIn;
            }
        }
        else if (transitionState == State.TOFAKE)
        {
            fakeMusic.volume = fadingIn;
            foreach (ReverbAudioSource source in realMusic)
            {
                source.volume = fadingOut;
            }
        }
    }



    public void SetNearEnd()
    {
        fakeMusic.currentAudioSource.time = fakeMusic.currentAudioSource.clip.length - 10;

        actualFakeMusic.currentAudioSource.time = actualFakeMusic.currentAudioSource.clip.length - 10;

        foreach (ReverbAudioSource source in realMusic)
        {
            source.currentAudioSource.time = source.currentAudioSource.clip.length - 10;
        }
    }

    public void Switch()
    {
        GameManager.instance.realCamera.GetComponent<AudioListener>().enabled = true;
        GameManager.instance.fakeCamera.GetComponent<AudioListener>().enabled = false;
    }

    public void Real()
    {
        Switch();
        transitionState = State.TOREAL;
    }

    public void Fake()
    {
        Switch();
        transitionState = State.TOFAKE;
    }



}
