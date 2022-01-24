using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable] private class EnemyData
    {
        [SerializeField] private Vector3 pos;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private float health;
        [SerializeField] private float destructionPointScore;

        public Vector3 StartPos => pos;
        public Enemy EnemyPrefab => enemyPrefab;
        public float Health => health;
        public float DestructionPointScore => destructionPointScore;
    }

    [SerializeField] private List<EnemyData> enemiesData;

    private float difficultCoefficient;
    private Queue<Enemy>[] enemiesQueue;
    private ScoreController scoreController;
    private PhysicalAreaOfThePlayingField playingField;


    public float DifficultCoefficient
    {
        set
        {
            difficultCoefficient = value;
        }
        get
        {
            return difficultCoefficient;
        }
    }

    private void Awake()
    {
        enemiesQueue = new Queue<Enemy>[enemiesData.Count];
        for (int i = 0; i < enemiesQueue.Length; i++)
        {
            enemiesQueue[i] = new Queue<Enemy>();
        }
    }

    public void AddEnemy(Vector3 pos, int id)
    {
        if (enemiesQueue[id].Count == 0)
        {
            CreateEnemy(pos, id);
        }
        else
        {
            Enemy enemy = enemiesQueue[id].Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.ReturnHealth();
            enemy.Setting(enemiesData[id].Health * difficultCoefficient, pos, this, scoreController, (int)(enemiesData[id].DestructionPointScore * difficultCoefficient), id, playingField);
        }
    }

    private void CreateEnemy(Vector3 pos, int id)
    {
        Enemy enemy = Instantiate(enemiesData[id].EnemyPrefab);
        enemy.Setting(enemiesData[id].Health * difficultCoefficient, pos, this, scoreController, (int)enemiesData[id].DestructionPointScore, id, playingField);
    }

    public void enemiesEnqueue(Enemy enemy, int id)
    {
        enemiesQueue[id].Enqueue(enemy);
    }

    public void Setting(ScoreController scoreController, PhysicalAreaOfThePlayingField playingField)
    {
        this.scoreController = scoreController;
        this.playingField = playingField;
    }

    public int GetEnemyTypeCount()
    {
        return enemiesQueue.Length;
    }
}
