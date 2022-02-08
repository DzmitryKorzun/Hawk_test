using UnityEngine;
using UnityEngine.UI;
using Core;
using Config;
using System.Collections.Generic;

public class ResultPannelController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private Button reloadGameButton;
    [SerializeField] private Text scoreNumText;
    [SerializeField] private Text recordNumText;
    [SerializeField] private Transform shipModel;

    private Game game;
    private GameConfig gameConfig;

    private void Start()
    {
        reloadGameButton.onClick.AddListener(RealoadLevel);
    }

    public void Setup(Game game, GameConfig gameConfig, GameObject bulletsContainers)
    {
        this.game = game;
        this.gameConfig = gameConfig;
    }

    private void RealoadLevel()
    {
        game.FinishGame();
    }

    private void FixedUpdate()
    {
        shipModel.Rotate(new Vector3(0, 0, gameConfig.ModelRotation));
    }

    private void OnEnable()
    {
        game.PauseGame(true);
        scoreNumText.text = scoreController.Score.ToString();
        recordNumText.text = scoreController.MaxScore.ToString();
    }

    private void OnDisable()
    {
        game.PauseGame(false);
    }
}

