using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.1f, 9f)]
    [SerializeField]
    float sensitivity;

    //[Range(0, 360)]
    [SerializeField]
    float yRotationLock = 88f;


    private void Update()
    {
        Vector3 cameraAngles = transform.eulerAngles;

        //cameraAngles.x -= Input.GetAxis("Mouse Y") * sensitivity;

        if (cameraAngles.x - yRotationLock > 10)
        {
            cameraAngles.x -= 360;
        }
        //else if (cameraAngles.x < -yRotationLock)
        //{
        //    cameraAngles.x += 360;
        //}

        cameraAngles.x = Mathf.Clamp(cameraAngles.x - Input.GetAxis("Mouse Y") * sensitivity, -yRotationLock, yRotationLock);
        
        

        print(Mathf.Clamp(cameraAngles.x - Input.GetAxis("Mouse Y") * sensitivity, -yRotationLock, yRotationLock));


        transform.eulerAngles = cameraAngles;

        // --------

        Vector3 playerAngles = transform.parent.eulerAngles;

        playerAngles.y += Input.GetAxis("Mouse X") * sensitivity;

        transform.parent.eulerAngles = playerAngles;




        //rotation.x += Input.GetAxis(xAxis) * sensitivity;
        //rotation.x += Input.GetAxis(xAxis) * sensitivity;



    }
}
