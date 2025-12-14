using UnityEngine;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
     public GameObject prefap;

    private List<GameObject> pool;
    private int poolSize=5;
    void Start()
    {
        CreteGameObjectsPool();
    }


    private void CreteGameObjectsPool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateGameOpject();
        }

    }

    private GameObject CreateGameOpject()
    {
        GameObject obj = Instantiate(prefap, transform);
        obj.SetActive(false);
        pool.Add(obj);
       
        return obj;

    }
    public GameObject GetPoolGameObjects()
    {
        foreach(GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateGameOpject();

    }


}
