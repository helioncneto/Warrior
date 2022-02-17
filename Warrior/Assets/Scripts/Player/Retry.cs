using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public GameObject initialPosition;
    
    public void RestartPlay()
    {
        gameObject.transform.position = initialPosition.transform.position;
    }
}
