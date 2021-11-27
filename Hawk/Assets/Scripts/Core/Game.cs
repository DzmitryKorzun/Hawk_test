using Config;
using UnityEngine;

namespace Core
{
    public class Game : MonoBehaviour
    {
        private Character character;
        private PhysicalAreaOfThePlayingField physicalField;
        private IMeta meta;
        private GameConfig config;

        public Game(IMeta iMeta, GameConfig config)
        {
            meta = iMeta;
            this.config = config;
        }

        public void StartGame(GameScreen gameScreen, Character person, PhysicalAreaOfThePlayingField field, EnemySpawner enemySpawner, MapGenerator mapGenerator, ScoreController scoreController, SaveManager saveManager)
        {
            this.physicalField = Instantiate(field);
            this.physicalField.gameObject.SetActive(false);
            this.character = Instantiate(person);
            character.gameObject.SetActive(true);
            character.Setup(config.PersonSpeed, config.StartShipPos, config.ShipSize, config.BulletSpeed, this, gameScreen, config.PersonHealth);
            physicalField.Setting(character, config);
            enemySpawner.Setting(scoreController, physicalField);
            mapGenerator.Setting(physicalField, enemySpawner);
            scoreController.Setting(config.MapMovementSpeed, saveManager);
            gameScreen.Setting(scoreController);
        }

        public void FinishGame()
        {
            meta.FinishGame();
        }
    }
}

