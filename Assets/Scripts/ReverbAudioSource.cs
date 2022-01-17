using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbAudioSource : MonoBehaviour
{
    public float reverbDuration = 4.1f;

    public GameObject audioSourcePrefab;

    public float volume
    {
        get
        {
            return vol;
        }
        set
        {
            currentAudioSource.volume = value;
            vol = value;
        }
    }

    [SerializeField]
    float vol = 1f;

    [HideInInspector]
    public AudioSource currentAudioSource;

    private void Start()
    {
        GameObject newAudioSource = Instantiate(audioSourcePrefab);
        newAudioSource.transform.parent = gameObject.transform;
        newAudioSource.transform.localPosition = Vector3.zero;
        currentAudioSource = newAudioSource.GetComponent<AudioSource>();
        currentAudioSource.volume = vol;
    }

    public void Update()
    {
        if (currentAudioSource.time > currentAudioSource.clip.length - reverbDuration)
        {
            Destroy(currentAudioSource.gameObject, reverbDuration + 1);
            GameObject newAudioSource = Instantiate(audioSourcePrefab);
            newAudioSource.transform.parent = gameObject.transform;
            newAudioSource.transform.localPosition = Vector3.zero;
            currentAudioSource = newAudioSource.GetComponent<AudioSource>();
            currentAudioSource.volume = vol;
        }
    }
}
