using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    bool opened = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!opened)
        {
            Application.OpenURL("https://www.youtube.com/watch?v=R9M4htrogqA&ab_channel=%D0%94%D0%BC%D0%B8%D1%82%D1%80%D0%B8%D0%B9%D0%A0%D1%8F%D0%B7%D0%B0%D0%BD%D1%86%D0%BE%D0%B2");
            opened = true;
        }
    }


}
