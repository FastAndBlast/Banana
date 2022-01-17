using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AberrationEffect : MonoBehaviour
{
    Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (volume.profile.TryGet<ChromaticAberration>(out var effect))
        {
            if (GameManager.reality == Reality.REAL)
            {
                effect.intensity.value = 0f;
            }
            else
            {
                effect.intensity.value = 0.5f + Mathf.Sin(Time.time * 2) / 2;
            }
        }
    }




}
