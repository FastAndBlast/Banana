using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float cooldown = 0.5f;
    float cooldownCurrent;

    public float forceStrength = 3000;

    public Vector3 forceDirection = Vector3.up;

    public float velocityMultiplier = 2f;

    //public bool accountForObjectRotation;

    private void Update()
    {
        if (cooldownCurrent > 0)
        {
            cooldownCurrent -= Time.deltaTime * GameManager.timeScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && cooldownCurrent <= 0)
        {
            other.attachedRigidbody.velocity *= velocityMultiplier;
            other.attachedRigidbody.AddForce(forceDirection * forceStrength);
            cooldownCurrent = cooldown;
        }
    }
}
