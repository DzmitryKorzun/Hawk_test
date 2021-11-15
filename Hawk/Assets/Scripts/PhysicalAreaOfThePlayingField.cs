using Config;
using UnityEngine;

public class PhysicalAreaOfThePlayingField : MonoBehaviour
{
    private Character character;
    private float moveSpeed;
    private Collider field;
    private GameConfig gameConfig;
    private Transform transformField;
    private float centerZ;
    private float borderX;
    private float borderZ;
    private Vector2 min;
    private Vector2 max;

    public float BorderX => borderX;

    public void Setting(Character character, GameConfig gameConfig)
    {
        this.character = character;
        this.moveSpeed = gameConfig.MapMovementSpeed;
        this.gameConfig = gameConfig;
        this.field = this.gameObject.GetComponent<Collider>();
        this.transformField = GetComponent<Transform>();
        this.gameObject.SetActive(true);
        borderX = (field.transform.position.x + field.bounds.size.x) / 2 - gameConfig.ShipSize.x;
        borderZ = (centerZ + field.bounds.size.z) / 2 - gameConfig.ShipSize.y;
        centerZ = field.transform.position.z;
        character.Target.transform.parent = this.transform;
    }

    private void FixedUpdate()
    {
        borderZ += moveSpeed;
        centerZ = field.transform.position.z;
        min = new Vector2(-borderX, centerZ - (borderZ - centerZ));
        max = new Vector2(borderX, borderZ);
        character.SettingShipBoundaries(min,max);
        this.transform.Translate(Vector3.forward * moveSpeed);
        Camera.main.transform.position = new Vector3(transformField.position.x, gameConfig.CameraHeightAboveMap, transformField.position.z);        
    }
}
