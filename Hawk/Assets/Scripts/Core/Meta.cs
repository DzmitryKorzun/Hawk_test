using Config;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Meta : MonoBehaviour, IMeta
    {
        [SerializeField] private Character characterPrefab;
        [SerializeField] private PhysicalAreaOfThePlayingField physicalField;
        [SerializeField] private ScreenController screenController;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private ResultPannelController resultPannelController;

        private Game game;
        private List<IPauseGame> pauseGameComponents = new List<IPauseGame>();


        public delegate void PauseGameDelegate(bool isPaused);
        public event PauseGameDelegate onPauseGame;

        public Character Player { get; private set; }

        SaveManager IMeta.saveManager => saveManager;

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
            GameScreen gameScreen = screenController.ShowGameScreen();
            game.StartGame(gameScreen, characterPrefab, physicalField, enemySpawner, mapGenerator, scoreController, saveManager, resultPannelController);
        }

        public void AddPauseGameComponentToList(IPauseGame isPausedComponent)
        {
            pauseGameComponents.Add(isPausedComponent);
        }

        public void PauseGame(bool value)
        {
            foreach (var component in pauseGameComponents)
            {
                component.PauseGame(value);
            }
        }
    }
}
