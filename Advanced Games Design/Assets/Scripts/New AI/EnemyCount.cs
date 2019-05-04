using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public static EnemyCount instance;

    public GameObject[] numberOfEnemies;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple enemycount's found");
        }
        instance = this;

        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
