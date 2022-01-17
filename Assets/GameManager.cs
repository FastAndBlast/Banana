using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reality { REAL, FAKE }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static float timeScale = 1f;

    [HideInInspector]
    public Camera realCamera;
    [HideInInspector]
    public Camera fakeCamera;

    public GameObject fakePlayer;
    public GameObject realPlayer;

    public static Reality reality = Reality.REAL;

    float switchDelay = TransitionEffect.audioLength * 0.386984687868f;

    public float timeUntilSwitch;

    public float switchCooldown = 2f;
    float switchCooldownTime;

    Vector3 startingPos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        realCamera = Camera.main;
        fakeCamera = GameObject.Find("SecondCamera").GetComponent<Camera>();

        startingPos = realPlayer.transform.position;
        //cameras.Add(Camera.main);
        //cameras.Add(GameObject.Find("SecondCamera").GetComponent<Camera>());
    }

    private void Update()
    {
        if (switchCooldownTime > 0)
        {
            switchCooldownTime -= Time.deltaTime;
        }
        else if (Input.GetButtonDown("Switch"))
        {
            Switch();
        }

        if (timeUntilSwitch > 0)
        {
            timeUntilSwitch -= Time.deltaTime * timeScale;
            if (timeUntilSwitch <= 0)
            {
                DelayedSwitch();
            }
        }
    }

    public void Switch()
    {
        timeUntilSwitch = switchDelay;
        TransitionEffect.instance.Flash();

        if (reality == Reality.REAL)
        {
            MusicManager.instance.Fake();
        }
        else
        {
            MusicManager.instance.Real();
        }

        switchCooldownTime = switchCooldown;
    }

    public void DelayedSwitch()
    {
        if (reality == Reality.REAL)
        {
            Fake();
        }
        else
        {
            Real();
        }
    }

    public void Respawn()
    {
        TransitionEffect.instance.Flash();
        realPlayer.transform.position = startingPos;
        fakePlayer.transform.position = startingPos + Vector3.right * 1000;
    }

    public void Real()
    {
        reality = Reality.REAL;

        realCamera.enabled = true;
        fakeCamera.enabled = false;

        realCamera.transform.localRotation = fakeCamera.transform.localRotation;

        realPlayer.GetComponent<PlayerController>().on = true;
        fakePlayer.GetComponent<PlayerController>().on = false;

        realPlayer.transform.position = fakePlayer.transform.position - Vector3.right * 1000;
        realPlayer.transform.rotation = fakePlayer.transform.rotation;

        /*
        cameras[0].tag = "MainCamera";
        cameras[0].enabled = true;
        cameras[1].tag = "Camera";
        cameras[1].enabled = false;
        */

        //Set position of the player properly as well as turn off the other one
    }

    public void Fake()
    {
        reality = Reality.FAKE;

        realCamera.enabled = false;
        fakeCamera.enabled = true;

        fakeCamera.transform.localRotation = realCamera.transform.localRotation;

        realPlayer.GetComponent<PlayerController>().on = false;
        fakePlayer.GetComponent<PlayerController>().on = true;

        fakePlayer.transform.position = realPlayer.transform.position + Vector3.right * 1000;
        fakePlayer.transform.rotation = realPlayer.transform.rotation;

        /*
        cameras[0].tag = "Camera";
        cameras[0].enabled = false;
        cameras[1].tag = "MainCamera";
        cameras[1].enabled = true;
        */

        //Set position of the player properly as well as turn off the other one
    }
}
