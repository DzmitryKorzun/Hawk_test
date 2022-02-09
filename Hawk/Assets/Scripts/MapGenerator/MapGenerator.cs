using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour, IPauseGame
{
    [SerializeField] private MapEngeDetection mapRef;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Vector3 firstMapChunkPosition;
    [SerializeField] private float minRadius;
    [SerializeField] private GameObject scoreController;
    [SerializeField] private MedicineChest healthBox;
    [SerializeField] private MedicineChestSpawner medecineChestSpawner;

    private int chunkNum;
    private float longMap;
    private Queue<MapEngeDetection> mapEngeDetections = new Queue<MapEngeDetection>();
    private PhysicalAreaOfThePlayingField playingField;
    private float playingFieldX;
    private float minPlayingFieldY;
    private float maxPlayingFieldY;

    private void Awake()
    {
        MapEngeDetection map = Instantiate(mapRef);
        map.transform.position = firstMapChunkPosition;
        map.Setting(this);
        SizeMapChunkDetermination(map);       
    }
    
    private void SizeMapChunkDetermination(MapEngeDetection map)
    {
        float posColider = map.GetComponent<Collider>().bounds.center.z;
        longMap = (posColider - map.transform.position.z);
    }

    private void AddEnemyToRandomPosition(MapEngeDetection map)
    {
        FieldBoundaryDefinition(map);
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < enemySpawner.GetEnemyTypeCount(); i++)
        {
            pos = GetRandomMapPosition();
            enemySpawner.AddEnemy(pos, i);
        }
    }

    private Vector3 GetRandomMapPosition()
    {
        float randPosX = UnityEngine.Random.Range(-playingFieldX + minRadius, playingFieldX - minRadius);
        float randPosY = UnityEngine.Random.Range(minPlayingFieldY, maxPlayingFieldY);
        return new Vector3(randPosX, 0, randPosY);
    }

    private void FieldBoundaryDefinition(MapEngeDetection map)
    {
        minPlayingFieldY = map.transform.position.z + longMap - minRadius;
        maxPlayingFieldY = map.transform.position.z - longMap + minRadius;
    }

    private void AttemptAddHealthBox()
    {
        MedicineChest medicineChest = medecineChestSpawner.TryGenerateMedicineChest();
        if (medicineChest != null)
        {
            medicineChest.gameObject.transform.position = GetRandomMapPosition();
        }
    }

    public void Setting(PhysicalAreaOfThePlayingField playingField, EnemySpawner enemySpawner)
    {
        this.playingField = playingField;
        this.enemySpawner = enemySpawner;
        playingFieldX = playingField.BorderX;
    }
    
    public void AddNewChunk()
    {
        scoreController.SetActive(true);
        chunkNum++;
        if (mapEngeDetections.Count == 0)
        {
            MapEngeDetection map = Instantiate(mapRef);
            map.transform.position = new Vector3(firstMapChunkPosition.x, firstMapChunkPosition.y, longMap * chunkNum * 2);
            map.Setting(this);
            AddEnemyToRandomPosition(map);
            AttemptAddHealthBox();
        }
        else
        {
            MapEngeDetection map = mapEngeDetections.Dequeue();
            map.transform.position = new Vector3(firstMapChunkPosition.x, firstMapChunkPosition.y, longMap * chunkNum * 2);
            AddEnemyToRandomPosition(map);
            AttemptAddHealthBox();
        }
    }

    public void ReturnToQueue(MapEngeDetection map)
    {
        mapEngeDetections.Enqueue(map);
    }

    public void PauseGame(bool isPaused)
    {
        
    }
}