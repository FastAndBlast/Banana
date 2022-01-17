using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && collision.gameObject.GetComponent<PlayerController>().on)
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }



}
