using Config;
using UnityEngine;

public interface IPauseGame
{
    void PauseGame(bool isPaused);
}


namespace Core
{
    public class Game : MonoBehaviour
    {
        private Character character;
        private PhysicalAreaOfThePlayingField physicalField;
        private IMeta meta;
        private GameConfig config;
        private GameObject bulletsContainers;


        public Game(IMeta iMeta, GameConfig config)
        {
            meta = iMeta;
            this.config = config;
            
        }

        public void StartGame(GameScreen gameScreen, Character person, PhysicalAreaOfThePlayingField field, EnemySpawner enemySpawner, MapGenerator mapGenerator, ScoreController scoreController, SaveManager saveManager, ResultPannelController resultPannelController)
        {
            bulletsContainers = new GameObject("bulletsContainers");
            resultPannelController.Setup(this, config, bulletsContainers);
            this.physicalField = Instantiate(field);
            this.physicalField.gameObject.SetActive(false);
            this.character = Instantiate(person);
            this.character.gameObject.SetActive(true);
            this.character.Setup(config.PersonSpeed, config.StartShipPos, config.ShipSize, config.BulletSpeed, this, gameScreen, config.PersonHealth, resultPannelController, bulletsContainers);
            this.physicalField.Setting(character, config);
            enemySpawner.Setting(scoreController, physicalField, bulletsContainers);
            mapGenerator.Setting(physicalField, enemySpawner);
            scoreController.Setting(config.MapMovementSpeed, saveManager);
            gameScreen.Setting(scoreController);
            meta.AddPauseGameComponentToList(enemySpawner);
        }

        public void FinishGame()
        {
            meta.FinishGame();
        }

        public void PauseGame(bool value)
        {
            meta.PauseGame(value);
        }

    }
}

