using UnityEngine;

namespace Config
{
    public class GameConfig : MonoBehaviour
    {
        [Header("---------- Character setting ----------")] 
        [SerializeField] private float personSpeed;
        [SerializeField] private float personHealth;
        [SerializeField] private Vector2 shipSize;
        [SerializeField] private Vector3 startShipPos;
        [SerializeField] private float bulletSpeed;
        [Header("---------- Map setting ----------")]
        [SerializeField] private float mapMovementSpeed;
        [SerializeField] private float cameraHeightAboveMap;
        [Header("---------- Result screen setting ----------")]
        [SerializeField] private float modelRotationSpeedOnSummaryScreen;
     //   [Header("---------- Result screen setting ----------")]

      //  [Header("---------- Result screen setting ----------")]


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

