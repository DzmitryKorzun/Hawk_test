using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text maxScore;
    [SerializeField] private Text lastAttemptText;

    private IMeta meta;
    private SaveManager saveManager;
    public void Setup(IMeta meta)
    {
        this.meta = meta;
        this.saveManager = meta.saveManager;
        maxScore.text = string.Concat("Record: ", saveManager.GetValue<int>(savePoint.maxScore));
        lastAttemptText.text = string.Concat("Last attempt: ", saveManager.GetValue<int>(savePoint.lastRace));
        playButton.onClick.AddListener(GameStartEvent);
    }

    private void GameStartEvent()
    {
        meta.StartGame();
        this.gameObject.SetActive(false);
    }
}
