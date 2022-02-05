using UnityEngine;

namespace Config
{
    public class GameConfig : MonoBehaviour
    {
        [Header("Pixel/sec")] [SerializeField] private float personSpeed;
        [SerializeField] private float personHealth;
        [SerializeField] private Vector2 shipSize;
        [SerializeField] private Vector3 startShipPos;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float mapMovementSpeed;
        [SerializeField] private float cameraHeightAboveMap;
        [SerializeField] private float modelRotationSpeedOnSummaryScreen;

        public float PersonSpeed => personSpeed;
        public Vector2 ShipSize => shipSize;
        public Vector3 StartShipPos => startShipPos;
        public float BulletSpeed => bulletSpeed;
        public float PersonHealth => personHealth;
        public float MapMovementSpeed => mapMovementSpeed;
        public float CameraHeightAboveMap => cameraHeightAboveMap;
        public float ModelRotation => modelRotationSpeedOnSummaryScreen;
    }
}

