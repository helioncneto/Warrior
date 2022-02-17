using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform location;
    public float livingTime;


    public void Instantiate()
    {
        GameObject instantiateObject = Instantiate(prefab, location.position, Quaternion.identity) as GameObject;

        if (livingTime > 0f)
        {
            Destroy(instantiateObject, livingTime);
        }

    }
}
