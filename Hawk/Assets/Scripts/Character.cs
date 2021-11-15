using Core;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Vector2 shipSize;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private GameObject movementTarget;
    [SerializeField] private ParticleSystem shipDestroyEffect;
    [SerializeField] private Gun[] guns;
    [SerializeField] private HealthBarController healthBar;

    private float maxSpeed;
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

    public GameObject Target => target;

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
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
                        newPos = new Vector3(Mathf.Clamp((worldPosition.x - deltaX), min.x, max.x), 0, Mathf.Clamp((worldPosition.z - deltaZ), min.y, max.y));
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
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                plane = new Plane(Vector3.up, 0);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out distance))
                {
                    worldPosition = ray.GetPoint(distance);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                deltaX = worldPosition.x - target.transform.position.x;
                deltaZ = worldPosition.z - target.transform.position.z;
            }
            else
            {
                newPos = new Vector3(Mathf.Clamp((worldPosition.x - deltaX), min.x, max.x), 0, Mathf.Clamp((worldPosition.z - deltaZ), min.y, max.y));
                target.transform.position = newPos;
            }
            charcterTransform.position = Vector3.MoveTowards(charcterTransform.position, target.transform.position, maxSpeed);
            deltaX = worldPosition.x - target.transform.position.x;
            deltaZ = worldPosition.z - target.transform.position.z;
            if (Input.GetMouseButton(1)) TakeDamage(maxHealth);
        }
    }
    public void SettingShipBoundaries(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;

    }

    private void SettingBulletWeaponsSpeed(float speed)
    {
        foreach (Gun myGun in guns)
        {
            myGun.BulletSpeed = speed;
        }
    }

    public void Setup(float speed, Vector3 startPos, Vector2 shipSize, float bulletSpeed, Game game, GameScreen gameScreen, float health)
    {
        this.game = game;
        this.shipSize = shipSize;
        charcterTransform = this.GetComponent<Transform>();
        charcterTransform.position = startPos;       
        target = Instantiate(movementTarget);
        target.transform.position = startPos;
        SettingBulletWeaponsSpeed(bulletSpeed);
        maxSpeed = speed;
        this.health = health;
        this.maxHealth = health;
    }

    private void TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(health - damage, 0, health);
        healthBar.FillImage(health / maxHealth);
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

