using UnityEngine;

public class CircularLauncher : BaseLauncher
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] protected Gun gun;

    private void Awake()
    {
        gun.transform.localPosition = new Vector3(radius, 0, 0);
    }

    public override void Move(Bullet bul, int numThread)
    {
        bul.transform.RotateAround(pivotPoint.transform.position, Vector3.up, bulletSpeed);
    }
}
