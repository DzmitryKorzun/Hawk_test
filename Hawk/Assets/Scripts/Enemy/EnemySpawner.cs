using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IPauseGame
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
    [SerializeField] private Transform enemyContainer;

    private float difficultCoefficient;
    private Queue<Enemy>[] enemiesQueue;
    private ScoreController scoreController;
    private PhysicalAreaOfThePlayingField playingField;
    private GameObject bulletsContainers;

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
            enemy.Setting(enemiesData[id].Health * difficultCoefficient, pos, this, scoreController, (int)(enemiesData[id].DestructionPointScore * difficultCoefficient), id, playingField, bulletsContainers);
        }
    }

    private void CreateEnemy(Vector3 pos, int id)
    {
        Enemy enemy = Instantiate(enemiesData[id].EnemyPrefab, enemyContainer);
        enemy.Setting(enemiesData[id].Health * difficultCoefficient, pos, this, scoreController, (int)enemiesData[id].DestructionPointScore, id, playingField, bulletsContainers);
    }

    public void enemiesEnqueue(Enemy enemy, int id)
    {
        enemiesQueue[id].Enqueue(enemy);
    }

    public void Setting(ScoreController scoreController, PhysicalAreaOfThePlayingField playingField, GameObject bulletsContainers)
    {
        this.scoreController = scoreController;
        this.playingField = playingField;
        this.bulletsContainers = bulletsContainers;
    }

    public int GetEnemyTypeCount()
    {
        return enemiesQueue.Length;
    }

    public void PauseGame(bool isPaused)
    {
        
    }
}
