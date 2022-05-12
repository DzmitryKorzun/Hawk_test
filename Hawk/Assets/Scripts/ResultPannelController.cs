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

    private IMeta meta;
    private Game game;
    private GameConfig gameConfig;
    private SaveManager saveManager;

    private void Start()
    {
        reloadGameButton.onClick.AddListener(RealoadLevel);
    }

    public void Setup(IMeta meta, Game game, SaveManager saveManager, GameConfig gameConfig, GameObject bulletsContainers)
    {
        this.meta = meta;
        this.gameConfig = gameConfig;
        this.saveManager = saveManager;
        this.game = game;
    }

    private void RealoadLevel()
    {
        saveManager.SetValue(savePoint.isSession, 1);
        game.FinishGame();
    }

    private void FixedUpdate()
    {
        shipModel.Rotate(new Vector3(0, 0, gameConfig.ModelRotation));
    }

    private void OnEnable()
    {
        scoreNumText.text = scoreController.Score.ToString();
        recordNumText.text = scoreController.MaxScore.ToString();
    }

    private void OnApplicationQuit()
    {
        saveManager.SetValue(savePoint.isSession, 0);
    }
}

