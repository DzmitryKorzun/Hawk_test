using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private Gun[] guns;
    [SerializeField] private Image healthBar;

    private float health;
    private float maxHealth;

    private void TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(health - damage, 0, health);
        healthBar.fillAmount = health / maxHealth;
        if (health == 0)
        {
            destroyEffect.Play();
            DisableGuns();
            Invoke("DisableShip", 1.5f);
        }
    }

    private void DisableShip()
    {
        this.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
    }

    private void DisableGuns()
    {
        foreach (Gun myGun in guns)
        {
            myGun.enabled = false;
        }
    }

    public void Setting(float health, Vector3 startPos, GameScreen gameScreen)
    {
        this.health = health;
        this.maxHealth = health;
        this.transform.position = startPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.BulletOwner == WeaponOwner.Person)
            {
                TakeDamage(bullet.Damage);
                bullet.gameObject.SetActive(false);
                Gun bullet—annon = bullet.MyGun;
                bullet—annon.AddBulletToQueue(bullet.gameObject.transform);
            }
        }
    }
}
