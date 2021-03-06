using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private Gun[] guns;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private MeshRenderer meshRenderer;


    private int typeId;
    private EnemySpawner enemySpawner;
    private float health;
    private float maxHealth;
    private ScoreController scoreController;
    private bool isLive = true;
    private int destructionPointScore;
    private bool isGunActive;
    private Collider playingFieldColider;
    private float collisionDamage;

    public float CollisionDamage => collisionDamage;
    public float MaxHealth => maxHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (isGunActive)
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (bullet.BulletOwner == WeaponOwner.Person)
                {
                    TakeDamage(bullet.Damage);
                    bullet.gameObject.SetActive(false);
                    Gun bullet?annon = bullet.MyGun;
                    bullet?annon.AddBulletToQueue(bullet.gameObject.transform, bullet.NumThread);
                }
            }
        }
        else
        {
            SwitchingGuns(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playingFieldColider.Equals(other))
        {
            DisableShip();
        }
    }

    public void Setting(float health, Vector3 startPos, EnemySpawner enemySpawner, ScoreController scoreController, int destructionPointScore, int typeId, PhysicalAreaOfThePlayingField playingField, GameObject bulletsContainers, float collisionDamage)
    {
        this.health = health;
        this.maxHealth = health;
        this.transform.position = startPos;
        this.enemySpawner = enemySpawner;
        this.scoreController = scoreController;
        this.destructionPointScore = destructionPointScore;
        this.typeId = typeId;
        this.playingFieldColider = playingField.gameObject.GetComponent<Collider>();
        this.collisionDamage = collisionDamage;
        SwitchingGuns(false);
        SetupAllGuns(bulletsContainers);
    }

    public void ReturnHealth()
    {
        enemyCollider.enabled = true;
        meshRenderer.enabled = true;
        isLive = true;
        health = maxHealth;
        healthBar.gameObject.SetActive(true);
        healthBar.FillImage(health / maxHealth);
    }

    private void SwitchingGuns(bool isActive)
    {
        foreach (Gun myGun in guns)
        {
            myGun.enabled = isActive;
            isGunActive = isActive;
            if(isGunActive)
            {
                myGun.StartFire();
            }
            else
            {
                myGun.StopFiring();
            }
        }
    }

    private void SetupAllGuns(GameObject bulletsContainers)
    {
        foreach (Gun myGun in guns)
        {
            myGun.Setup(bulletsContainers);
        }
    }

    public void TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(health - damage, 0, health);
        healthBar.FillImage(health / maxHealth);
        if (health == 0 && isLive)
        {
            enemyCollider.enabled = false;
            isLive = false;
            destroyEffect.Play();
            SwitchingGuns(false);
            meshRenderer.enabled = false;
            healthBar.gameObject.SetActive(false);
            Invoke(nameof(DisableShip), 1.5f);
        }
    }

    private void DisableShip()
    {
        SwitchingGuns(false);
        this.gameObject.SetActive(false);
        enemySpawner.enemiesEnqueue(this, typeId);
        scoreController.AddBonusScore(5);
    }
}
