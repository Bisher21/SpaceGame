using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform minPos;
    [SerializeField]
    private Transform MaxPos;

     private int waveNum;
    [SerializeField] private List<Wave> waves;


    [System.Serializable]
    public class Wave
    {

        public ObjectPooler pool;
        public float spawnTimer;
        
        public float spawnInrevale;

        public int objectsPerWave;
        public int spawnedObjectCounter;
    }


    void Update()
    {
       waves[waveNum].spawnTimer -= GameManager.Instance.adjustedworldSpeed;
        if (waves[waveNum].spawnTimer <=0)
        {
            waves[waveNum].spawnTimer += waves[waveNum].spawnInrevale;
            SpawnObject();
        }
        if (waves[waveNum].spawnedObjectCounter >= waves[waveNum].objectsPerWave)
        {
            waves[waveNum].spawnedObjectCounter = 0;
            waveNum++;
            if (waveNum >= waves.Count)
            {
                waveNum = 0;

            }
        }
    }

    private void SpawnObject()
    {
        
        GameObject spawnedObject = waves[waveNum].pool.GetPoolGameObjects();
        spawnedObject.transform.position = SpawnPostion();
        //spawnedObject.transform.rotation=transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNum].spawnedObjectCounter++;
    }
    private Vector2 SpawnPostion()
    {
        Vector2 spawnPos = new Vector2(minPos.position.x,Random.Range(minPos.position.y,MaxPos.position.y));
        return spawnPos;

    }
}
