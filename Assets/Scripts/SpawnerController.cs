using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerController : MonoBehaviour
{
    public WaveScriptableObject[] wavesSO;
    private DelayedExecutionTicket[] tickets;
    private DelayedExecutionTicket ticket;
    private GameObject target;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public static event Action<int> OnEnemiesSet;

    private int spawnerCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        tickets = new DelayedExecutionTicket[wavesSO.Length];
        //just a temp solution --- will be removed afterwards
        target = GameObject.Find("Player");

        if (wavesSO.Length > 0)
        {
            tickets[0] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[0].waveTime, () =>
            {
                Spawn(spawnPoint1.position, wavesSO[0].spawnPos1Enemies, spawnPoint2.position, wavesSO[0].spawnPos2Enemies);
            });
        }
        if (wavesSO.Length > 1)
        {
            tickets[1] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[1].waveTime, () =>
            {
                Spawn(spawnPoint1.position, wavesSO[1].spawnPos1Enemies, spawnPoint2.position, wavesSO[1].spawnPos2Enemies);
            });
        }
        if (wavesSO.Length > 2)
        {
            tickets[2] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[2].waveTime, () =>
            {
                Spawn(spawnPoint1.position, wavesSO[2].spawnPos1Enemies, spawnPoint2.position, wavesSO[2].spawnPos2Enemies);
            });
        }
        if (wavesSO.Length > 3)
        {
            tickets[3] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[3].waveTime, () =>
            {
                Spawn(spawnPoint1.position, wavesSO[3].spawnPos1Enemies, spawnPoint2.position, wavesSO[3].spawnPos2Enemies);
            });
        }
        if (wavesSO.Length > 4)
        {
            tickets[4] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[4].waveTime, () =>
            {
                Spawn(spawnPoint1.position, wavesSO[4].spawnPos1Enemies, spawnPoint2.position, wavesSO[4].spawnPos2Enemies);
            });
        }

        //for (int i = 0; i < wavesSO.Length; i++)
        //{
        //    tickets[i] = DelayedExecutionManager.ExecuteActionAfterDelay(wavesSO[i].waveTime, () =>
        //    {
        //        Spawn(spawnPoint1.position, wavesSO[i].spawnPos1Enemies, spawnPoint2.position, wavesSO[i].spawnPos2Enemies);
        //    });
        //}

        CalculateTotalEnemies();
    }

    private void CalculateTotalEnemies()
    {
        int totalEnemies = 0;

        foreach (var enemies in wavesSO)
        {
            totalEnemies += enemies.spawnPos1Enemies.Length;
            totalEnemies += enemies.spawnPos2Enemies.Length;
        }
        OnEnemiesSet?.Invoke(totalEnemies);
    }

    private void Spawn(Vector3 spawnPosition1, EnemyType[] spawnPos1Enemies, Vector3 spawnPosition2, EnemyType[] spawnPos2Enemies)
    {
        for (int i = 0; i < spawnPos1Enemies.Length; i++)
        {
            var enemy = EnemyProvider.GetEnemy(spawnPos1Enemies[i]).GetComponent<EnemyAIController>();
            enemy.transform.position = spawnPosition1;
        }
        for (int i = 0; i < spawnPos2Enemies.Length; i++)
        {
            var enemy = EnemyProvider.GetEnemy(spawnPos2Enemies[i]).GetComponent<EnemyAIController>();
            enemy.transform.position = spawnPosition2;
        }
    }

    //private void Spawn()
    //{
    //    //spawn...
    //    var randomEnemyType = HelperFunctions.RandomEnumElement<EnemyType>();

    //    var enemy = EnemyProvider.GetEnemy(EnemyType.MeleeWeak).GetComponent<EnemyAIController>();
    //    //var enemy = EnemyProvider.GetEnemy(randomEnemyType).GetComponent<EnemyController>();
    //    //enemy.Activate(transform.position, target.transform);

    //    //keep spawning...
    //    ticket = DelayedExecutionManager.ExecuteActionAfterDelay(3000, () =>
    //    {
    //        Spawn();
    //    });


    //}

    private void OnDisable()
    {
        //DelayedExecutionManager.CancelTicket(ticket);
        foreach (var ticket in tickets)
        {
            DelayedExecutionManager.CancelTicket(ticket);
        }
    }

    private void OnDestroy()
    {
        //DelayedExecutionManager.CancelTicket(ticket);
        foreach (var ticket in tickets)
        {
            DelayedExecutionManager.CancelTicket(ticket);
        }
    }
}

public class EnemyProvider
{
    public static GameObject GetEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.MeleeWeak:
                return AssetProvider.GetAsset(GameAsset.MeleeWeak);
            case EnemyType.MeleeFast:
                return AssetProvider.GetAsset(GameAsset.MeleeFast);
            case EnemyType.MeleeStrong:
                return AssetProvider.GetAsset(GameAsset.MeleeStrong);
            case EnemyType.Ranged:
                return AssetProvider.GetAsset(GameAsset.Ranged);
            case EnemyType.Mage:
                return AssetProvider.GetAsset(GameAsset.Mage);
        }
        return null;
    }
}

public enum EnemyType
{
    MeleeWeak,
    MeleeFast,
    MeleeStrong,
    Ranged,
    Mage,
}

[System.Serializable]
public struct Wave
{
    public int waveTime;
    public Vector3 spawnPosition1;
    public EnemyType[] spawnPos1Enemies;
    public Vector3 spawnPosition2;
    public EnemyType[] spawnPos2Enemies;
}