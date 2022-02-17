using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    public GameObject prefab;
    public int instantiateGap = 5;
    public int amount = 10;


    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
        InvokeRepeating("GetEnemyFromPool", 1f, instantiateGap);

    }

    void InitializePool()
    {
        for(int i = 0; i < amount; i++)
        {
            AddPrefabToPool();
        }
    }

    void AddPrefabToPool()
    {
        GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);
    }

    GameObject GetEnemyFromPool()
    {
        GameObject enemy = null;

        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                enemy = transform.GetChild(i).gameObject;
                break;
            }
        }

        if(enemy == null)
        {
            AddPrefabToPool();
            enemy = transform.GetChild(transform.childCount - 1).gameObject;
        }

        enemy.transform.position = this.transform.position;
        enemy.SetActive(true);
        return enemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
