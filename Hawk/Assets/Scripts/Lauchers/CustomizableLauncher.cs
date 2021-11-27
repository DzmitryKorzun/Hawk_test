using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableLauncher : BaseLauncher
{
    [Serializable]
    private class ThreadData
    {
        [SerializeField] private Direction referenceDirection;
        [SerializeField] private float angle;

        public Direction ReferenceDirection => referenceDirection;
        public float Angle => angle;
    }

    [SerializeField] private List<ThreadData> threadDatas;

    private int threadsNum;
    private Vector3[] directions;
    private Vector3 bulletDirection;
    private float angleChangeStep;
    private float angleThread;

    public override void Move(Bullet bul, int numThread)
    {
        bul.transform.Translate(directions[numThread] * bulletSpeed);
    }

    public override int SetNumBulletsPerShot()
    {
        threadsNum = threadDatas.Count;
        return threadsNum;
    }

    private void Awake()
    {
        threadsNum = threadDatas.Count;
        for (int i = 0; i < threadDatas.Count; i++)
        {
            DirectionsGeneration();
        }
    }

    private Vector3 SetReferenceDirection(Direction referenceDirection)
    {
        switch (referenceDirection)
        {
            case Direction.Back:
                return Vector3.back;
            case Direction.Forward:
                return Vector3.forward;
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            default:
                return Vector3.forward;
        }
    }

    private void DirectionsGeneration()
    {
        directions = new Vector3[threadsNum];
        for (int i = 0; i < threadsNum; i++)
        {
            angleThread = threadDatas[i].Angle;
            directions[i] = Quaternion.Euler(0, angleThread, 0) * SetReferenceDirection(threadDatas[i].ReferenceDirection);
        }
    }
}
