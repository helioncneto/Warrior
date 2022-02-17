using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse buttom Fown");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse buttom is pressing");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse buttom is released");
        }

        // Keyboard

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump using Key Code, Not recommended");
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump using Key Code, is the recommendation");
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            Debug.Log("O valor do eixo horizontal é " + horizontal);
        }

        if (vertical != 0)
        {
            Debug.Log("O valor do eixo vertical é " + vertical);
        }
    }
}
