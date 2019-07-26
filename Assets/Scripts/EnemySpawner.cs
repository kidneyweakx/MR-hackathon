using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner instance;
    public GameObject enemyObject;
    public Transform enemyList;
    public Transform[] spawnerPointArray;
    private float timer;
    public float spawnTime = 5;

    private bool isActiveSpawer = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        spawnerPointArray = new Transform[transform.childCount];
        for (int i = 0; i < spawnerPointArray.Length; i++)
        {
            spawnerPointArray[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        if (isActiveSpawer)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                spawnTime = Random.Range(5.0f, 15.0f); ;
                SpawnEnemy();
            }
        }

    }
    void SpawnEnemy()
    {
        int spawnerPointIndex = Random.Range(0, spawnerPointArray.Length);
        Instantiate(enemyObject, spawnerPointArray[spawnerPointIndex].position, Quaternion.identity, enemyList);
    }

	public void StartSpawn(bool isActive){
		isActiveSpawer = isActive;
		timer = 0;
	}
}