using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int buildingAmount = 50;

    public float safeZoneSize = 50f;

    public GameObject buildingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buildingAmount; i++)
        {
            Vector3 pos = Vector3.zero;
            while (pos.magnitude < safeZoneSize)
            {
                pos = new Vector3(Random.Range(-500, 500), 0, Random.Range(-500, 500));
            }

            pos.y = Random.Range(-25, 25) - 100;

            pos += transform.position;

            GameObject building = Instantiate(buildingPrefab);
            building.transform.position = pos;

        }
    }
}
