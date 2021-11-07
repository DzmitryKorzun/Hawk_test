using Config;
using UnityEngine;

namespace Core
{
    public class Game : MonoBehaviour
    {
        private Character character;
        private Collider physicalField;
        private IMeta meta;
        private GameConfig config;
        public Game(IMeta iMeta, GameConfig config)
        {
            meta = iMeta;
            this.config = config;
        }

        public void StartGame(GameScreen gameScreen, Character person, Collider field, EnemySpawner enemySpawner, Camera cameraUI)
        {
            this.physicalField = Instantiate(field);
            this.character = Instantiate(person);
            character.gameObject.SetActive(true);
            character.Setup(config.PersonSpeed, config.StartShipPos, config.ShipSize, config.BulletSpeed, physicalField, this, gameScreen, config.PersonHealth, cameraUI);
            enemySpawner.Setup(gameScreen);
            enemySpawner.AddEnemy();
        }

        public void FinishGame()
        {
            meta.FinishGame();
        }
    }
}

