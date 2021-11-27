using UnityEngine;

enum Direction
{
    Forward,
    Back,
    Left,
    Right
}

public class LinearLauncher : BaseLauncher
{
    [SerializeField] private float angle;
    [SerializeField] private Direction enumDirection;
    [SerializeField] private bool isEnum;

    private Vector3 direction;

    private void Awake()
    {
        if (isEnum)
        {
            switch (enumDirection)
            {
                case Direction.Forward:
                    direction = Vector3.forward;
                    break;
                case Direction.Back:
                    direction = Vector3.back;
                    break;
                case Direction.Left:
                    direction = Vector3.left;
                    break;
                case Direction.Right:
                    direction = Vector3.right;
                    break;
                default:
                    break;
            }
        }
        else
        {
            direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        }
    }

    public override void Move(Bullet bul, int numThread)
    {
        bul.transform.Translate(direction * bulletSpeed);
    }
}
