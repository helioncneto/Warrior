using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public int myInt = 10;
    public int[] myArray = new int[5];
    
    // Start is called before the first frame update
    // funções de uma linha podem ser execitadas como: void func() => ação
    void Start()
    {
        print("Hello World");

        GameObject myPlayer = GameObject.FindWithTag("Player");
        
        for (int i = 0; i < myArray.Length; i++)
        {
            print("O numero é " + myArray[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void print(string message)
    {
        Debug.Log(message);
    }

    bool isEven(int num)
    {
        if (num % 2 == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
