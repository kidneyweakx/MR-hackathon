using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loser;
    public int playerHealth = 10;
    public Image healthImahe;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // StartGame(true);
    }
    public void PlayerUnderAttack()
    {
        playerHealth -= 1;
        SetHealthMask(playerHealth);
        if (playerHealth == 0)
        {
            StartGame(false);
        }
    }
    public void StartGame(bool isStart)
    {
        EnemySpawner.instance.StartSpawn(isStart);
        if (isStart)
        {
            healthImahe.enabled = false;
            playerHealth = 10;
        }
        else
        {
            foreach (Transform elment in EnemySpawner.instance.enemyList)
            {
                Destroy(elment.gameObject);
            }
            healthImahe.enabled = false;
            ModeTrantitionManager.instance.SetMixedRealityModeAR(true);
            loser.SetActive(true);
        }
    }
    void SetHealthMask(int value)
    {
        if (playerHealth < 10)
        {
            healthImahe.enabled = true;
        }
        else
        {
            healthImahe.enabled = false;
        }
        healthImahe.color = new Color(healthImahe.color.r, healthImahe.color.g, healthImahe.color.b, 1 - value / 10.0f);
    }
}