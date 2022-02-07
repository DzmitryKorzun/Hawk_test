using Config;
using UnityEngine;

public class PhysicalAreaOfThePlayingField : MonoBehaviour, IPauseGame
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
    private Camera cam;

    public float BorderX => borderX;

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            this.enabled = false;
            Debug.Log(this.enabled);
        }
        else
        {
          //  moveSpeed = tmpMoveSpeedCash;
          //  Debug.Log(isPaused);
        }
    }

    public void Setting(Character character, GameConfig gameConfig)
    {
        this.character = character;
        this.gameConfig = gameConfig;
        this.moveSpeed = this.gameConfig.MapMovementSpeed;
        this.field = this.gameObject.GetComponent<Collider>();
        this.transformField = GetComponent<Transform>();
        this.gameObject.SetActive(true);
        this.cam = Camera.main;
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
        cam.transform.position = new Vector3(transformField.position.x, gameConfig.CameraHeightAboveMap, transformField.position.z);        
    }
}
