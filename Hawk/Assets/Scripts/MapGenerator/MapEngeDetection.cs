using UnityEngine;

public class MapEngeDetection : MonoBehaviour
{
    private MapGenerator mapGenerator;
    private bool isAddingFreely = true;

    public void Setting(MapGenerator mapGenerator)
    {
        this.mapGenerator = mapGenerator;
    }

    private void OnCollisionEnter(Collision collision)
    {
        mapGenerator.AddNewChunk();
    }

    private void OnCollisionExit(Collision collision)
    {
        isAddingFreely = true;
        mapGenerator.ReturnToQueue(this);
    }
}
