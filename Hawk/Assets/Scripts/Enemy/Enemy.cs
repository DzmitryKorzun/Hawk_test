using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private Gun[] guns;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private Collider enemyCollider;

    private int typeId;
    private EnemySpawner enemySpawner;
    private float health;
    private float maxHealth;
    private ScoreController scoreController;
    private bool isLive = true;
    private int destructionPointScore;
    private bool isGunActive;
    private Collider playingFieldColider;

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
                    Gun bullet—annon = bullet.MyGun;
                    bullet—annon.AddBulletToQueue(bullet.gameObject.transform, bullet.NumThread);
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

    public void Setting(float health, Vector3 startPos, EnemySpawner enemySpawner, ScoreController scoreController, int destructionPointScore, int typeId, PhysicalAreaOfThePlayingField playingField)
    {
        this.health = health;
        this.maxHealth = health;
        this.transform.position = startPos;
        this.enemySpawner = enemySpawner;
        this.scoreController = scoreController;
        this.destructionPointScore = destructionPointScore;
        this.typeId = typeId;
        this.playingFieldColider = playingField.gameObject.GetComponent<Collider>();
        SwitchingGuns(false);
        Debug.Log("HP = "+this.maxHealth);
    }

    public void ReturnHealth()
    {
        enemyCollider.enabled = true;
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
        }
    }
    private void TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(health - damage, 0, health);
        healthBar.FillImage(health / maxHealth);
        if (health == 0 && isLive)
        {
            enemyCollider.enabled = false;
            isLive = false;
            destroyEffect.Play();
            SwitchingGuns(false);
            Invoke(nameof(DisableShip), 1.5f);
        }
    }

    private void DisableShip()
    {
        this.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        enemySpawner.enemiesEnqueue(this, typeId);
        scoreController.AddBonusScore(5);
    }
}
