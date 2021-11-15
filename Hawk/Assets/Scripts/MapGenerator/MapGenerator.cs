using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapEngeDetection mapRef;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Vector3 firstMapChunkPosition;
    [SerializeField] private Vector2 numberPossibleNumberInitPos;

    private float score;
    private Vector3[,] coordinateCell;
    private int chunkNum;
    private float longMap;
    private Queue<MapEngeDetection> mapEngeDetections = new Queue<MapEngeDetection>(); 
    private PhysicalAreaOfThePlayingField playingField;
    private float playingFieldX;

    private void Awake()
    {
        MapEngeDetection map = Instantiate(mapRef);
        map.transform.position = firstMapChunkPosition;
        map.Setting(this);
        SizeMapChunkDetermination(map);
    }

    private void CreateCoordinatCellInitPos(MapEngeDetection map)
    {
        coordinateCell = new Vector3[(int)numberPossibleNumberInitPos.x, (int)numberPossibleNumberInitPos.y];
        float startPosZ = map.transform.position.z - longMap;
        Vector3 startPos = new Vector3(-playingFieldX, firstMapChunkPosition.y, startPosZ);
        float stepX = (Mathf.Abs(map.transform.position.x - playingFieldX) * 2) / numberPossibleNumberInitPos.x;
        float stepZ = (Mathf.Abs(map.transform.position.z - startPosZ) * 2) / numberPossibleNumberInitPos.y;
        for (int i = 0; i < numberPossibleNumberInitPos.x; i++)
        {
            for (int j = 0; j < numberPossibleNumberInitPos.y; j++)
            {
            //    coordinateCell[i,j] = new Vector3
            }
        }
    }

    private void SizeMapChunkDetermination(MapEngeDetection map)
    {
        float posColider = map.GetComponent<Collider>().bounds.center.z;
        longMap = (posColider - map.transform.position.z) * 2;
    }
    public void Setting(PhysicalAreaOfThePlayingField playingField)
    {
        this.playingField = playingField;
        playingFieldX = playingField.BorderX;
    }

    public void AddNewChunk()
    {
        chunkNum++;
        if (mapEngeDetections.Count == 0)
        {
            MapEngeDetection map = Instantiate(mapRef);
            map.transform.position = new Vector3(firstMapChunkPosition.x, firstMapChunkPosition.y, longMap * chunkNum);
            map.Setting(this);
            CreateCoordinatCellInitPos(map);
        }
        else
        {
            MapEngeDetection map = mapEngeDetections.Dequeue();
            map.transform.position = new Vector3(firstMapChunkPosition.x, firstMapChunkPosition.y, longMap * chunkNum);
        }
    }

    public void ReturnToQueue(MapEngeDetection map)
    {
        mapEngeDetections.Enqueue(map);
    }
}