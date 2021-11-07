using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    private IMeta meta;
    [SerializeField] private Button button;
    public void Setup(IMeta meta)
    {
        this.meta = meta;
        button.onClick.AddListener(GameStartEvent);
    }

    private void GameStartEvent()
    {
        meta.StartGame();
        this.gameObject.SetActive(false);
    }
}
