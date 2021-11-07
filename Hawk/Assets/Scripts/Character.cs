using Core;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private Collider field;
    [SerializeField] private Vector2 shipSize;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private GameObject movementTarget;
    [SerializeField] private ParticleSystem shipDestroyEffect;
    [SerializeField] private Gun[] guns;
    [SerializeField] private Image healthBar;

    private Vector3 worldPosition;
    private Plane plane;
    private Touch touch;
    private Ray ray;
    private float deltaX, deltaZ;
    private float distance;
    private Vector3 newPos;
    private Vector2 min;
    private Vector2 max;
    private GameObject target;
    private Transform charcterTransform;
    private Game game;
    private float health;
    private float maxHealth;


    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            plane = new Plane(Vector3.up, 0);
            ray = Camera.main.ScreenPointToRay(touch.position);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = worldPosition.x - target.transform.position.x;
                    deltaZ = worldPosition.z - target.transform.position.z;
                    break;
                case TouchPhase.Moved:
                    newPos = new Vector3(Mathf.Clamp((worldPosition.x - deltaX),min.x, max.x), 0, Mathf.Clamp((worldPosition.z - deltaZ),min.y,max.y));
                    target.transform.position = newPos;
                    deltaX = worldPosition.x - target.transform.position.x;
                    deltaZ = worldPosition.z - target.transform.position.z;
                    break;
                case TouchPhase.Ended:
                    deltaX = 0;
                    deltaZ = 0;
                    break;
            }
        }
        else
        {
            deltaX = 0;
            deltaZ = 0;
        }
        charcterTransform.position = Vector3.MoveTowards(charcterTransform.position, target.transform.position, maxSpeed);     
    }

    private float SpeedConverter(float speed)
    {
        float screenWorldWidth = max.y - min.y;
        float screenPixeldWidth = Screen.height;
        float worldTranslatePerSecond = speed / (screenPixeldWidth / screenWorldWidth);
        return worldTranslatePerSecond / (1 / Time.fixedDeltaTime);
    }

    private void SettingShipBoundaries()
    {
        float X = (field.bounds.center.x + field.bounds.size.x) / 2 - shipSize.x;
        float Z = (field.bounds.center.z + field.bounds.size.z) / 2 - shipSize.y;
        min = new Vector2(-X, -Z);
        max = new Vector2(X, Z);
    }

    private void SettingBulletWeaponsSpeed(float speed)
    {
        foreach (Gun myGun in guns)
        {
            myGun.BulletSpeed = speed;
        }
    }

    public void Setup(float speed, Vector3 startPos, Vector2 shipSize, float bulletSpeed, Collider field, Game game, GameScreen gameScreen, float health, Camera camera)
    {
        this.game = game;
        this.shipSize = shipSize;
        charcterTransform = this.GetComponent<Transform>();
        charcterTransform.position = startPos;
        this.field = field;        
        target = Instantiate(movementTarget);
        target.transform.position = startPos;
        SettingShipBoundaries();
        SettingBulletWeaponsSpeed(bulletSpeed);
        maxSpeed = SpeedConverter(speed);
        this.health = health;
        this.maxHealth = health;
    }

    private void TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(health - damage, 0, health);
        healthBar.fillAmount = health / maxHealth;
        if (health == 0)
        {
            maxSpeed = 0;
            shipDestroyEffect.Play();
            DisableGuns();
            Invoke("GameOver", 1.5f);
        }
    }

    private void GameOver()
    {
        game.FinishGame();
    }

    private void DisableGuns()
    {
        foreach (Gun myGun in guns)
        {
            myGun.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.BulletOwner == WeaponOwner.Enemy)
            {
                TakeDamage(bullet.Damage);
                bullet.gameObject.SetActive(false);
                Gun bullet—annon = bullet.MyGun;
                bullet—annon.AddBulletToQueue(bullet.gameObject.transform);
            }
        }
    }
}

