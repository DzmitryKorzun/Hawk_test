using Core;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IPauseGame
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
    private ResultPannelController resultPannel;
    private RuntimePlatform runtimePlatform;

    public GameObject Target => target;

    private void Awake()
    {
        runtimePlatform = Application.platform;
    }

    private void FixedUpdate()
    {
        if (runtimePlatform == RuntimePlatform.Android)
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
        if (runtimePlatform == RuntimePlatform.WindowsEditor)
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

    public void Setup(float speed, Vector3 startPos, Vector2 shipSize, float bulletSpeed, Game game, GameScreen gameScreen, float health, ResultPannelController resultPannelController, GameObject bulletsContainers)
    {
        this.game = game;
        this.shipSize = shipSize;
        this.charcterTransform = this.GetComponent<Transform>();
        this.charcterTransform.position = startPos;
        this.target = Instantiate(movementTarget);
        this.target.transform.position = startPos;
        this.SettingBulletWeaponsSpeed(bulletSpeed);
        this.maxSpeed = speed;
        this.health = health;
        this.maxHealth = health;
        this.resultPannel = resultPannelController;
        SetupAllGuns(bulletsContainers);
    }

    private void SettingBulletWeaponsSpeed(float speed)
    {
        foreach (Gun myGun in guns)
        {
            myGun.BulletSpeed = speed;
        }
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
            Invoke(nameof(GameOver), 1.5f);
        }
    }
    private void SetupAllGuns(GameObject bulletsContainers)
    {
        foreach (Gun myGun in guns)
        {
            myGun.Setup(bulletsContainers);
        }
    }
    private void GameOver()
    {
        resultPannel.gameObject.SetActive(true);
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
                bullet—annon.AddBulletToQueue(bullet.gameObject.transform, bullet.NumThread);
            }
        }
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            DisableGuns();
        }
        else
        {

        }
    }
}

