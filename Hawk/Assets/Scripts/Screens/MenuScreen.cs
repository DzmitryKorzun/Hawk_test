using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text maxScore;
    [SerializeField] private Text lastAttemptText;
    [SerializeField] private GameObject shopPannel;
    [SerializeField] private Dropdown difficultDropdown;

    private IMeta meta;
    private SaveManager saveManager;
    private List<string> difficultOptions;
    private GameDifficult gameDifficult;

    private void Awake()
    {
        difficultOptions = System.Enum.GetNames(typeof(GameDifficult)).ToList();
        difficultDropdown.AddOptions(difficultOptions);
    }

    public void Setup(IMeta meta)
    {
        this.meta = meta;
        this.saveManager = meta.saveManager;
        maxScore.text = string.Concat("Record: ", saveManager.GetValue<int>(savePoint.maxScore));
        lastAttemptText.text = string.Concat("Last attempt: ", saveManager.GetValue<int>(savePoint.lastRace));
        playButton.onClick.AddListener(GameStartEvent);
        if (saveManager.GetValue<int>(savePoint.isSession) == 1) GameStartEvent();
    }

    private void GameStartEvent()
    {
        meta.StartGame();
        this.gameObject.SetActive(false);
    }

    private void ShopButton_Click()
    {
        shopPannel.gameObject.SetActive(!shopPannel.activeSelf);
    }
}
