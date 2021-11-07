using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable] public class EnemyData
    {
        [SerializeField] private Vector3 pos;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private float health;

        public Vector3 StartPos => pos;
        public Enemy EnemyPrefab => enemyPrefab;
        public float Health => health;
    }

    [SerializeField] private List<EnemyData> enemiesData;


    private List<Enemy> enemies = new List<Enemy>();
    private GameScreen gameScreen;

    public List<Enemy> Enemies => enemies;

    public void Setup(GameScreen gameScreen)
    {
        this.gameScreen = gameScreen;
    }

    public void AddEnemy()
    {
        for (int i = 0; i < enemiesData.Count; i++)
        {
            Enemy enemy = Instantiate(enemiesData[i].EnemyPrefab);
            enemies.Add(enemy);
            enemies[i].Setting(enemiesData[i].Health, enemiesData[i].StartPos, gameScreen);
        }
    }
}
