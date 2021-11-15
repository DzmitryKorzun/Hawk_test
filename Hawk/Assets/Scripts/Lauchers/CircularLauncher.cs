using UnityEngine;

public class CircularLauncher : BaseLauncher
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject pivot;

    private void Awake()
    {
        GetBulletSpeed();
        gun.transform.localPosition = new Vector3(radius, 0, 0);
    }

    public override void Move(Bullet bul)
    {
        bul.transform.RotateAround(pivot.transform.position, Vector3.up, bulletSpeed);
    }
}
