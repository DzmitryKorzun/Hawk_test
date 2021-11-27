using UnityEngine;

public class CornerLauncher: BaseLauncher
{
    [SerializeField] private float firingSectorAngle;
    [SerializeField] private int threadsNum;
    [SerializeField] private Direction referenceDirection;

    private int activeThreadNum = 0;
    private Vector3 angleReadingDirection;
    private Vector3[] directions;
    private float angleChangeStep;
    private float angleThread;

    public override void Move(Bullet bul, int numThread)
    {
        bul.transform.Translate(directions[numThread] * bulletSpeed);
    }

    public override int SetNumBulletsPerShot()
    {
        return threadsNum;
    }

    private void Awake()
    {
        switch(referenceDirection)
        {
            case Direction.Back:
                angleReadingDirection = Vector3.back;
                break;
            case Direction.Forward:
                angleReadingDirection = Vector3.forward;
                break;
            case Direction.Left:
                angleReadingDirection = Vector3.left;
                break;
            case Direction.Right:
                angleReadingDirection = Vector3.right;
                break;
        }
        DirectionsGeneration();
    }

    private void DirectionsGeneration()
    {
        directions = new Vector3[threadsNum];
        angleChangeStep = firingSectorAngle / (threadsNum - 1);
        angleThread = (firingSectorAngle / 2) * -1;

        for (int i = 0; i < threadsNum ; i++)
        {

            directions[i] = Quaternion.Euler(0, angleThread, 0) * angleReadingDirection;
            angleThread += angleChangeStep;
        }
    }
}
