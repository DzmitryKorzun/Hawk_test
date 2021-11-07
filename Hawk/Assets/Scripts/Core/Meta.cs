using Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Meta : MonoBehaviour, IMeta
    {
        [SerializeField] private Character characterPrefab;
        [SerializeField] private Collider physicalField;
        [SerializeField] private ScreenController screenController;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private Camera cameraUI;

        private Game game;
        private bool isLevelStart;

        public bool IsLevelStart => isLevelStart;
        public Character Player { get; private set; }

        private void Awake()
        {
            game = new Game(this, gameConfig);
            GoToMenu();
        }

        public void FinishGame()
        {
            SceneManager.LoadScene(0);
        }

        private void GoToMenu()
        {
            MenuScreen menuScreen = screenController.ShowMenuScreen();
            menuScreen.Setup(this);
        }

        public void StartGame()
        {
            isLevelStart = false;
            GameScreen gameScreen = screenController.ShowGameScreen();
            game.StartGame(gameScreen, characterPrefab, physicalField, enemySpawner, cameraUI);
        }
    }
}
